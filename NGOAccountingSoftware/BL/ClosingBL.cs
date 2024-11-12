using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication
{
    class ClosingBL : BL
    {
        LoadAccountsBL lbl = new LoadAccountsBL();

        public void OpeningEntries(int TrustCode, string Year)
        {
            DateTime newdate = util.GetNextDate(Year);
            lbl.LoadData(TrustCode, Year);
            List<Accounts> openingentries = th.GetOpeningEntries(TrustCode, lbl, util.FormatDate(newdate));
            AccountsInsert(TrustCode, openingentries);
        }

        public void ClosingEntries(int TrustCode, string Year)
        {
            DateTime newdate = util.GetNextDate(Year);
            lbl.LoadData(TrustCode, Year);
            string formatteddate = util.FormatDate(newdate);
            List<Accounts> closingentries = th.GetClosingEntries(TrustCode, lbl, formatteddate);
            AccountsInsert(TrustCode, closingentries);
        }
    }
}
