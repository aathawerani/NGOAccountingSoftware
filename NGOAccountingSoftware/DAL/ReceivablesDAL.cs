using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace TrustApplication
{
    class ReceivablesDAL : DAL
    {
        public void InsertReceivable(string TrustCode, string AccountsDate, string ReceiptNo, string PartyName, string Particulars, string Amount, 
            string Status)
        {
            string query = string.Format("INSERT INTO `" + DBName + "`.`receivables` (`TrustCode`,`AccountsDate`,`ReceiptNo`,`PartyName`,`Particulars`,"
                + "`Amount`,`Status`)"
                + "VALUES (\'{0}\',\'{1}\',\'{2}\',\'{3}\',\'{4}\',\'{5}\',\'{6}\';"
                , TrustCode, AccountsDate, ReceiptNo, PartyName, Particulars, Amount, Status);
            ExecuteNonQuery(query);
        }
        public DataSet GetReceivables(string TrustCode)
        {
            string query = string.Format("SELECT * FROM " + DBName + ".receivables where status = \'Pending\' and TrustCode=\'{0}\';", TrustCode);
            return ExecuteQueryGetTable(query);
        }
        public void UpdateReceivables(string Receivable_ID)
        {
            string query = string.Format("update " + DBName + ".receivables set status = \'Received\' where ID=\'{0}\';", Receivable_ID);
            ExecuteNonQuery(query);
        }
    }
}
