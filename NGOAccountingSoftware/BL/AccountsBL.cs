using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TrustApplication
{
    class AccountsBL : BL
    {
        AccountsDAL dal = new AccountsDAL(); 

        public void AccountingEntries(int TrustCode, string AccountType, string Date, string SerialNo, string Name, string ContraAccountCode,
            string Particulars, string Debit, string Credit, string roworder, string AccountKey)
        {
            string formatteddate = util.FormatDate(Date); 
            dal.AccountsInsert(TrustCode.ToString(), AccountType, formatteddate, SerialNo, Name,
                ContraAccountCode, Particulars, Debit, Credit, roworder, AccountKey); 
            formatteddate = util.FormatDate(Date);
            dal.AccountsInsert(TrustCode.ToString(), ContraAccountCode, formatteddate, SerialNo, Name,
                AccountType, Particulars, Credit, Debit, roworder, AccountKey); 
        }

        public List<Accounts> GetCashAccount(int TrustCode)
        {
            string formatteddate = util.FormatDate(gs.StartDate); 
            string formatteddate2 = util.FormatDate(gs.EndDate); 
            DataSet ds = dal.GetAccountsByAccountCode(TrustCode.ToString(), "1", formatteddate,
                formatteddate2);
            return GetAccounts(ds);
        }
        public void AccountsUpdate(string Date, string Serial, string Name, string RefAccount, string Particulars, string Debit, string Credit, 
            string ID)
        {
            string formatteddate = util.FormatDate(Date); 
            string debit = util.RawAmount(Debit); 
            string credit = util.RawAmount(Credit); 
            dal.AccountsUpdate(formatteddate, Serial, Name, RefAccount, Particulars, debit, 
                credit, ID);
        }
        public void AccountsInsert(int TrustCode, string AccountTypeCode, string Date, string SerialNo, string Name, string RefAccount, string Particulars,
            string Debit, string Credit, string AccountKey)
        {
            string formatteddate = util.FormatDate(Date); 
            if (Particulars.Contains("OPENING"))
            {
                dal.AccountsInsert(TrustCode.ToString(), AccountTypeCode.ToString(), formatteddate, SerialNo, Name,
                    RefAccount, Particulars, Debit, Credit, "1", AccountKey);
                return;
            }
            dal.AccountsInsert(TrustCode.ToString(), AccountTypeCode.ToString(), formatteddate, SerialNo, Name,
                RefAccount, Particulars, Debit, Credit, "2", AccountKey);
        }
        public void AccountsDelete(string ID)
        {
            dal.AccountsDelete(ID);
        }

        public Dictionary<string, string> GetContraAccountCodes(Object Account)
        {
            KeyValuePair<string, string> Item = (KeyValuePair<string, string>)Account;
            string AccountTypeCode = Item.Key;
            Dictionary<string, string> contraAccounts = dal.GetContraAccountTypeCodesByAccountTypeCode("3", AccountTypeCode);
            return contraAccounts;
        }

        public string GetAccountTotal(int TrustCode, string AccountType, DateTime startdate, DateTime enddate)
        {
            string Total = "";
            string formattedstartdate = util.FormatDate(startdate); 
            string formattedenddate = util.FormatDate(enddate); 
            string AccountTypeCode = GetAccountTypeCodesByAccountCode(TrustCode, AccountType);
            DataSet ds = dal.GetAccountsByAccountCode(TrustCode.ToString(), AccountTypeCode,
                formattedstartdate, formattedenddate); 
            List<Accounts> accountentries = GetAccounts(ds); 
            Total = (accountentries.Count > 0) ? util.RawAmount(((Accounts)accountentries.Last()).Total) : "0";
            return Total;
        }

        public List<string> GetTrustAccountNames(int TrustCode)
        {
            return dal.GetTrustAccountNames(TrustCode.ToString());
        }
        public List<string> GetContraAccountTypeCodes(int TrustCode, string AccountCode)
        {
            return dal.GetContraAccountTypeCodes(TrustCode.ToString(), AccountCode);
        }
        public Dictionary<string, string> GetAccountsByAccountType(int TrustCode, string Type)
        {
            return dal.GetAccountsByAccountType(TrustCode.ToString(), Type);
        }
        public List<string> GetAccountTypes()
        {
            return dal.GetAccountTypes();
        }
        public void InsertAccount(int TrustCode, object AccountType, string AccountCode, string AccountName)
        {
            string _TrustCode = TrustCode.ToString();
            string _AccountType = (string)AccountType;
            List<string> accountstypecodes = dal.GetMaxAccountTypeCode(_TrustCode); 
            string MaxAccountTypeCode = accountstypecodes.FirstOrDefault();
            string AccountTypeCode = (int.Parse(MaxAccountTypeCode) + 1).ToString();
            dal.InsertAccountType(_TrustCode, _AccountType, AccountTypeCode, AccountCode, AccountName);
        }

        public Dictionary<string, string> GetAccountsByTrust(int TrustCode)
        {
            return dal.GetAccountsByTrust(TrustCode.ToString());
        }

        public void TransferAmount(int TrustCode, string Date, string Amount, Object DebitAccount, Object CreditAccount,
            string Particulars)
        {
            double amount = 0;
            double.TryParse(Amount, out amount);
            KeyValuePair<string, string> Item = (KeyValuePair<string, string>)DebitAccount;
            string DebitAccountTypeCode = Item.Key;
            KeyValuePair<string, string> Item2 = (KeyValuePair<string, string>)CreditAccount;
            string CreditAccountTypeCode = Item2.Key;
            AccountingEntries(TrustCode, DebitAccountTypeCode, Date, "", "", CreditAccountTypeCode, Particulars, 
                amount.ToString(), "0", "2", "");
        }

        public List<Accounts> GetAccountsList(int TrustCode, string AccountCode)
        {
            string AccountTypeCode = GetAccountTypeCodesByAccountCode(TrustCode, AccountCode);
            //if (AccountCode == "SSC" || AccountCode == "DSC" || AccountCode == "BEH")
            List<string> certificates = th.GetCertificates(TrustCode); 
            string formattedstartdate = util.FormatDate("1-1-2000"); 
            string formattedstartdate2 = util.FormatDate(gs.StartDate); 
            string formattedenddate = util.FormatDate(gs.EndDate); 
            DataSet ds;
            if (certificates.Contains(AccountCode))
            {
                ds = dal.GetAccountsByAccountCode(TrustCode.ToString(), AccountTypeCode, formattedstartdate,
                    formattedenddate); 
                return GetAccounts(ds);
            }
            ds = dal.GetAccountsByAccountCode(TrustCode.ToString(), AccountTypeCode, formattedenddate,
                formattedenddate); 
            return GetAccounts(ds);
        }

        public DataSet GetAccountByDetail(string TrustCode, string AccountTypeCode, string AccountsDate, string ReceiptNo, 
            string AccountKey)
        {
            return dal.GetAccountByDetail(TrustCode, AccountTypeCode, AccountsDate, ReceiptNo, AccountKey);
        }

    }
}
