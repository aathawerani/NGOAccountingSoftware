using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using TrustApplication.Exceptions;

namespace TrustApplication
{
    class Utils
    {
        public string TrimTimeDateGB(string Date)
        {
            string DateString = "";
            DateTimeStyles styles;
            CultureInfo GBculture;
            GBculture = CultureInfo.CreateSpecificCulture("en-GB");
            styles = DateTimeStyles.None;
            DateTime dt;

            if (DateTime.TryParse(Date, GBculture, styles, out dt))
            {
                int year = dt.Year;
                int month = dt.Month;
                int day = dt.Day;
                DateString = day + "-" + month + "-" + year;
            }
            else
            {
                throw new UtilityException("Could not parse date");
            }
            return DateString;
        }
        public string FormatDateUS(string Date)
        {
            string DateString = "";
            DateTimeStyles styles;
            CultureInfo GBculture;
            GBculture = CultureInfo.CreateSpecificCulture("en-GB");
            styles = DateTimeStyles.None;
            DateTime dt;

            if (DateTime.TryParse(Date, GBculture, styles, out dt))
            {
                DateString = dt.ToString();
            }
            else
            {
                throw new UtilityException("Could not parse date");
            }
            return DateString;
        }
        public string FormatDateGB(string Date)
        {
            string DateString = "";
            DateTimeStyles styles;
            CultureInfo USculture;
            USculture = CultureInfo.CreateSpecificCulture("en-US");
            styles = DateTimeStyles.None;
            DateTime dt;

            if (DateTime.TryParse(Date, USculture, styles, out dt))
            {
                int year = dt.Year;
                int month = dt.Month;
                int day = dt.Day;
                DateString = day + "-" + month + "-" + year;
            }
            else
            {
                throw new UtilityException("Could not parse date");
            }
            return DateString;
        }
        public string FormatDate(DateTime Date)
        {
            int year = Date.Year;
            int month = Date.Month;
            int day = Date.Day;
            string DateString = day + "-" + month + "-" + year;
            return DateString;
        }
        public string FormatDate(string Date)
        {
            DateTime dt = new DateTime();
            DateTime.TryParse(Date, out dt);
            int year = dt.Year;
            int month = dt.Month;
            int day = dt.Day;
            string DateString = "" + day + "-" + month + "-" + year + "";
            return DateString;
        }
        public string FormatDate2(DateTime Date)
        {
            int year = Date.Year;
            int month = Date.Month;
            int day = Date.Day;
            string DateString = day + "/" + month + "/" + year;
            return DateString;
        }
        public string[] Splitdate(string Date)
        {
            string[] date = Date.Split('-');
            if (date.Length < 3)
            {
                date = Date.Split('/');
                if (date.Length < 3)
                {
                    throw new UtilityException("Could not parse date");
                }
            }
            return date;
        }
        public string RawAmount(string amount)
        {
            string temp = amount;
            int index = temp.IndexOf('.');
            if (index > 0)
            {
                temp = temp.Substring(0, index);
            }
            string[] temp2 = temp.Split(new char[] { ',' });
            string final = "";
            foreach(string s in temp2)
            {
                final += s;
            }
            return final;
        }
        public string FormatAmount(string amount)
        {
            if (amount == "")
            {
                return "0";
            }
            string temp = string.Format("{0:N}", Convert.ToDecimal(amount));
            int index = temp.IndexOf('.');
            if (index > 0)
            {
                temp = temp.Substring(0, index);
            }
            return temp;
        }
        public DateTime ConvertToDate(string Date)
        {
            DateTime date = new DateTime();
            if (DateTime.TryParse(Date, out date))
            {
                return date;
            }
            throw new UtilityException("Could not parse date");
        }
        public string GetSerialNumber(int num)
        {
            string receiptno = "";
            if (num > 0 && num < 999)
            {
                num++;
                if (num < 10)
                {
                    receiptno = "00" + num.ToString();
                }
                else if (num >= 10 && num < 100)
                {
                    receiptno = "0" + num.ToString();
                }
                else if (num >= 100)
                {
                    receiptno = num.ToString();
                }
            }
            else if (num == 999)
            {
                receiptno = "001";
            }
            else if (num == 0)
            {
                throw new UtilityException("Unable to generate serial no");
            }
            return receiptno;
        }

        public double GetAmount(string amount)
        {
            double doubleamount = 0;
            if (double.TryParse(amount, out doubleamount))
            {
                return doubleamount;
            }
            throw new UtilityException("Could not parse amount");
        }

        public DateTime GetEndDate(string Years)
        {
            string[] years = Years.Split('-');
            int temp = 0;
            DateTime reportEndDate;
            if (int.TryParse(years[1], out temp))
            {
                reportEndDate = new DateTime(temp, 6, 30);
                return reportEndDate;
            }
            throw new UtilityException("Could not parse year");
        }

        /*public string[] GetStartEndDates(string Date, out Response response)
        {
            string[] dates = new string[2];
            DateTime dt = ConvertToDate(Date, out response);
            if (!response.IsSuccess()) return dates;
            int day = dt.Day;
            int month = dt.Month;
            int year = dt.Year;
            DateTime startdate;
            DateTime enddate;
            if (month > 6)
            {
                startdate = new DateTime(year, 7, 1);
                enddate = new DateTime(year + 1, 6, 30);
            }
            else
            {
                startdate = new DateTime(year - 1, 7, 1);
                enddate = new DateTime(year, 6, 30);
            }
            dates[0] = startdate.ToString();
            dates[1] = enddate.ToString();
            response = new Response(ResponseType.Success, "Start dates created successfully");
            return dates;
        }*/

        public DateTime GetStartDate(string Years)
        {
            string[] years = Years.Split('-');
            int temp;
            DateTime reportStartDate;
            if (int.TryParse(years[0], out temp))
            {
                reportStartDate = new DateTime(temp, 7, 1);
                return reportStartDate;
            }
            throw new UtilityException("Could not parse date");
        }

        public DateTime GetNextDate(string Year)
        {
            int endyear;
            DateTime newdate = new DateTime();
            string[] years = Year.Split('-');
            if (int.TryParse(years[1], out endyear))
            {
                newdate = new DateTime(endyear, 7, 1);
                return newdate;
            }
            throw new UtilityException("Could not parse date");
        }

        public DateTime ConstructFromDate(string Y, int M)
        {
            int FrY;
            DateTime Date = new DateTime();
            if (int.TryParse(Y, out FrY))
            {
                Date = new DateTime(FrY, M, 1);
                return Date;
            }
            throw new UtilityException("Could not parse year");
        }

        public DateTime ConstructToDate(string Y, int M)
        {
            int FrY;
            DateTime Date = new DateTime();
            if (int.TryParse(Y, out FrY))
            {
                Date = new DateTime(FrY, M, DateTime.DaysInMonth(FrY, M));
                return Date;
            }
            throw new UtilityException("Could not parse year");
        }
        public int CalculateNumMonths(int FromM, int FromY, int ToM, int ToY)
        {
            int index = 0;
            if (FromY == ToY)
            {
                if (FromM <= ToM)
                {
                    index = ToM - FromM;
                }
                else
                {
                    throw new UtilityException("From month should be less than to month");
                }
            }
            else if (FromY < ToY)
            {
                if (FromM == ToM)
                {
                    index = (ToY - FromY) * 12;
                }
                else if (FromM < ToM)
                {
                    int NumMonths = (ToY - FromY) * 12;
                    NumMonths += ToM - FromM;
                    index = NumMonths;
                }
                else if (FromM > ToM)
                {
                    int NumMonths = (ToY - FromY - 1) * 12;
                    NumMonths = 12 - FromM;
                    NumMonths += ToM; //could be mistake of 1 month
                    index = NumMonths;
                }
            }
            else
            {
                throw new UtilityException("From Year should not be greater than To Year");
            }
            return index;
        }
        public bool IsAlphaNumeric(string s)
        {
            Regex r = new Regex("^[a-zA-Z0-9]*$");
            return r.IsMatch(s);
        }

        public string ExtractNo(string s, int maxPos)
        {
            int no = 0;
            for(int pos = maxPos; pos>0; pos--)
            {
                if (s.Length < pos) continue;
                if (int.TryParse(s.Substring(0, pos), out no))
                {
                    //return no.ToString();
                    return s.Substring(0, pos);
                }
            }
            return no.ToString();
        }

        public string ExtractToken(string token, string particulars, out bool tokenfound)
        {
            if (particulars.Length < token.Length)
            {
                throw new TrustException("Particulars can't be blank");
            }
            if (particulars.ToUpper().StartsWith(token))
            {
                particulars = particulars.Substring(token.Length).Trim();
                tokenfound = true;
            }
            else
            {
                tokenfound = false;
            }
            return particulars;
        }

        public string ExtractDatePart(string particulars, int length, out string datepart)
        {
            if (particulars.Length > 1)
            {
                datepart = ExtractNo(particulars, length).ToString();
                particulars = particulars.Substring(datepart.Length).Trim('/').Trim();
            }
            else
            {
                throw new TrustException("Rent duration not found");
            }
            return particulars;
        }

        public void ExtractDateRange(string particulars, out string FDate, out string TDate)
        {
            //Shop 34, RENT @430, 1/1/2021-30/6/2021
            string fromday, frommonth, fromyear, today, tomonth, toyear;
            particulars = ExtractDatePart(particulars, 2, out fromday);
            particulars = ExtractDatePart(particulars, 2, out frommonth);
            particulars = ExtractDatePart(particulars, 4, out fromyear);
            int intfromday = Convert.ToInt32(fromday);
            int intfrommonth = Convert.ToInt32(frommonth);
            int intfromyear = Convert.ToInt32(fromyear);
            DateTime fromdatetime = new DateTime(intfromyear, intfrommonth, intfromday);
            FDate = FormatDate(fromdatetime);
            particulars = particulars.Trim('-').Trim();
            particulars = ExtractDatePart(particulars, 2, out today);
            particulars = ExtractDatePart(particulars, 2, out tomonth);
            particulars = ExtractDatePart(particulars, 4, out toyear);
            int inttoday = Convert.ToInt32(today);
            int inttomonth = Convert.ToInt32(tomonth);
            int inttoyear = Convert.ToInt32(toyear);
            DateTime todatetime = new DateTime(inttoyear, inttomonth, inttoday);
            TDate = FormatDate(todatetime);
        }

        public int SkipChars(string particulars)
        {
            int skipcount = 0;
            bool IsChar = false;
            int value;
            if (int.TryParse(particulars.Substring(0, 1), out value))
                IsChar = false;
            else
                IsChar = true;
            for(skipcount = 0; IsChar == true; skipcount++)
            {
                if (int.TryParse(particulars.Substring(skipcount, 1), out value))
                    break;
            }
            return skipcount;
        }
    }
}
