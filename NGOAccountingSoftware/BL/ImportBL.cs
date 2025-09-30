using ClosedXML.Excel;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Spreadsheet;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TrustApplication.Exceptions;

namespace TrustApplication
{
    class ImportBL : BL
    {
        ImportDAL dal = new ImportDAL();
        RentBL rbl = new RentBL();
        CertificateBL cbl = new CertificateBL();
        AccountsBL abl = new AccountsBL();
        Utils util = new Utils();

        private Func<string, Task> _log;
        private Task Log(string msg) => _log(msg);

        public async void ImportAccounts(string FilePath, string UserGivenName, bool? HasHeader, Func<string, Task>? logAsync,
            ProgressBar Progress)
        {
            _log = logAsync ?? (_ => Task.CompletedTask);

            // Validate inputs
            if (string.IsNullOrWhiteSpace(UserGivenName))
                throw new InvalidOperationException("User Given Name is required.");
            if (!File.Exists(FilePath))
                throw new FileNotFoundException("Excel file not found.", FilePath);

            var fileInfo = new FileInfo(FilePath);
            await Log("Opening workbook...");
            using var wb = new XLWorkbook(fileInfo.FullName);
            int sheetCount = wb.Worksheets.Count;
            await Log($"Found {sheetCount} sheet(s).");

            long fileId;
            fileId = await dal.InsertImportFileAsync(UserGivenName, originalFilename: fileInfo.Name,
            storagePath: fileInfo.FullName, mimeType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            fileSizeBytes: fileInfo.Length);

            await Log($"Created import_file id={fileId}.");

            //dal.beginTransaction();

            var inserted = 0L;
            var duplicates = 0L;
            var errors = 0L;
            var totalRows = 0L;

            int sheetIndex = 0;
            foreach (var ws in wb.Worksheets)
            {
                sheetIndex++;
                Progress.Value = Math.Min(95, (double)sheetIndex / sheetCount * 80 + 10);
                await Log($"Sheet {sheetIndex}/{sheetCount}: \"{ws.Name}\"");

                var lastRow = ws.LastRowUsed()?.RowNumber() ?? 0;
                if (lastRow == 0)
                {
                    await Log("  (empty sheet)");
                    continue;
                }

                // Build header map
                int headerRowNumber = HasHeader == true ? 1 : 0;
                Dictionary<string, int> headerToCol = new();

                if (headerRowNumber == 1)
                {
                    var headerRow = ws.Row(1);
                    foreach (var cell in headerRow.CellsUsed())
                    {
                        var key = NormalizeHeader(cell.GetString());
                        if (!string.IsNullOrWhiteSpace(key) && !headerToCol.ContainsKey(key))
                            headerToCol[key] = cell.Address.ColumnNumber;
                    }
                }
                else
                {
                    // If you ever need headerless files, map by fixed positions here
                    throw new InvalidOperationException("This sample expects a header row. Enable the checkbox.");
                }

                // Resolve column mapping to our schema names (case/space/slash insensitive)
                var map = BuildColumnMap(headerToCol);

                // Loop through data rows
                int startRow = 2; // because header at row 1
                var lastDataRow = ws.LastRowUsed()?.RowNumber() ?? 1;
                for (int r = startRow; r <= lastDataRow; r++)
                {
                    totalRows++;
                    try
                    {
                        var row = ws.Row(r);

                        // Extract raw values
                        var rawDict = ExtractRawRow(row, map);
                        // Normalize/convert to target types
                        var (accName, accType, accCode, accCodeAlt, txnDate, voucher, tenant, particulars, debit, credit, balance) =
                            Canonicalize(rawDict);

                        // Quick validity heuristic (customize as needed)
                        bool isValid = txnDate.HasValue || !string.IsNullOrWhiteSpace(voucher) ||
                                       debit.HasValue || credit.HasValue;

                        // Hash for dedupe across files
                        string rowHash = ComputeRowHash(fileId, ws.Name, r, accName, accType, accCode, accCodeAlt, txnDate, voucher, tenant, particulars, debit, credit, balance);

                        int affected = await dal.insertImportRecordError(fileId, ws.Name, sheetIndex, r, accName, accType, accCode, accCodeAlt,
                            txnDate, voucher, tenant, particulars, debit, credit, balance, rowHash, isValid, rawDict);
                        if (affected == 0)
                        {
                            dal.updateImportLedger(fileId, ws.Name, r);
                            duplicates++;
                        }
                        else
                        {
                            inserted++;
                        }
                    }
                    catch (Exception exRow)
                    {
                        await Log($"  Row {r}: ERROR {exRow.Message}");
                        errors++;
                    }

                }
            }

            // Finalize the import_file record
            await dal.UpdateImportFileDoneAsync(fileId, sheetCount: sheetCount, rowCount: totalRows,
            status: "imported", errorMessage: null);

            //dal.commitTransaction();

            await Log($"Done. Inserted: {inserted}, Duplicates: {duplicates}, Errors: {errors}, Total rows seen: {totalRows}");
            Progress.Value = 100;

        }

        public async void updateImportRecordError(long failId, Exception ex)
        {
            dal.updateImportRecordError(failId, ex);
        }
        private Dictionary<string, int> BuildColumnMap(Dictionary<string, int> headerToCol)
        {
            // Normalize expected headers to our internal keys
            // input → internal: account_name, account_type, account_code, account_code_alt, txn_date, voucher_no, tenant_name, particulars, debit, credit, balance
            var map = new Dictionary<string, int>();

            // Helpers
            int find(params string[] keys)
            {
                foreach (var k in keys)
                {
                    if (headerToCol.TryGetValue(k, out var col)) return col;
                }
                return -1;
            }

            // The user-listed headers (case/space/slash-insensitive)
            var code1 = find("accountcode", "account_code", "a/c code", "acccode");
            var code2 = -1;
            if (code1 != -1)
            {
                // try to find another "accountcode" occurrence by scanning raw headers where they appeared twice
                var duplicates = headerToCol.Where(kv => kv.Key == "accountcode").Select(kv => kv.Value).ToList();
                if (duplicates.Count > 1)
                    code2 = duplicates.First(c => c != code1);
            }

            TrySet(map, "account_name", find("nameofaccount", "accountname", "a/c name"));
            TrySet(map, "account_type", find("typeofaccount", "accounttype"));
            TrySet(map, "account_code", code1);
            TrySet(map, "account_code_alt", code2);
            TrySet(map, "txn_date", find("date", "transactiondate", "txn_date"));
            TrySet(map, "voucher_no", find("receipt/voucherno", "receiptnumber", "voucherno", "receiptno"));
            TrySet(map, "tenant_name", find("nameoftenant", "tenantname"));
            TrySet(map, "particulars", find("particulars", "description", "narration"));
            TrySet(map, "debit", find("debit", "dr"));
            TrySet(map, "credit", find("credit", "cr"));
            TrySet(map, "balance", find("balance", "runningbalance"));

            return map;
        }

        private void TrySet(Dictionary<string, int> map, string key, int col)
        {
            if (col > 0) map[key] = col;
        }

        private static string NormalizeHeader(string header)
        {
            if (string.IsNullOrWhiteSpace(header)) return string.Empty;
            var s = header.ToLowerInvariant();
            s = s.Replace(" ", "").Replace("\t", "").Replace("_", "").Replace("-", "");
            s = s.Replace("/", ""); // handle "Receipt / Voucher No"
            s = s.Replace(".", "");
            return s;
        }

        private Dictionary<string, object> ExtractRawRow(IXLRow row, Dictionary<string, int> map)
        {
            var dict = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            foreach (var kv in map)
            {
                var cell = row.Cell(kv.Value);
                dict[kv.Key] = cell?.Value;
            }
            return dict;
        }

        private (string accName, string accType, string accCode, string accCodeAlt,
                        DateTime? txnDate, string voucher, string tenant, string particulars,
                        decimal? debit, decimal? credit, decimal? balance)
            Canonicalize(Dictionary<string, object> raw)
        {
            string S(string key)
            {
                return raw.TryGetValue(key, out var v) ? Convert.ToString(v)?.Trim() : null;
            }

            DateTime? D(string key)
            {
                if (!raw.TryGetValue(key, out var v) || v == null) return null;
                // ClosedXML returns actual DateTime for Excel date cells
                if (v is DateTime dt) return dt.Date;
                var s = Convert.ToString(v)?.Trim();
                if (string.IsNullOrEmpty(s)) return null;
                if (DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out var p)) return p.Date;
                return null;
            }

            decimal? M(string key)
            {
                if (!raw.TryGetValue(key, out var v) || v == null) return null;
                if (v is double d) return Convert.ToDecimal(d);
                var s = Convert.ToString(v)?.Trim();
                if (string.IsNullOrEmpty(s)) return null;
                // remove thousands separators if any
                s = s.Replace(",", "");
                if (decimal.TryParse(s, NumberStyles.Number | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var m))
                    return m;
                return null;
            }

            return (
                accName: S("account_name"),
                accType: S("account_type"),
                accCode: S("account_code"),
                accCodeAlt: S("account_code_alt"),
                txnDate: D("txn_date"),
                voucher: S("voucher_no"),
                tenant: S("tenant_name"),
                particulars: S("particulars"),
                debit: M("debit"),
                credit: M("credit"),
                balance: M("balance")
            );
        }

        private string ComputeRowHash(long fileId, string sheetName, int sheetRow,
    string accName, string accType, string accCode, string accCodeAlt, DateTime? txnDate, string voucher, string tenant, string particulars,
    decimal? debit, decimal? credit, decimal? balance)
        {
            string norm(string s) => (s ?? "").Trim().ToLowerInvariant();
            string num(decimal? d) => d.HasValue ? d.Value.ToString("0.############", CultureInfo.InvariantCulture) : "";
            string date(DateTime? d) => d.HasValue ? d.Value.ToString("yyyy-MM-dd") : "";

            var payload = string.Join("|", new[]
            {
                fileId.ToString(),
                norm(sheetName),
                sheetRow.ToString(),
                norm(accName),
                norm(accType),
                norm(accCode),
                norm(accCodeAlt),
                date(txnDate),
                norm(voucher),
                norm(tenant),
                norm(particulars),
                num(debit),
                num(credit),
                num(balance)
            });

            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(payload));
            var sb = new StringBuilder(bytes.Length * 2);
            foreach (var b in bytes) sb.Append(b.ToString("x2"));
            return sb.ToString();
        }


        public void ImportAccounts(int TrustCode, string FileName, string Years)
        {
            dal.CleanImport();
            //dal.ImportTenant(); //if past excel sheets are imported then tenants should not be updated

            AccountingSheets accSheet = th.GetSheet(TrustCode);
            string cash = th.GetCashAccount(TrustCode);

            accSheet.SetReaderFile(FileName);
            //accSheet.VerifyExcelSheets(); //not necessary that all excel sheets have same sheet names
            //accSheet.SetAccountingSheet(cash);
            List<string> sheets = accSheet.GetAllSheets();
            foreach(string sheet in sheets)
            {
                accSheet.SetAccountingSheet(sheet);
                accSheet.OpenExcelSheet();

                //List<AccountingSheets.XLvalue> allValues = accSheet.ReadWholeXLSheet();
                Dictionary<string, List<Dictionary<string, string>>> XLData = accSheet.ReadWholeXL();
                List<Dictionary<string, string>> xlheader = XLData["Header"];
                List<Dictionary<string, string>> xlrows = XLData["Rows"];
                string accountCode = xlheader[0]["AccountCode"];
                string accountType = xlheader[0]["AccountType"];

                foreach(Dictionary<string, string> row in xlrows)
                {

                }
            }

            //List<Dictionary<string, string>> xlentries = accSheet.ReadAllLines();
            //if (xlentries.Count > 0)
            //{
            /*string date = xlentries.First()["Date"];
            string[] dates = util.GetStartEndDates(date);
            string startdate = dates[0];
            string enddate = dates[1];*/

            //foreach (Dictionary<string, string> entry in xlentries)
            //{
            //Accounts account = GetAccount(entry, cash, TrustCode, Years);
            //ImportAccountsInsert(TrustCode, account);
            //if (th.IsRent(TrustCode, account.ContraAccount))
            //{
            //InsertRent(TrustCode, account, Years);
            //}
            //if (th.IsCertificate(TrustCode, account.ContraAccount))
            //{
            //InsertCertificate(TrustCode, account, Years);
            //}
            //}
            //}

            UpdateAccounts(TrustCode);
            UpdateRent(TrustCode);
            UpdateCertificate(TrustCode);
        }

        public Accounts GetAccount(Dictionary<string, string> entry, string cash, int TrustCode, string Years)
        {
            DateTime startdate = util.GetStartDate(Years);
            DateTime enddate = util.GetEndDate(Years);
            Accounts account = new Accounts();
            account.ContraAccount = entry["Account"];
            account.Credit = entry["Credit"];
            
            account.Date = entry["Date"];
            account.Debit = entry["Debit"];
            account.Name = entry["Name"];
            account.No = entry["No"];
            account.Particulars = entry["Particulars"];
            account.Account = cash;
            account.AccountKey = th.GetAccountKey(TrustCode, account.ContraAccount, account.Particulars);
            account.Order = th.GetAccountOrder(account.Particulars);

            return account;
        }

        public void ImportAccountsInsert(int TrustCode, Accounts entry)
        {
            string account = GetAccountTypeCodesByAccountCode(TrustCode, entry.Account);
            string contraaccount = GetAccountTypeCodesByAccountCode(TrustCode, entry.ContraAccount);
            dal.ImportAccountsInsert(TrustCode.ToString(), account, util.FormatDate(entry.Date), entry.No, entry.Name,
                contraaccount, entry.Particulars, util.RawAmount(entry.Debit), util.RawAmount(entry.Credit), entry.Order, entry.AccountKey);
        }

        public void InsertRent(int TrustCode, Accounts account, string Years)
        {
            //Rent r = th.GetRent(TrustCode, account);
            //Tenant t = th.GetTenant(TrustCode, account);
            Rent r; Tenant t;
            th.GetRent(TrustCode, account, Years, out t, out r);
            int total = 0;

            try
            {
                DataSet ds = dal.GetReceiptNo(TrustCode.ToString(), r.No);
                Rent r2 = Rent.GetRent(ds).FirstOrDefault();
                total = Convert.ToInt32((r.RArears == "0" ? util.RawAmount(r2.RArears) : util.RawAmount(r.RArears)))
                    + Convert.ToInt32((r.WArears == "0" ? util.RawAmount(r2.WArears) : util.RawAmount(r.WArears)))
                    + Convert.ToInt32((r.TotalRent == "0" ? util.RawAmount(r2.TotalRent) : util.RawAmount(r.TotalRent)))
                    + Convert.ToInt32((r.TotalWCharges == "0" ? util.RawAmount(r2.TotalWCharges) : util.RawAmount(r.TotalWCharges)));
                dal.UpdateRent((r.Date == "" ? r2.Date : r.Date), (r.No == "" ? r2.No : r.No), 
                    (r.MRent == "0" ? util.RawAmount(r2.MRent) : util.RawAmount(r.MRent)),
                    (r.WCharges == "0" ? util.RawAmount(r2.WCharges) : util.RawAmount(r.WCharges)), 
                    (r.Name == "" ? r2.Name : r.Name), (r.FDate == "" ? r2.FDate : r.FDate),
                    (r.TDate == "" ? r2.TDate : r.TDate), (r.RArears == "0" ? util.RawAmount(r2.RArears) : util.RawAmount(r.RArears)),
                    (r.WArears == "0" ? util.RawAmount(r2.WArears) : util.RawAmount(r.WArears)),
                    (r.TotalRent == "0" ? util.RawAmount(r2.TotalRent) : util.RawAmount(r.TotalRent)),
                    (r.TotalWCharges == "0" ? util.RawAmount(r2.TotalWCharges) : util.RawAmount(r.TotalWCharges)),
                    total.ToString(), r2.ID, (r.Rpart == "" ? r2.Rpart : r.Rpart),
                    (r.Wpart == "" ? r2.Wpart : r.Wpart), (r.RApart == "" ? r2.RApart : r.RApart),
                    (r.WApart == "" ? r2.WApart : r.WApart));
                return;
            }
            catch (DALException)
            {

            }
            string plotNo = dal.GetPlotNo(account.ContraAccount);
            total = Convert.ToInt32(util.RawAmount(r.RArears)) + Convert.ToInt32(util.RawAmount(r.WArears))
                + Convert.ToInt32(util.RawAmount(r.TotalRent)) + Convert.ToInt32(util.RawAmount(r.TotalWCharges));
            dal.InsertRent(util.FormatDate(r.Date), r.No, TrustCode.ToString(), plotNo, t.Space,
                t.No, util.RawAmount(r.MRent), util.RawAmount(r.WCharges), r.Name, util.FormatDate(r.FDate), util.FormatDate(r.TDate),
                util.RawAmount(r.RArears), util.RawAmount(r.WArears), util.RawAmount(r.TotalRent), util.RawAmount(r.TotalWCharges),
                total.ToString(), r.Rpart, r.Wpart, r.RApart, r.WApart);
            DateTime todate = DateTime.Parse(r.TDate);
            string month = todate.Month.ToString();
            string year = todate.Year.ToString();
            dal.UpdateTenant(r.Name, r.MRent, r.WCharges, month, year, TrustCode.ToString(), plotNo, t.Space, t.No);
        }

        public void InsertCertificate(int TrustCode, Accounts account, string Years)
        {
            Certificate c = th.GetCertificate(TrustCode, account, Years);
            DataSet ds = dal.GetCertificateByNo(TrustCode.ToString(), c.No, c.Folio);
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (row["CertificateNos"].ToString() == c.No || row["CertificateFolioNo"].ToString() == c.Folio)
                    {
                        dal.UpdateCertificate(util.FormatDate(c.PurchaseDate), c.Folio, c.No, util.RawAmount(c.Amount),
                            c.Maturity, row["ID"].ToString(), c.Status, c.SaleDate);
                    }
                }
            }
            else
            {
                dal.InsertCertificate(TrustCode.ToString(), util.FormatDate(c.PurchaseDate), c.Folio, c.No, util.RawAmount(c.Amount),
                    account.ContraAccount, c.Status, c.Maturity, util.FormatDate(c.PurchaseDate), c.SaleDate);
            }
        }
        
        public void UpdateAccounts(int TrustCode)
        {
            List<Accounts> importAccounts = GetAccountsFromTable(dal.GetImportedAccounts());
            foreach(Accounts acc in importAccounts)
            {
                DataSet OriginalAccounts = abl.GetAccountByDetail(TrustCode.ToString(), acc.Account, acc.Date, acc.No, acc.AccountKey);
                if(OriginalAccounts.Tables[0].Rows.Count > 0)
                {
                    abl.AccountsUpdate(acc.Date, acc.No, acc.Name, acc.ContraAccount, acc.Particulars, acc.Debit, acc.Credit, 
                        OriginalAccounts.Tables[0].Rows[0]["ID"].ToString());
                }
                else
                {
                    abl.AccountingEntries(TrustCode, acc.Account, acc.Date, acc.No, acc.Name, acc.ContraAccount, acc.Particulars, 
                        acc.Debit, acc.Credit, acc.Order, acc.AccountKey);
                }
            }
        }

        public void UpdateRent(int TrustCode)
        {
            DataSet importRent = dal.GetImportedRent();
            foreach(DataRow row in importRent.Tables[0].Rows)
            {
                DataSet receipts = rbl.GetReceiptEntry(TrustCode.ToString(), row["No"].ToString());
                if (receipts.Tables[0].Rows.Count > 0)
                {
                    string ID = receipts.Tables[0].Rows[0]["ID"].ToString();
                    //rbl.UpdateTenant(RentTenantName, RentPerMonth, WaterPerMonth, ToM, ToY, RentTrustCode, RentTrustPlotCode, RentSpaceType, RentSpaceNo, CNIC);

                    rbl.UpdateRent(util.FormatDate(row["RentDate"].ToString()), row["RentSerialNo"].ToString(), row["RentPerMonth"].ToString(), 
                        row["RentWaterChargesPerMonth"].ToString(), row["RentTenantName"].ToString(), util.FormatDate(row["RentFromDate"].ToString()), 
                        util.FormatDate(row["RentToDate"].ToString()), row["RentArears"].ToString(), row["RentWaterChargesArears"].ToString(), 
                        row["RentTotalRent"].ToString(), row["RentTotalWaterCharges"].ToString(), row["RentTotalAmount"].ToString(), ID, 
                        row["RentParticulars"].ToString(), row["RentWaterParticulars"].ToString(), row["RentArearsParticulars"].ToString(),
                        row["RentWaterArearsParticulars"].ToString());
                }
                else
                {
                    Rent r = Rent.GetRent(importRent).FirstOrDefault();
                    rbl.InsertRent(r.Date, r.No, TrustCode.ToString(), row["RentTrustPlotCode"].ToString(), row["RentSpaceType"].ToString(), 
                        row["RentSpaceNo"].ToString(), r.MRent, r.WCharges, r.Name, r.FDate, r.TDate, r.RArears, r.WArears, r.TotalRent, 
                        r.TotalWCharges, r.Total, r.Rpart, r.Wpart, r.RApart, r.WApart);
                }
            }
        }

        public void UpdateCertificate(int TrustCode)
        {
            DataSet importCertificates = dal.GetImportedCertificate();
            foreach (DataRow row in importCertificates.Tables[0].Rows)
            {
                List<Certificate> currcerts = cbl.GetCertificatesByFolio(TrustCode, row["CertificateType"].ToString(), row["CertificateFolioNo"].ToString());
                if(currcerts.Count == 0)
                {
                    currcerts = cbl.GetCertificatesByFolio(TrustCode, row["CertificateType"].ToString(), row["CertificateNos"].ToString());
                    if (currcerts.Count == 0)
                    {
                        cbl.InsertCertificate(TrustCode, row["CertificateDate"].ToString(), row["CertificateFolioNo"].ToString(), 
                            row["CertificateNos"].ToString(), row["CertificateAmount"].ToString(), row["CertificateType"].ToString(), 
                            row["CertificateStatus"].ToString(), row["MaturityDate"].ToString(), row["PurchaseDate"].ToString(), row["SaleDate"].ToString());
                    }
                }
                Certificate c = currcerts.FirstOrDefault();
                cbl.UpdateCertificate(c.Date, c.Folio, c.No, c.Amount, c.Maturity, c.ID, c.Status, c.SaleDate);
            }
        }
    }
}
