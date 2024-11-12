using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TrustApplication.Exceptions;

namespace TrustApplication
{
    class Receivables
    {
        Utils utils = new Utils();
        string _Date;
        public string Date
        {
            get
            {
                return _Date;
            }
            set
            {
                try
                {
                    _Date = utils.FormatDateGB(value);
                }
                catch (Exception e)
                {
                    _Date = utils.FormatDateUS(value);
                }
            }
        }
        public string No { get; set; }
        public string Rpart { get; set; }
        public string Name { get; set; }
        string _Total;
        public string Total
        {
            get
            {
                return utils.FormatAmount(_Total);
            }
            set
            {
                _Total = utils.RawAmount(value);
            }
        }
        public string ID
        {
            get;
            set;
        }
        public string Status { get; set; }


        public static List<Receivables> GetReceivables(DataSet Receivable)
        {
            List<Receivables> Result = new List<Receivables>();
            foreach (DataRow row in Receivable.Tables[0].Rows)
            {
                Receivables temp = new Receivables();
                if (row["ID"] == null)
                {
                    throw new ReceivablesException("ID not found");
                }
                temp.ID = row["ID"].ToString();
                if (row["AccountsDate"] == null)
                {
                    throw new ReceivablesException("AccountsDate not found");
                }
                temp._Date = row["AccountsDate"].ToString();
                if (row["ReceiptNo"] == null)
                {
                    throw new ReceivablesException("ReceiptNo not found");
                }
                temp.No = row["ReceiptNo"].ToString();
                if (row["PartyName"] == null)
                {
                    throw new ReceivablesException("PartyName not found");
                }
                temp.Name = row["PartyName"].ToString();
                if (row["Particulars"] == null)
                {
                    throw new ReceivablesException("Particulars not found");
                }
                temp.Rpart = row["Particulars"].ToString();
                if (row["Amount"] == null)
                {
                    throw new ReceivablesException("Amount not found");
                }
                temp.Total = row["Amount"].ToString();
                if (row["Status"] == null)
                {
                    throw new ReceivablesException("Status not found");
                }
                temp.Status = row["Status"].ToString();
                Result.Add(temp);
            }
            return Result;
        }

    }
}
