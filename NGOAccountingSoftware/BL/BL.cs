using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Globalization;
using TrustApplication.Exceptions;
using System.Collections;

namespace TrustApplication
{
    class BL
    {
        DAL dal = new DAL(); 
        int CurrentYear;

        protected TrustHandler th = new TrustHandler();
        protected Utils util = new Utils();
        protected GlobalSettings gs = GlobalSettings.GetInstance();

        protected BL()
        {
            CurrentYear = DateTime.Now.Year;
            if (DateTime.Now.Month < 7)
            {
                gs.StartDate = new DateTime(CurrentYear - 1, 7, 1);
                gs.EndDate = new DateTime(CurrentYear, 6, 30);
            }
            else
            {
                gs.StartDate = new DateTime(CurrentYear, 7, 1);
                gs.EndDate = new DateTime(CurrentYear + 1, 6, 30);
            }
            //StartDate = new DateTime(2017, 7, 1);
            //dal = new DAL();
        }

        #region General


        public string[] GetMonthNames() {return gs.Months;}
        public string[] GetMonthNos(){return gs.NumMonths;}
        public List<string> GetRentYears(){return GetYears(10, 3);}
        public string[] GetDays(){return gs.days;}
        public List<string> GetMaturityYears(){return GetYears(1, 11);}
        public string[] GetIslamicMonths(){return gs.IslamicMonths;}
        public string[] GetTime(){return gs.time;}
        public string[] GetAM(){return gs.AM;}
        public List<string> GetTrustNames(){return dal.GetTrustNames();}

        public void SetYear(string year)
        {
            string[] years = year.Split('-');
            int temp;
            if (int.TryParse(years[0], out temp))
            {
                gs.StartDate = new DateTime(temp, 7, 1);
            }
            else
            {
                throw new BLException("Could not parse starting year");
            }
            if (int.TryParse(years[1], out temp))
            {
                gs.EndDate = new DateTime(temp, 6, 30);
            }
            else
            {
                throw new BLException("Could not parse ending year");
            }
        }

        public List<string> GetYears(int start, int end)
        {
            List<string> NumY = new List<string>();
            for (int i = CurrentYear - start; i < CurrentYear + end; i++)
            {
                NumY.Add(i.ToString());
            }
            return NumY;
        }
        public string[] ConvertDate(string date)
        {
            DateTimeFormatInfo fi = new CultureInfo("ar-sa").DateTimeFormat;
            fi.Calendar = new HijriCalendar();
            string hijriDate = String.Format(fi, "{0:D}", Convert.ToDateTime(date).Date);
            string[] result = hijriDate.Split('/');
            return result;
        }
        public bool ValidateDateRange(int FromM, string FromY, int ToM, string ToY)
        {
            int toy = 0, fromy = 0;
            if (!int.TryParse(ToY, out toy))
            {
                throw new BLException("Could not parse to year");
            }
            if (!int.TryParse(FromY, out fromy))
            {
                throw new BLException("Could not parse from year");
            }
            if (toy < fromy)
            {
                throw new BLException("to year is less than from year");
            }
            if (toy == fromy && ToM < FromM)
            {
                throw new BLException("to month is less than from month");
            }
            return true;
        }
        public bool ValidateAmount(string Amount)
        {
            double amount = 0;
            if (double.TryParse(Amount, out amount))
            {
                if (amount < 0)
                {
                    throw new BLException("Amount is less than 0");
                }
            }
            else
            {
                throw new BLException("Could not parse amount");
            }
            return true;
        }

        public string[] GetAccountingYears()
        {
            List<string> years = dal.GetAccountYears("1");
            ArrayList Years = new System.Collections.ArrayList();
            //string[] Years = new string[years.Count];
            int counter = 0;
            int startYear = 0;
            foreach (string year in years)
            {
                int yearNum = int.Parse(year);
                if (counter == 0)
                {
                    startYear = int.Parse(year);
                }
                else
                {
                    if(yearNum < startYear)
                    {
                        startYear = yearNum;
                    }
                }
                counter++;
            }

            int endYear = DateTime.Now.Year;
            counter = 0;
            string temp = "";
            for (int yearNum=startYear; yearNum <= endYear; yearNum++)
            {
                if (counter == 0)
                {
                    temp = yearNum.ToString();
                }
                else 
                {
                    temp = temp + "-" + yearNum.ToString();
                    //Years[counter - 1] = temp;
                    Years.Add(temp);
                    temp = yearNum.ToString();
                }
                counter++;
            }
            temp = temp + "-" + (int.Parse(temp) + 1);
            //Years[counter - 1] = temp;
            Years.Add(temp);

            string[] strYears = new string[Years.Count];
            counter = 0;
            foreach (string str in Years)
            {
                strYears[counter++] = str;
            }

            return strYears;
        }
        public string GetAccountTypeCodesByAccountCode(int TrustCode, string AccountCode)
        {
            List<string> accountcodes = dal.GetAccountTypeCodesByAccountCode(TrustCode.ToString(), AccountCode);
            string accounttypecode = accountcodes.FirstOrDefault();
            return accounttypecode;
        }

        public void AccountsDualInsert(int TrustCode, List<Accounts> entries)
        {
            foreach (Accounts entry in entries)
            {
                string account = GetAccountTypeCodesByAccountCode(TrustCode, entry.Account);
                string contraaccount = GetAccountTypeCodesByAccountCode(TrustCode, entry.ContraAccount);
                string formatteddate = util.FormatDate(entry.Date);
                dal.AccountsInsert(TrustCode.ToString(), account, formatteddate, entry.No, entry.Name,
                    contraaccount, entry.Particulars, entry.Debit, entry.Credit, entry.Order, entry.AccountKey);
                dal.AccountsInsert(TrustCode.ToString(), contraaccount, formatteddate, entry.No, entry.Name,
                    account, entry.Particulars, entry.Credit, entry.Debit, entry.Order, entry.AccountKey);
            }
        }
        public void AccountsInsert(int TrustCode, List<Accounts> entries)
        {
            foreach (Accounts entry in entries)
            {
                string account = GetAccountTypeCodesByAccountCode(TrustCode, entry.Account);
                string contraaccount = GetAccountTypeCodesByAccountCode(TrustCode, entry.ContraAccount);
                string formatteddate = util.FormatDate(entry.Date);
                dal.AccountsInsert(TrustCode.ToString(), account, formatteddate, entry.No, entry.Name,
                    contraaccount, entry.Particulars, entry.Debit, entry.Credit, entry.Order, entry.AccountKey);
            }
        }

        public List<Accounts> GetAccounts(DataSet Accounts)
        {
            List<Accounts> Result = new List<Accounts>();
            foreach (DataRow row in Accounts.Tables[0].Rows)
            {
                //from stored procedure GetAccounts
                Accounts temp = new Accounts();
                if (row["ID"] == null)
                {
                    throw new BLException("ID not found");
                }
                temp.ID = row["ID"].ToString();
                if (row["Date"] == null)
                {
                    throw new BLException("Date not found");
                }
                temp.Date = row["Date"].ToString();
                if (row["ReceiptNo"] == null)
                {
                    throw new BLException("ReceiptNo not found");
                }
                temp.No = row["ReceiptNo"].ToString();
                if (row["Name"] == null)
                {
                    throw new BLException("Name not found");
                }
                temp.Name = row["Name"].ToString();
                if (row["IAccountD"] == null)
                {
                    throw new BLException("Account not found");
                }
                temp.Account = row["Account"].ToString();
                if (row["Particulars"] == null)
                {
                    throw new BLException("Particulars not found");
                }
                temp.Particulars = row["Particulars"].ToString();
                if (row["Debit"] == null)
                {
                    throw new BLException("Debit not found");
                }
                temp.Debit = row["Debit"].ToString();
                if (row["Credit"] == null)
                {
                    throw new BLException("Credit not found");
                }
                temp.Credit = row["Credit"].ToString();
                if (row["cumulative_sum"] == null)
                {
                    throw new BLException("cumulative_sum not found");
                }
                temp.Total = row["cumulative_sum"].ToString();
                if (row["AccountKey"] == null)
                {
                    throw new BLException("AccountKey not found");
                }
                temp.AccountKey = row["AccountKey"].ToString();
                Result.Add(temp);
            }
            return Result;
        }

        public List<Accounts> GetAccountsFromTable(DataSet Accounts)
        {
            List<Accounts> Result = new List<Accounts>();
            foreach (DataRow row in Accounts.Tables[0].Rows)
            {
                //from stored procedure GetAccounts
                Accounts temp = new Accounts();
                if (row["ID"] == null)
                {
                    throw new BLException("ID not found");
                }
                temp.ID = row["ID"].ToString();
                if (row["AccountsDate"] == null)
                {
                    throw new BLException("AccountsDate not found");
                }
                temp.Date = row["AccountsDate"].ToString();
                if (row["ReceiptNo"] == null)
                {
                    throw new BLException("ReceiptNo not found");
                }
                temp.No = row["ReceiptNo"].ToString();
                if (row["PartyName"] == null)
                {
                    throw new BLException("PartyName not found");
                }
                temp.Name = row["PartyName"].ToString();
                if (row["AccountTypeCode"] == null)
                {
                    throw new BLException("AccountTypeCode not found");
                }
                temp.Account = row["AccountTypeCode"].ToString();
                if (row["ContraAccountTypeCode"] == null)
                {
                    throw new BLException("ContraAccountTypeCode not found");
                }
                temp.ContraAccount = row["ContraAccountTypeCode"].ToString();
                if (row["Particulars"] == null)
                {
                    throw new BLException("Particulars not found");
                }
                temp.Particulars = row["Particulars"].ToString();
                if (row["Debit"] == null)
                {
                    throw new BLException("Debit not found");
                }
                temp.Debit = row["Debit"].ToString();
                if (row["Credit"] == null)
                {
                    throw new BLException("Credit not found");
                }
                temp.Credit = row["Credit"].ToString();
                if (row["AccountKey"] == null)
                {
                    throw new BLException("AccountKey not found");
                }
                temp.AccountKey = row["AccountKey"].ToString();
                Result.Add(temp);
            }
            return Result;
        }


        public void AccountsDelete(List<Accounts> entries)
        {
            foreach (Accounts entry in entries)
            {
                dal.AccountsDelete(entry.ID);
            }
        }

        public DataSet GetAccountsByAccountCode(int TrustCode, string AccountCode)
        {
            string AccountTypeCode = GetAccountTypeCodesByAccountCode(TrustCode, AccountCode);
            string formattedstartdate = util.FormatDate(gs.StartDate);
            string formattedenddate = util.FormatDate(gs.EndDate);
            return dal.GetAccountsByAccountCode(TrustCode.ToString(), AccountTypeCode, formattedstartdate,
                formattedenddate);
        }

        #endregion General
    }
}
