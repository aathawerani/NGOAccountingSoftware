using DocumentFormat.OpenXml.Wordprocessing;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TrustApplication
{
    class ImportDAL : DAL
    {
        private MySqlConnection importConnection;
        private MySqlTransaction importTransaction;

        public async void beginTransaction()
        {

            importConnection = dbCon.Connection;
            await importConnection.OpenAsync();
            importTransaction = await dbCon.Connection.BeginTransactionAsync();
        }
        public async void commitTransaction()
        {
            await importTransaction.CommitAsync();
        }

        public async void updateImportLedger(long fileId, string wsName, int r)
        {
            using var conn = dbCon.Connection;
            await conn.OpenAsync();
            // Duplicate by (file_id, sheet_name, sheet_row) → flag as duplicate
            using var upd = new MySqlCommand(@"
                                        UPDATE import_ledger_row
                                        SET process_status='duplicate', updated_at=CURRENT_TIMESTAMP
                                        WHERE file_id=@fid AND sheet_name=@sn AND sheet_row=@sr;", conn /*importConnection, importTransaction*/);
            upd.Parameters.AddWithValue("@fid", fileId);
            upd.Parameters.AddWithValue("@sn", wsName);
            upd.Parameters.AddWithValue("@sr", r);
            await upd.ExecuteNonQueryAsync();
        }

        public async Task<int> insertImportRecordError(long fileId, string wsName, int sheetIndex, int r, string accName, string accType, 
            string accCode, string accCodeAlt, DateTime? txnDate, string voucher, string tenant, string particulars, decimal? debit, 
            decimal? credit, decimal? balance, string rowHash, bool isValid, Dictionary<string, object> rawDict)
        {
            using var conn = dbCon.Connection;
            await conn.OpenAsync();
            // Prepare reusable INSERT command (INSERT IGNORE to honor uq_file_sheet_row)
            using var cmd = new MySqlCommand(@"
                            INSERT IGNORE INTO import_ledger_row
                            (file_id, sheet_name, sheet_index, sheet_row,
                             account_name, account_type, account_code, account_code_alt,
                             txn_date, voucher_no, tenant_name, particulars,
                             debit, credit, balance,
                             currency, row_hash, is_processed, process_status, processed_at, processor_job_id, process_note,
                             is_valid, validation_errors, raw_row, ingested_at, updated_at)
                            VALUES
                            (@file_id, @sheet_name, @sheet_index, @sheet_row,
                             @account_name, @account_type, @account_code, @account_code_alt,
                             @txn_date, @voucher_no, @tenant_name, @particulars,
                             @debit, @credit, @balance,
                             @currency, @row_hash, @is_processed, @process_status, @processed_at, @processor_job_id, @process_note,
                             @is_valid, @validation_errors, @raw_row, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
                            ", conn/*importConnection, (MySqlTransaction)importTransaction*/);

            cmd.Parameters.Add("@file_id", MySqlDbType.UInt64);
            cmd.Parameters.Add("@sheet_name", MySqlDbType.VarChar);
            cmd.Parameters.Add("@sheet_index", MySqlDbType.UInt32);
            cmd.Parameters.Add("@sheet_row", MySqlDbType.UInt32);
            cmd.Parameters.Add("@account_name", MySqlDbType.VarChar);
            // Replace all instances of MySqlDbType.Bool with MySqlDbType.Bit

            cmd.Parameters.Add("@account_type", MySqlDbType.VarChar);
            cmd.Parameters.Add("@account_code", MySqlDbType.VarChar);
            cmd.Parameters.Add("@account_code_alt", MySqlDbType.VarChar);
            cmd.Parameters.Add("@txn_date", MySqlDbType.Date);
            cmd.Parameters.Add("@voucher_no", MySqlDbType.VarChar);
            cmd.Parameters.Add("@tenant_name", MySqlDbType.VarChar);
            cmd.Parameters.Add("@particulars", MySqlDbType.Text);
            cmd.Parameters.Add("@debit", MySqlDbType.NewDecimal);
            cmd.Parameters.Add("@credit", MySqlDbType.NewDecimal);
            cmd.Parameters.Add("@balance", MySqlDbType.NewDecimal);
            cmd.Parameters.Add("@currency", MySqlDbType.VarChar);
            cmd.Parameters.Add("@row_hash", MySqlDbType.VarChar);
            cmd.Parameters.Add("@is_processed", MySqlDbType.Bit);
            cmd.Parameters.Add("@process_status", MySqlDbType.VarChar);
            cmd.Parameters.Add("@processed_at", MySqlDbType.DateTime);
            cmd.Parameters.Add("@processor_job_id", MySqlDbType.VarChar);
            cmd.Parameters.Add("@process_note", MySqlDbType.Text);
            cmd.Parameters.Add("@is_valid", MySqlDbType.Bit);
            cmd.Parameters.Add("@validation_errors", MySqlDbType.JSON);
            cmd.Parameters.Add("@raw_row", MySqlDbType.JSON);

            // Static params
            cmd.Parameters["@file_id"].Value = fileId;
            cmd.Parameters["@sheet_name"].Value = wsName;
            cmd.Parameters["@sheet_index"].Value = sheetIndex;

            // Fill params
            cmd.Parameters["@sheet_row"].Value = r;
            cmd.Parameters["@account_name"].Value = (object?)accName ?? DBNull.Value;
            cmd.Parameters["@account_type"].Value = (object?)accType ?? DBNull.Value;
            cmd.Parameters["@account_code"].Value = (object?)accCode ?? DBNull.Value;
            cmd.Parameters["@account_code_alt"].Value = (object?)accCodeAlt ?? DBNull.Value;
            cmd.Parameters["@txn_date"].Value = (object?)txnDate ?? DBNull.Value;
            cmd.Parameters["@voucher_no"].Value = (object?)voucher ?? DBNull.Value;
            cmd.Parameters["@tenant_name"].Value = (object?)tenant ?? DBNull.Value;
            cmd.Parameters["@particulars"].Value = (object?)particulars ?? DBNull.Value;
            cmd.Parameters["@debit"].Value = (object?)debit ?? DBNull.Value;
            cmd.Parameters["@credit"].Value = (object?)credit ?? DBNull.Value;
            cmd.Parameters["@balance"].Value = (object?)balance ?? DBNull.Value;

            cmd.Parameters["@currency"].Value = DBNull.Value;              // set if present
            cmd.Parameters["@row_hash"].Value = rowHash;
            cmd.Parameters["@is_processed"].Value = false;                  // not processed yet
            cmd.Parameters["@process_status"].Value = "pending";
            cmd.Parameters["@processed_at"].Value = DBNull.Value;
            cmd.Parameters["@processor_job_id"].Value = DBNull.Value;
            cmd.Parameters["@process_note"].Value = DBNull.Value;
            cmd.Parameters["@is_valid"].Value = isValid;
            cmd.Parameters["@validation_errors"].Value = DBNull.Value;     // fill with JSON if you collect issues
            cmd.Parameters["@raw_row"].Value = JsonSerializer.Serialize(rawDict);

            var affected = await cmd.ExecuteNonQueryAsync();
            return affected;
        }
        public async void updateImportRecordError(long failId, Exception ex)
        {
            using var conn = dbCon.Connection;
            await conn.OpenAsync();
            using var cmd = new MySqlCommand(@"
                                UPDATE import_file
                                SET import_status='failed', error_message=LEFT(CONCAT(IFNULL(error_message,''), '\n', @msg), 65535),
                                    import_completed_at=CURRENT_TIMESTAMP, updated_at=CURRENT_TIMESTAMP
                                WHERE id=@id;", conn);
            cmd.Parameters.AddWithValue("@id", failId);
            cmd.Parameters.AddWithValue("@msg", ex.ToString());
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<long> InsertImportFileAsync(string userGivenName, string originalFilename, string storagePath,
            string mimeType, long fileSizeBytes)
        {
            using var conn = dbCon.Connection;
            await conn.OpenAsync();
            using var cmd = new MySqlCommand(@"
                INSERT INTO import_file
                (user_given_name, original_filename, storage_path, mime_type, file_size_bytes,
                    uploaded_at, import_status, import_started_at,
                    sheet_count, row_count, error_message, created_at, updated_at)
                VALUES
                (@user_given_name, @original_filename, @storage_path, @mime_type, @file_size_bytes,
                    CURRENT_TIMESTAMP, 'parsing', CURRENT_TIMESTAMP,
                    NULL, NULL, NULL, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
                SELECT LAST_INSERT_ID();", conn);

            cmd.Parameters.AddWithValue("@user_given_name", userGivenName);
            cmd.Parameters.AddWithValue("@original_filename", originalFilename);
            cmd.Parameters.AddWithValue("@storage_path", storagePath);
            cmd.Parameters.AddWithValue("@mime_type", mimeType);
            cmd.Parameters.AddWithValue("@file_size_bytes", fileSizeBytes);

            var id = await cmd.ExecuteScalarAsync();
            return Convert.ToInt64(id);
        }

        public async Task UpdateImportFileDoneAsync(
            long fileId, int sheetCount, long rowCount, string status, string errorMessage)
        {
            using var conn = dbCon.Connection;
            await conn.OpenAsync();
            using var cmd = new MySqlCommand(@"
                UPDATE import_file
                SET import_status=@status,
                    import_completed_at=CURRENT_TIMESTAMP,
                    sheet_count=@sheet_count,
                    row_count=@row_count,
                    error_message=@err,
                    updated_at=CURRENT_TIMESTAMP
                WHERE id=@id;", conn/*importConnection, importTransaction*/);

            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@sheet_count", sheetCount);
            cmd.Parameters.AddWithValue("@row_count", rowCount);
            cmd.Parameters.AddWithValue("@err", (object?)errorMessage ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@id", fileId);

            await cmd.ExecuteNonQueryAsync();
        }

        public void CleanImport()
        {
            string query = string.Format("truncate table `" + DBName + "`.`import_accounts`;");
            ExecuteNonQuery(query);
        }

        public void ImportTenant()
        {
            string query = string.Format("INSERT INTO `" + DBName + "`.`import_tenant`(`TenantName`, `TenantTrustCode`, `TenantTrustPlotCode`, "
                + "`TenantSpaceType`, `TenantSpaceNo`, `TenantRentPerMonth`, `TenantWaterChargesPerMonth`)"
                + "SELECT `tenant`.`TenantName`, `tenant`.`TenantTrustCode`, `tenant`.`TenantTrustPlotCode`, `tenant`.`TenantSpaceType`, "
                + "`tenant`.`TenantSpaceNo`, `tenant`.`TenantRentPerMonth`, `tenant`.`TenantWaterChargesPerMonth`  FROM `"
                + DBName + "`.`tenant`; ");
            
            ExecuteNonQuery(query);
        }

        public void InsertRent(string RentDate, string RentSerialNo, string RentTrustCode, string RentTrustPlotCode, string RentSpaceType, string RentSpaceNo
            , string RentPerMonth, string RentWaterChargesPerMonth, string RentTenantName, string RentFromDate, string RentToDate, string RentArears
            , string RentWaterChargesArears, string RentTotalRent, string RentTotalWaterCharges, string RentTotalAmount, string RentParticulars,
            string WaterParticulars, string RentArearsParticulars, string WaterArearsParticulars)
        {
            string query = string.Format("INSERT INTO `" + DBName + "`.`import_rent` (`RentDate`,`RentSerialNo`,`RentTrustCode`,`RentTrustPlotCode`,`RentSpaceType`,"
                + "`RentSpaceNo`,`RentPerMonth`,`RentWaterChargesPerMonth`,`RentTenantName`,`RentFromDate`,`RentToDate`,`RentArears`,"
                + "`RentWaterChargesArears`,`RentTotalRent`,`RentTotalWaterCharges`,`RentTotalAmount`,`RentParticulars`,`RentWaterParticulars`,`RentArearsParticulars`,"
                + "`RentWaterArearsParticulars`)"
                + "VALUES (\'{0}\',\'{1}\',\'{2}\',\'{3}\',\'{4}\',\'{5}\',\'{6}\',\'{7}\',\'{8}\',"
                + "\'{9}\',\'{10}\',\'{11}\',\'{12}\',\'{13}\',\'{14}\',\'{15}\',\'{16}\',"
                + "\'{17}\',\'{18}\',\'{19}\');"
                , RentDate, RentSerialNo, RentTrustCode, RentTrustPlotCode, RentSpaceType, RentSpaceNo, RentPerMonth, RentWaterChargesPerMonth, RentTenantName
                , RentFromDate, RentToDate, RentArears, RentWaterChargesArears, RentTotalRent, RentTotalWaterCharges, RentTotalAmount, RentParticulars,
                WaterParticulars, RentArearsParticulars, WaterArearsParticulars);
            ExecuteNonQuery(query);
        }

        public void UpdateTenant(string TenantName, string TenantRentPerMonth, string TenantWaterChargesPerMonth, string TenantLastPaidMonth
            , string TenantLastPaidyear, string TenantTrustCode, string TenantTrustPlotCode, string TenantSpaceType, string TenantSpaceNo)
        {
            string query = string.Format("UPDATE `" + DBName + "`.`import_tenant` SET `TenantName` = \'{0}\', `TenantRentPerMonth` = \'{1}\', "
                + "`TenantWaterChargesPerMonth` = \'{2}\', `TenantLastPaidMonth` = \'{3}\', `TenantLastPaidyear` = \'{4}\' WHERE "
                + "`TenantTrustCode` = \'{5}\' and `TenantTrustPlotCode` = \'{6}\' and `TenantSpaceType` = \'{7}\' and `TenantSpaceNo` = \'{8}\';",
                TenantName, TenantRentPerMonth, TenantWaterChargesPerMonth, TenantLastPaidMonth, TenantLastPaidyear, TenantTrustCode, TenantTrustPlotCode,
            TenantSpaceType, TenantSpaceNo);
            ExecuteNonQuery(query);
        }

        public string GetPlotNo(string account)
        {
            string query = "select PlotNo from plot p, plotaccounts pa, accounttype at where at.AccountCode = '" + account 
                + "' and p.plotcode = pa.PlotCode and p.trustcode = pa.trustcode and p.trustcode = at.trustcode and at.accounttypecode = pa.accountcode;";
            return ExecuteQueryGetColumns(query).FirstOrDefault();
        }

        public void InsertCertificate(string TrustCode, string CertificateDate, string FolioNo, string CertificateNo, string Amount, string Type, 
            string Status, string Maturity, string PurchaseDate, string SaleDate)
        {
            string query = string.Format("INSERT INTO `" + DBName + "`.`import_certificates`(`TrustCode`, `CertificateDate`, `CertificateNos`, `CertificateFolioNo`, "
                + "`CertificateType`, `CertificateAmount`, `CertificateStatus`, `MaturityDate`, `PurchaseDate`, `SaleDate`) VALUES (\'{0}\', \'{1}\', \'{2}\', \'{3}\', \'{4}\', \'{5}\', "
                + "\'{6}\', \'{7}\', \'{8}\', \'{9}\');", TrustCode, CertificateDate, CertificateNo, FolioNo, Type, Amount, Status, Maturity, PurchaseDate, SaleDate);
            ExecuteNonQuery(query);
        }

        public void ImportAccountsInsert(string TrustCode, string AccountTypeCode, string AccountsDate, string ReceiptNo, string PartyName, 
            string AccountCode, string Particulars, string Debit, string Credit, string roworder, string AccountKey)
        {
            string query = string.Format("INSERT INTO `" + DBName + "`.`import_accounts`(`TrustCode`,`AccountTypeCode`,`AccountsDate`,`ReceiptNo`,`PartyName`,`ContraAccountTypeCode`,"
                + "`Particulars`,`Debit`,`Credit`, `roworder`, `AccountKey`)"
                + "VALUES(\'{0}\',\'{1}\',\'{2}\',\'{3}\',\'{4}\',\'{5}\',\'{6}\',\'{7}\',\'{8}\',\'{9}\',\'{10}\'); "
                , TrustCode, AccountTypeCode, AccountsDate, ReceiptNo, PartyName, AccountCode, Particulars, Debit, Credit, roworder, AccountKey);

            ExecuteNonQuery(query);
        }

        public void UpdateCertificate(string Date, string FolioNo, string CertificateNo, string Amount, string Maturity, string ID, string Status, 
            string SaleDate)
        {
            string query = string.Format("UPDATE `" + DBName + "`.`import_certificates` SET `CertificateDate` = \'{0}\', `CertificateNos` = \'{1}\', "
                + "`CertificateFolioNo` = \'{2}\', `CertificateAmount` = \'{3}\', `MaturityDate` = \'{4}\', `CertificateStatus` = \'{6}\',"
                + " `SaleDate` = \'{7}\' WHERE `ID` = \'{5}\'; ", Date, CertificateNo, FolioNo, Amount, Maturity, ID, Status, SaleDate);
            ExecuteNonQuery(query);
        }

        public DataSet GetCertificateByNo(string TrustCode, string CertificateNo, string FolioNo)
        {
            string query = string.Format("select * from " + DBName + ".import_certificates where CertificateNos like \'%{0}%\' or CertificateFolioNo like \'%{1}%\' ",
                TrustCode, CertificateNo, FolioNo);
            return ExecuteQueryGetTable(query);
        }

        public DataSet GetImportedAccounts()
        {
            string query = string.Format("select * from " + DBName + ".import_accounts ;");
            return ExecuteQueryGetTable(query);
        }

        public DataSet GetImportedRent()
        {
            string query = string.Format("select * from " + DBName + ".import_rent ;");
            return ExecuteQueryGetTable(query);
        }

        public DataSet GetImportedCertificate()
        {
            string query = string.Format("select * from " + DBName + ".import_certificates ;");
            return ExecuteQueryGetTable(query);
        }

        public DataSet GetReceiptNo(string TrustCode, string ReceiptNo)
        {
            string query = string.Format("select * from " + DBName + ".import_rent where RentTrustCode = \'{0}\' and RentSerialNo = \'{1}\' ", TrustCode, ReceiptNo);
            return ExecuteQueryGetTable(query);
        }

        public void UpdateRent(string RentDate, string RentSerialNo, string RentPerMonth, string RentWaterChargesPerMonth, string RentTenantName,
            string RentFromDate, string RentToDate, string RentArears, string RentWaterChargesArears, string RentTotalRent, string RentTotalWaterCharges,
            string RentTotalAmount, string ID, string RentParticulars, string WaterParticulars, string RentArearsParticulars, string WaterArearsParticulars)
        {
            string query = string.Format("UPDATE " + DBName + ".import_rent SET `RentDate` = \'{0}\',`RentSerialNo` = \'{1}\',`RentPerMonth` = \'{2}\',"
                + "`RentWaterChargesPerMonth` = \'{3}\',`RentTenantName` = \'{4}\',`RentFromDate` = \'{5}\',`RentToDate` = \'{6}\',`RentArears` = \'{7}\',"
                + "`RentWaterChargesArears` = \'{8}\',`RentTotalRent` = \'{9}\',`RentTotalWaterCharges` = \'{10}\',`RentTotalAmount` = \'{11}\'"
                + ",`RentParticulars` = \'{13}\',`RentArearsParticulars` = \'{14}\',`RentWaterParticulars` = \'{15}\',`RentWaterArearsParticulars` = \'{16}\'"
                + "where `ID` = \'{12}\';", RentDate, RentSerialNo, RentPerMonth, RentWaterChargesPerMonth, RentTenantName, RentFromDate,
                RentToDate, RentArears, RentWaterChargesArears, RentTotalRent, RentTotalWaterCharges, RentTotalAmount, ID, RentParticulars,
                RentArearsParticulars, WaterParticulars, WaterArearsParticulars);
            ExecuteNonQuery(query);
        }

    }
}
