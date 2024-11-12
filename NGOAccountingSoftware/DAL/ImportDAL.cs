using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TrustApplication
{
    class ImportDAL : DAL
    {
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
