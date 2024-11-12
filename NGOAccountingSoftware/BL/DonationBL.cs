using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustApplication.Exceptions;

namespace TrustApplication
{
    class DonationBL : BL
    {
        //AccountsDAL dal = new AccountsDAL(); 
        AccountsBL bl = new AccountsBL();

        public Dictionary<string, string> GetDonationAccounts()
        {
            return bl.GetAccountsByAccountType(3, "DONATION");
        }

        public string GetDonationParticulars(Object item)
        {
            KeyValuePair<string, string> Item = (KeyValuePair<string, string>)item;
            string description = Item.Value;
            string Particulars = "";
            if (description.ToUpper().Contains("CAPITAL"))
            {
                Particulars = "RECEIVED FROM BANDE KHUDA FOR CONSTRUCTION OF IMAMBARGAH ANNEXE";
            }
            else if (description.ToUpper().Contains("BOX"))
            {
                Particulars = "BOXES IN IMAMBARGAH WALLS & ON MIMBER";
            }
            else
            {
                Particulars = "RECEIVED FROM ";
            }
            return Particulars;
        }

        public void InsertDonation(string DonationDate, string DonationSerialNo, string DonationDonarName,
            string DonationReason, Object Account, Object contraAccount, string DonationAmount)
        {
            double Amount;
            if (double.TryParse(DonationAmount, out Amount))
            {
                throw new DonationBLException("Unable to parse donation amount");
            }
            int TrustCode = 3;
            KeyValuePair<string, string> Item = (KeyValuePair<string, string>)Account;
            string AccountTypeCode = Item.Key;
            KeyValuePair<string, string> Item2 = (KeyValuePair<string, string>)contraAccount;
            string contraAccountTypeCode = Item2.Key;
            string formatteddate = util.FormatDate(DonationDate);
            bl.AccountingEntries(TrustCode, AccountTypeCode, formatteddate, DonationSerialNo, DonationDonarName,
                contraAccountTypeCode, DonationReason, "0", Amount.ToString(), "2", "");
        }
    }
}
