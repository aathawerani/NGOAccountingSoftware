using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace TrustApplication
{
    class RentDAL : DAL
    {
        public List<string> GetPlots(string TrustCode)
        {
            string query = string.Format("SELECT PlotNo FROM " + DBName + ".plot where TrustCode = \'{0}\' order by PlotCode;", TrustCode);
            return ExecuteQueryGetRows(query);
        }
        public List<string> GetPlotNoByPlotCode(string TrustCode, string PlotCode)
        {
            string query = string.Format("SELECT PlotNo FROM " + DBName
                + ".plot where TrustCode = \'{0}\' and PlotCode = \'{1}\';", TrustCode, PlotCode);
            return ExecuteQueryGetRows(query);
        }
        public List<string> GetSpaceType(string TrustCode, string PlotCode)
        {
            string query = string.Format("SELECT distinct TenantSpaceType FROM " + DBName + ".tenant where tenantTrustCode = \'{0}\' and tenanttrustplotcode = \'{1}\';",
                TrustCode, PlotCode);
            return ExecuteQueryGetRows(query);
        }
        public List<string> GetSpaceNo(string TrustCode, string PlotCode, string SpaceType)
        {
            string query = string.Format("SELECT tenantspaceno FROM " + DBName + ".tenant where tenantTrustCode = \'{0}\' and tenanttrustplotcode = \'{1}\' "
                + "and tenantspacetype = \'{2}\' order by cast(tenantspaceno as unsigned);", TrustCode, PlotCode, SpaceType);
            return ExecuteQueryGetRows(query);
        }
        public List<string> GetTenantInfo(string TrustCode, string PlotCode, string SpaceType, string SpaceNo)
        {
            string query = string.Format("select * from " + DBName + ".tenant where tenantTrustCode = \'{0}\' and tenanttrustplotcode = \'{1}\' "
                + "and tenantspacetype = \'{2}\' and tenantspaceno = \'{3}\';", TrustCode, PlotCode, SpaceType, SpaceNo);
            return ExecuteQueryGetColumns(query);
        }
        public List<string> GetTenantInfo(string TrustCode, string PlotCode)
        {
            string query = string.Format("select * from " + DBName + ".tenant where tenantTrustCode = \'{0}\' and tenanttrustplotcode = \'{1}\' ;", TrustCode, PlotCode);
            return ExecuteQueryGetColumns(query);
        }
        public List<string> GetReceiptNo(string TrustCode, string ReceiptNo)
        {
            string query = string.Format("select * from " + DBName + ".rent where RentTrustCode = \'{0}\' and RentSerialNo = \'{1}\' ", TrustCode, ReceiptNo);
            return ExecuteQueryGetColumns(query);
        }
        public DataSet GetReceiptEntry(string TrustCode, string ReceiptNo)
        {
            string query = string.Format("select * from " + DBName + ".rent where RentTrustCode = \'{0}\' and RentSerialNo = \'{1}\' ", TrustCode, ReceiptNo);
            return ExecuteQueryGetTable(query);
        }

        public List<string> GetLastReceiptNo(string TrustCode)
        {
            string query = string.Format("select RentSerialNo from " + DBName + ".rent where RentTrustCode = \'{0}\' order by STR_TO_DATE(rentdate, '%d-%m-%Y') desc, ID desc limit 1", TrustCode);
            return ExecuteQueryGetColumns(query);
        }

        public void InsertRent(string RentDate, string RentSerialNo, string RentTrustCode, string RentTrustPlotCode, string RentSpaceType, string RentSpaceNo
            , string RentPerMonth, string RentWaterChargesPerMonth, string RentTenantName, string RentFromDate, string RentToDate, string RentArears
            , string RentWaterChargesArears, string RentTotalRent, string RentTotalWaterCharges, string RentTotalAmount, string RentParticulars, 
            string WaterParticulars, string RentArearsParticulars, string WaterArearsParticulars)
        {
            string query = string.Format("INSERT INTO `" + DBName + "`.`rent` (`RentDate`,`RentSerialNo`,`RentTrustCode`,`RentTrustPlotCode`,`RentSpaceType`,"
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
            , string TenantLastPaidyear, string TenantTrustCode, string TenantTrustPlotCode, string TenantSpaceType, string TenantSpaceNo, string CNIC)
        {
            string query = string.Format("UPDATE `" + DBName + "`.`tenant` SET `TenantName` = \'{0}\', `TenantRentPerMonth` = \'{1}\', "
                + "`TenantWaterChargesPerMonth` = \'{2}\', `TenantLastPaidMonth` = \'{3}\', `TenantLastPaidyear` = \'{4}\', `CNIC` = \'{9}\' WHERE "
                + "`TenantTrustCode` = \'{5}\' and `TenantTrustPlotCode` = \'{6}\' and `TenantSpaceType` = \'{7}\' and `TenantSpaceNo` = \'{8}\';",
                TenantName, TenantRentPerMonth, TenantWaterChargesPerMonth, TenantLastPaidMonth, TenantLastPaidyear, TenantTrustCode, TenantTrustPlotCode,
            TenantSpaceType, TenantSpaceNo, CNIC);
            ExecuteNonQuery(query);
        }

        public DataSet GetRent(string TrustCode, string PlotCode, string SpaceType, string SpaceNo)
        {
            string query = string.Format("select * from (SELECT * FROM " + DBName + ".rent "
                + "where RentTrustCode = \'{0}\' and RentTrustPlotCode = \'{1}\' and RentSpaceType = \'{2}\' and RentSpaceNo = \'{3}\'"
                + " order by STR_TO_DATE(RentDate, '%d-%m-%Y') desc) rent order by STR_TO_DATE(RentDate, '%d-%m-%Y');",
                TrustCode, PlotCode, SpaceType, SpaceNo);
            return ExecuteQueryGetTable(query);
        }

        public void UpdateRent(string RentDate, string RentSerialNo, string RentPerMonth, string RentWaterChargesPerMonth, string RentTenantName,
            string RentFromDate, string RentToDate, string RentArears, string RentWaterChargesArears, string RentTotalRent, string RentTotalWaterCharges,
            string RentTotalAmount, string ID, string RentParticulars, string WaterParticulars, string RentArearsParticulars, string WaterArearsParticulars)
        {
            string query = string.Format("UPDATE " + DBName + ".rent SET `RentDate` = \'{0}\',`RentSerialNo` = \'{1}\',`RentPerMonth` = \'{2}\',"
                + "`RentWaterChargesPerMonth` = \'{3}\',`RentTenantName` = \'{4}\',`RentFromDate` = \'{5}\',`RentToDate` = \'{6}\',`RentArears` = \'{7}\',"
                + "`RentWaterChargesArears` = \'{8}\',`RentTotalRent` = \'{9}\',`RentTotalWaterCharges` = \'{10}\',`RentTotalAmount` = \'{11}\'"
                + ",`RentParticulars` = \'{13}\',`RentArearsParticulars` = \'{14}\',`RentWaterParticulars` = \'{15}\',`RentWaterArearsParticulars` = \'{16}\'"
                + "where `ID` = \'{12}\';", RentDate, RentSerialNo, RentPerMonth, RentWaterChargesPerMonth, RentTenantName, RentFromDate,
                RentToDate, RentArears, RentWaterChargesArears, RentTotalRent, RentTotalWaterCharges, RentTotalAmount, ID, RentParticulars,
                RentArearsParticulars, WaterParticulars, WaterArearsParticulars);
            ExecuteNonQuery(query);
        }
        public DataSet GetTenants(string TrustCode, string PlotCode)
        {
            string query = string.Format("select * from " + DBName + ".tenant where tenantTrustCode = \'{0}\' and tenanttrustplotcode = \'{1}\' ;", TrustCode, PlotCode);
            return ExecuteQueryGetTable(query);
        }
        public DataSet GetPlotRentAccountCodes(string TrustCode, string PlotNo)
        {
            string query = string.Format("select at.accounttypecode, at.accountcode, at.accountname "
                + "from " + DBName + ".plotaccounts pa, " + DBName + ".accounttype at, " + DBName + ".plot p where "
                + "pa.accountcode = at.accounttypecode and p.plotcode = pa.plotcode "
                + " and pa.trustcode = p.trustcode and pa.trustcode = at.trustcode and pa.trustcode = \'{0}\' "
                + "and pa.plotcode = \'{1}\'; ", TrustCode, PlotNo);
            return ExecuteQueryGetTable(query);
        }
        public DataSet GetAccountsByRentReceipt(string TrustCode, string ReceiptNo)
        {
            string query = string.Format("SELECT ID, AccountsDate as Date, ReceiptNo, PartyName as Name, "
                + "(select accountcode from accounttype where accounttypecode = a.contraaccounttypecode and trustcode = \'{0}\') as ContraAccount, "
                + "(select accountcode from accounttype where accounttypecode = a.accounttypecode and trustcode = \'{0}\') as Account, "
                //+ "accounttypecode as Account, "
                + "Particulars, Debit, Credit, 0 cumulative_sum, AccountKey FROM " + DBName + ".accounts a where TrustCode =  \'{0}\' and receiptno = \'{1}\';",
                TrustCode, ReceiptNo);
            return ExecuteQueryGetTable(query);
        }
    }
}
