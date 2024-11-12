using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TrustApplication.Exceptions;
using DocumentFormat.OpenXml.Spreadsheet;
using ClosedXML.Excel;

namespace TrustApplication
{
    class ImportBL : BL
    {
        ImportDAL dal = new ImportDAL();
        RentBL rbl = new RentBL();
        CertificateBL cbl = new CertificateBL();
        AccountsBL abl = new AccountsBL();
        Utils util = new Utils();

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
