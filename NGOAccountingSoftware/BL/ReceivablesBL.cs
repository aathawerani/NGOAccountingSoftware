using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication
{
    class ReceivablesBL : BL
    {
        ReceivablesDAL dal = new ReceivablesDAL(); //Utils util = new Utils();

        public void InsertReceivable(string TrustCode, string AccountsDate, string ReceiptNo, string PartyName, string Particulars, string Amount)
        {
            double amount = util.GetAmount(Amount);
            dal.InsertReceivable(TrustCode, AccountsDate, ReceiptNo, PartyName, Particulars, amount.ToString(), "Pending");
        }
        public List<Receivables> GetReceivables(string TrustCode)
        {
            DataSet ds = dal.GetReceivables(TrustCode);
            return Receivables.GetReceivables(ds);
        }
        public void UpdateReceivables(string Receivable_ID)
        {
            dal.UpdateReceivables(Receivable_ID);
        }
    }
}
