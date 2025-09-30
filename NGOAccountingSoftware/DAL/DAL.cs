using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using TrustApplication.Exceptions;

namespace TrustApplication
{
    class DAL : IDisposable
    {
        protected DBConnection dbCon = DBConnection.Instance();
        protected string DBName = "trustdb";

        public DAL()
        {
            dbCon.DatabaseName = DBName;
        }

        public void Dispose()
        {
            dbCon.Close();
        }

        #region General

        protected DataSet ExecuteQueryGetTable(string query)
        {
            DataSet Result = new DataSet();
            dbCon.Connect();
            dbCon.Open();
            var cmd = new MySqlCommand(query, dbCon.Connection);
            var adapter = new MySqlDataAdapter(cmd);
            int num = adapter.Fill(Result);
            dbCon.Close();
            if (num == 0)
            {
                throw new DALException("Filled 0 records");
            }
            return Result;
        }
        protected List<string> ExecuteQueryGetRows(string query)
        {
            List<string> Result = new List<string>();
            dbCon.Connect();
            dbCon.Open();
            var cmd = new MySqlCommand(query, dbCon.Connection);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Result.Add(reader.GetString(0));
            }
            dbCon.Close();
            if (Result.Count == 0)
            {
                throw new DALException("Filled 0 records");
            }
            return Result;
        }
        protected List<string> ExecuteQueryGetColumns(string query)
        {
            List<string> Result = new List<string>();
            dbCon.Connect();
            dbCon.Open();
            var cmd = new MySqlCommand(query, dbCon.Connection);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Result.Add(reader.GetString(i));
                }
            }
            dbCon.Close();
            if (Result.Count == 0)
            {
                throw new DALException("Filled 0 records");
            }
            return Result;
        }
        protected Dictionary<string, string> ExecuteQueryGetDictionary(string query)
        {
            Dictionary<string, string> Result = new Dictionary<string, string>();
            dbCon.Connect();
            dbCon.Open();
            var cmd = new MySqlCommand(query, dbCon.Connection);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Result.Add(reader.GetString(0), reader.GetString(1));
            }
            dbCon.Close();
            if (Result.Count == 0)
            {
                throw new DALException("Filled 0 records");
            }
            return Result;
        }
        protected void ExecuteNonQuery(string query)
        {
            dbCon.Connect();
            dbCon.Open();
            var cmd = new MySqlCommand(query, dbCon.Connection);
            if(cmd.ExecuteNonQuery() < 0)
                throw new DALException("Query execution failed");
            dbCon.Close();
        }

        #endregion General

        public List<string> GetTrustNames()
        {
            string query = "SELECT trustname FROM " + DBName + ".trust order by TrustCode;";
            return ExecuteQueryGetRows(query);
        }

        public List<string> GetAccountYears(string AccountTypeCode)
        {
            string query = string.Format("select distinct Year(STR_TO_DATE(AccountsDate, '%d-%m-%Y')) from " + DBName
                + ".accounts where accounttypecode = \'{0}\' "
                + "order by Year(STR_TO_DATE(AccountsDate, '%d-%m-%Y'));", AccountTypeCode);
            return ExecuteQueryGetRows(query);
        }

        public void AccountsInsert(string TrustCode, string AccountTypeCode, string AccountsDate, string ReceiptNo, string PartyName, string AccountCode
            , string Particulars, string Debit, string Credit, string roworder, string AccountKey)
        {
            string query = string.Format("INSERT INTO `" + DBName + "`.`accounts`(`TrustCode`,`AccountTypeCode`,`AccountsDate`,`ReceiptNo`,`PartyName`,`ContraAccountTypeCode`,"
                + "`Particulars`,`Debit`,`Credit`, `roworder`, `AccountKey`)"
                + "VALUES(\'{0}\',\'{1}\',\'{2}\',\'{3}\',\'{4}\',\'{5}\',\'{6}\',\'{7}\',\'{8}\',\'{9}\',\'{10}\'); "
                , TrustCode, AccountTypeCode, AccountsDate, ReceiptNo, PartyName, AccountCode, Particulars, Debit, Credit, roworder, AccountKey);

            ExecuteNonQuery(query);
        }
        public List<string> GetAccountTypeCodesByAccountCode(string TrustCode, string Description)
        {
            string query = string.Format("select accounttypecode from " + DBName + ".accounttype where trustcode = \'{0}\' and accountcode like \'%{1}%\';",
                TrustCode, Description);
            return ExecuteQueryGetRows(query);
        }
        public void AccountsDelete(string ID)
        {
            string query = string.Format("DELETE FROM `" + DBName + "`.`accounts` WHERE `ID` = \'{0}\';", ID);
            ExecuteNonQuery(query);
        }
        public List<string> GetAccountCodes(string TrustCode)
        {
            string query = string.Format("select accountcode from " + DBName + ".accounttype where trustcode = \'{0}\';",
                TrustCode);
            return ExecuteQueryGetRows(query);
        }
        public DataSet GetAccountsByAccountCode(string TrustCode, string AccountCodeType, string StartDate, string EndDate)
        {
            string query = string.Format("CALL " + DBName + ".GetAccounts (\'{0}\', \'{1}\', \'{2}\', \'{3}\');", TrustCode, AccountCodeType, StartDate, EndDate);
            return ExecuteQueryGetTable(query);
        }

        public Dictionary<string, string> GetContraAccounts(string TrustCode, string AccountCode)
        {
            string query = string.Format("select at.accounttypecode, at.accountcode from " + DBName + ".contraaccounts ca, " + DBName + ".accounttype at "
                + "where ca.trustcode = at.trustcode and ca.accountcode = at.accounttypecode and ca.trustcode = \'{0}\' and at.accountcode = \'{1}\';",
                TrustCode, AccountCode);
            return ExecuteQueryGetDictionary(query);
        }
    }
}
