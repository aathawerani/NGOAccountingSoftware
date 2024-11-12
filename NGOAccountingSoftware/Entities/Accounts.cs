using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TrustApplication
{
    class Accounts
    {
        public Accounts() {}
        public Accounts(string _Date, string _Account, string _Particulars, string _Debit, string _Credit, string _AccountKey)
        {
            Date = _Date; Account = _Account; Particulars = _Particulars; Debit = _Debit; Credit = _Credit; AccountKey = _AccountKey;
        }
        Utils utils = new Utils();
        string _Date;
        string _Account;
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
        public string Account
        {
            get
            {
                return _Account;
            }
            set
            {
                _Account = value;
            }
        }
        public string Name { get; set; }
        public string AccountKey { get; set; }
        public string Particulars { get; set; }
        string _Debit;
        public string Debit {
            get
            {
                return utils.FormatAmount(_Debit);
            }
            set
            {
                _Debit = utils.RawAmount(value);
            }
        }
        string _Credit;
        public string Credit {
            get
            {
                return utils.FormatAmount(_Credit);
            }
            set
            {
                _Credit = utils.RawAmount(value);
            }
        }
        string _Total;
        public string Total {
            get
            {
                return utils.FormatAmount(_Total);
            }
            set
            {
                _Total = utils.RawAmount(value);
            }
        }
        public string ID { get; set; }

        public string ContraAccount { get; set; }
        public string Order { get; set; }


        /*public static List<Accounts> GetAccounts(DataSet Accounts, out Response response)
        {
            List<Accounts> Result = new List<Accounts>();
            foreach(DataRow row in Accounts.Tables[0].Rows)
            {
                //from stored procedure GetAccounts
                Accounts temp = new Accounts();
                if (row["ID"] == null)
                {
                    response = new Response(ResponseType.Failure, "ID not found");
                    return Result;
                }
                temp.ID = row["ID"].ToString();
                if (row["Date"] == null)
                {
                    response = new Response(ResponseType.Failure, "Date not found");
                    return Result;
                }
                temp._Date = row["Date"].ToString();
                if (row["ReceiptNo"] == null)
                {
                    response = new Response(ResponseType.Failure, "ReceiptNo not found");
                    return Result;
                }
                temp.No = row["ReceiptNo"].ToString();
                if (row["Name"] == null)
                {
                    response = new Response(ResponseType.Failure, "Name not found");
                    return Result;
                }
                temp.Name = row["Name"].ToString();
                if (row["IAccountD"] == null)
                {
                    response = new Response(ResponseType.Failure, "Account not found");
                    return Result;
                }
                temp.Account = row["Account"].ToString();
                if (row["Particulars"] == null)
                {
                    response = new Response(ResponseType.Failure, "Particulars not found");
                    return Result;
                }
                temp.Particulars = row["Particulars"].ToString();
                if (row["Debit"] == null)
                {
                    response = new Response(ResponseType.Failure, "Debit not found");
                    return Result;
                }
                temp.Debit = row["Debit"].ToString();
                if (row["Credit"] == null)
                {
                    response = new Response(ResponseType.Failure, "Credit not found");
                    return Result;
                }
                temp.Credit = row["Credit"].ToString();
                if (row["cumulative_sum"] == null)
                {
                    response = new Response(ResponseType.Failure, "cumulative_sum not found");
                    return Result;
                }
                temp.Total = row["cumulative_sum"].ToString();
                if (row["AccountKey"] == null)
                {
                    response = new Response(ResponseType.Failure, "AccountKey not found");
                    return Result;
                }
                temp.AccountKey = row["AccountKey"].ToString();
                Result.Add(temp);
            }
            response = new Response(ResponseType.Success, "Accounts list created successfully");

            return Result;
        }*/

    }
}
