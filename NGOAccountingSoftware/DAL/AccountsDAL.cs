using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TrustApplication.Exceptions;

namespace TrustApplication
{
    class AccountsDAL : DAL
    {
        public List<string> GetTrustAccountNames(string TrustCode)
        {
            //string query = string.Format("SELECT accounttypecode, CONCAT(accountcode, ' - ', AccountName) AS AccountName "
            //+"FROM " + DBName + ".accounttype where TrustCode =  \'{0}\' order by accountcode;", TrustCode);
            string query = string.Format("SELECT accountcode "
                + "FROM `" + DBName + "`.accounttype where TrustCode =  \'{0}\' order by accountcode;", TrustCode);
            List<string> list = ExecuteQueryGetRows(query);
            if (list.Count <= 0)
                throw new AccountsDALException("Failed to get account names");
            return list;
        }


        public void AccountsUpdate(string AccountsDate, string ReceiptNo, string PartyName, string AccountCode, string Particulars, string Debit,
            string Credit, string ID)
        {
            string query = string.Format("UPDATE `" + DBName + "`.`accounts` set `AccountsDate` = \'{0}\',`ReceiptNo` = \'{1}\',`PartyName` = \'{2}\',"
                + "`ContraAccountTypeCode` = \'{3}\',`Particulars` = \'{4}\',`Debit` = \'{5}\',`Credit` = \'{6}\' WHERE `ID` = \'{7}\';"
                , AccountsDate, ReceiptNo, PartyName, AccountCode, Particulars, Debit, Credit, ID);

            ExecuteNonQuery(query);
        }

        public List<string> GetContraAccountTypeCodes(string TrustCode, string AccountCode)
        {
            string query = string.Format("select at.accounttypecode, at.accountcode from `" + DBName + "`.contraaccounts ca, " + DBName + ".accounttype at "
                + "where ca.trustcode = at.trustcode and ca.contraaccountcode = at.accounttypecode and ca.trustcode = \'{0}\' and ca.accountcode = \'{1}\';",
                TrustCode, AccountCode);
            return ExecuteQueryGetRows(query);
        }

        public Dictionary<string, string> GetContraAccountTypeCodesByAccountTypeCode(string TrustCode, string AccountCode)
        {
            string query = string.Format("select at.accounttypecode, at.accountcode from `" + DBName + "`.`contraaccounts` ca, `" + DBName + "`.`accounttype` at "
                + "where ca.trustcode = at.trustcode and ca.contraaccountcode = at.accounttypecode and ca.trustcode = \'{0}\' and ca.accountcode = \'{1}\';",
                TrustCode, AccountCode);

            return ExecuteQueryGetDictionary(query);
        }

        public Dictionary<string, string> GetAccountsByAccountType(string TrustCode, string Type)
        {
            string query = string.Format("select accounttypecode, accountname  from `" + DBName + "`.`accounttype` "
                + "where TrustCode = \'{0}\' and accounttype = \'{1}\';", TrustCode, Type);

            return ExecuteQueryGetDictionary(query);
        }

        public Dictionary<string, string> GetAccountsByTrust(string TrustCode)
        {
            string query = string.Format("select accounttypecode, accountname  from `" + DBName + "`.`accounttype` "
                + "where TrustCode = \'{0}\' order by accountname;", TrustCode);

            return ExecuteQueryGetDictionary(query);
        }

        public List<string> GetAccountTypes()
        {
            string query = string.Format("select distinct accounttype from `" + DBName + "`.`accounttype`;");
            return ExecuteQueryGetRows(query);
        }

        public List<string> GetMaxAccountTypeCode(string TrustCode)
        {
            string query = string.Format("select max(convert(AccountTypeCode, unsigned integer)) from `" + DBName + "`.`accounttype` where trustcode = \'{0}\';",
                TrustCode);
            return ExecuteQueryGetRows(query);
        }

        public void InsertAccountType(string TrustCode, string AccountType, string AccountTypeCode, string AccountCode, string AccountName)
        {
            string query = string.Format("INSERT INTO " + DBName + ".`accounttype`(`TrustCode`,`AccountTypeCode`,`AccountCode`,`AccountType`,`AccountName`)"
                + "VALUES(\'{0}\', \'{1}\', \'{2}\', \'{3}\', \'{4}\'); ", TrustCode, AccountTypeCode, AccountCode, AccountType, AccountName);
            ExecuteNonQuery(query);
        }

        public DataSet GetAccountByDetail(string TrustCode, string AccountTypeCode, string AccountsDate, string ReceiptNo, string AccountKey)
        {
            string query = string.Format("select * from `" + DBName + "`.`accounts` where TrustCode = \'{0}\' and AccountTypeCode = \'{1}\'" 
                + " and AccountsDate = \'{2}\' and ReceiptNo = \'{3}\' and AccountKey = \'{4}\'", TrustCode, AccountTypeCode, AccountsDate, 
                ReceiptNo, AccountKey);
            return ExecuteQueryGetTable(query);
        }
    }
}
