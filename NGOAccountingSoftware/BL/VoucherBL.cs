using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication
{
    class VoucherBL : BL
    {
        /*AccountsDAL dal = new AccountsDAL(); */ AccountsBL bl = new AccountsBL(); //GlobalSettings gs = GlobalSettings.GetInstance();

        public void InsertVoucher(string ExpenseDate, string ExpenseSerialNo, string ExpenseReason, string ExpenseAmount, Object Account)
        {
            double Amount = 0;
            double.TryParse(ExpenseAmount, out Amount);
            KeyValuePair<string, string> Item = (KeyValuePair<string, string>)Account;
            string AccountTypeCode = Item.Key;
            int TrustCode = 3;
            List<string> contraAccounts = bl.GetContraAccountTypeCodes(TrustCode, AccountTypeCode);
            string cAccount = contraAccounts[0].ToString();
            bl.AccountingEntries(TrustCode, AccountTypeCode, util.FormatDate(ExpenseDate), ExpenseSerialNo, "", cAccount,
                ExpenseReason, ExpenseAmount, "0", "2", "");
        }
        public string GetMiscParticulars(Object item)
        {
            KeyValuePair<string, string> Item = (KeyValuePair<string, string>)item;
            string description = Item.Value;
            string Particulars = "";
            if (description.ToLower().Contains("tax"))
            {
                Particulars = Item.Value + " " + gs.StartDate.Year + "-" + (gs.StartDate.Year + 1).ToString();
            }
            else if (description.ToLower().Contains("loan"))
            {
                Particulars = "LOAN FROM TRUSTEES";
            }
            else if (description.ToLower().Contains("charity"))
            {
                Particulars = description + " PAID";
            }
            else
            {
                int month = DateTime.Now.Date.Month - 2, year = DateTime.Now.Date.Year;
                if (DateTime.Now.Date.Month == 1)
                {
                    month = 11;
                    year--;
                }
                Particulars = "PAID " + description + " FOR " + GetMonthNames()[month] + " " + year;
            }
            return Particulars;
        }
        public Dictionary<string, string> GetExpenseAccounts(int TrustCode)
        {
            return bl.GetAccountsByAccountType(TrustCode, "EXPENSE");
        }
    }
}
