using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustApplication.Exceptions;

namespace TrustApplication
{
    abstract class Trust
    {
        protected Utils util = new Utils();
        public List<string> GetCertificates()
        {
            List<string> certificates = new List<string>();
            certificates.Add("SSC");
            certificates.Add("DSC");
            certificates.Add("BEH");
            return certificates;
        }
        public Accounts CreateEntry(string Account, string ContraAccount, string Debit, string Credit, string AccountKey
            , string Particulars, string Date)
        {
            Accounts entry = new Accounts();
            entry.Particulars = Particulars; entry.Account = Account; entry.ContraAccount = ContraAccount; 
            entry.Debit = Debit; entry.Credit = Credit; entry.AccountKey = AccountKey;
            entry.Order = "2"; entry.Name = ""; entry.No = ""; entry.Date = Date;
            return entry;
        }
        public Accounts CreateEntry(string Account, string Debit, string Credit, string Date)
        {
            Accounts entry = new Accounts();
            entry.Particulars = "OPENING BALANCE"; entry.Account = Account; entry.ContraAccount = "";
            entry.Debit = Debit; entry.Credit = Credit; entry.AccountKey = "";
            entry.Order = "1"; entry.Name = ""; entry.No = ""; entry.Date = Date;
            return entry;
        }
        public Accounts CreateEntry(string Account, string ContraAccount, string Debit, string Credit, string AccountKey, 
            string Particulars, string Date, string Name, string No)
        {
            Accounts entry = new Accounts();
            entry.Particulars = Particulars; entry.Account = Account; entry.ContraAccount = ContraAccount;
            entry.Debit = Debit; entry.Credit = Credit; entry.AccountKey = AccountKey;
            entry.Order = "2"; entry.Name = Name; entry.No = No; entry.Date = Date;
            return entry;
        }
        public Accounts CreateEntry(string Account, string ContraAccount, string Debit, string Credit, string AccountKey,
            string Particulars, string Date, string Name, string No, string ID, string Order)
        {
            Accounts entry = new Accounts();
            entry.Particulars = Particulars; entry.Account = Account; entry.ContraAccount = ContraAccount;
            entry.Debit = Debit; entry.Credit = Credit; entry.AccountKey = AccountKey;
            entry.Order = Order; entry.Name = Name; entry.No = No; entry.Date = Date; entry.ID = ID;
            return entry;
        }
        public List<Accounts> GetCertificateProfit(string CertList, string ProfitAmount, string TaxAmount, 
            string FolioNo, string Date)
        {
            List<Accounts> profitentries = new List<Accounts>();
            profitentries.Add(CreateEntry("PROFIT", "CASH", "0", ProfitAmount, "PROFIT-" + FolioNo, 
                CertList + " PROFIT RECEIVED ", Date));
            profitentries.Add(CreateEntry("WHT", "CASH", TaxAmount, "0", "TAX-" + FolioNo,
                CertList + " TAX ON PROFIT ", Date));
            return profitentries;
        }
        public List<Accounts> GetCertificateMatured(string CertList, string Type, string CertificateAmount, 
            string FolioNo, string Date)
        {
            List<Accounts> profitentries = new List<Accounts>();
            profitentries.Add(CreateEntry(Type, "CASH", "0", CertificateAmount, "MATURED-" + FolioNo,
                CertList + " MATURED ", Date));
            return profitentries;
        }
        public List<Accounts> GetCertificatePurchased(string CertificateNo, string Type, string CertificateAmount,
            string FolioNo, string Date)
        {
            List<Accounts> profitentries = new List<Accounts>();
            profitentries.Add(CreateEntry(Type, "CASH", CertificateAmount, "0", "MATURED-" + FolioNo,
                FolioNo + " " + CertificateNo + " PURCHASED ", Date));
            return profitentries;
        }
        abstract public List<Accounts> GetOpeningEntries(LoadAccountsBL lbl, string newdate);
        abstract public List<Accounts> GetClosingEntries(LoadAccountsBL lbl, string newdate);
        abstract public List<Accounts> GetRentEntries(string Plotcode, string Date, string SerialNo, string Name,
            string RentParticulars, string RentTotalRent, string WaterParticulars, string RentTotalWaterCharges, string RentArearsParticulars,
            double RArears, string WaterArearsParticulars, double WArears);
        abstract public Dictionary<string, string> GetYearlyTotals(LoadAccountsBL lbl);
        abstract public List<Accounts> GetTrialBalance(LoadAccountsBL lbl);
        abstract public string GetRentAccount(string PlotNo, int type);

        abstract public string GetAccountKey(string ContraAccount, string Particulars);

        abstract public bool IsRent(string AccountCode);
        abstract public bool IsCertificate(string AccountCode);

        public void GetRent(Accounts entry, string Years, out Tenant t, out Rent r)
        {
            //Rent r = new Rent();
            r = new Rent();
            t = new Tenant();
            //Shop 34, RENT @430, 1/1/2021-30/6/2021
            //Shop 34, WATER @170, 1/1/2021-30/6/2021
            //Shop 34, WATER AREARS JUL-DEC 2020 @60

            r.Name = entry.Name;
            t.Name = entry.Name;
            r.No = entry.No;

            ValidateEntryDate(Years, entry);

            r.Date = entry.Date;

            string particulars = entry.Particulars;
            if (particulars.Length == 0)
            {
                throw new TrustException("Particulars can't be blank");
            }
            string token = "SHOP";
            bool tokenfound;
            particulars = util.ExtractToken(token, particulars, out tokenfound);
            if(tokenfound)
            {
                t.Space = token;
            }
            else
            {
                token = "FLAT";
                particulars = util.ExtractToken(token, particulars, out tokenfound);
                if (tokenfound)
                {
                    t.Space = token;
                }
            }
            if (!tokenfound) 
                throw new TrustException("SHOP / FLAT not found");

            if (particulars.Length > 1)
            {
                string no = util.ExtractNo(particulars, 2).ToString();
                particulars = particulars.Substring(no.Length).Trim(',').Trim();
                t.No = no;
            }
            else
            {
                throw new TrustException("SHOP / FLAT no. not found");
            }
            if (particulars.Contains("AREARS"))
            {
                if (particulars.ToUpper().StartsWith("RENT"))
                {
                    r.RArears = entry.Debit;
                    r.RApart = entry.Particulars;
                }
                else if (particulars.ToUpper().StartsWith("WATER"))
                {
                    r.WArears = entry.Debit;
                    r.WApart = entry.Particulars;
                }
            }
            else
            {
                string amount = "";
                if (particulars.ToUpper().StartsWith("RENT"))
                {
                    particulars = particulars.Substring("RENT".Length).Trim();
                    particulars = particulars.Trim('@').Trim();
                    particulars = SkipChars(particulars);
                    amount = util.ExtractNo(particulars, 5);
                    r.MRent = amount;
                    t.MRent = amount;
                    r.TotalRent = entry.Debit;
                    r.Rpart = entry.Particulars;
                }
                else if (particulars.ToUpper().StartsWith("WATER"))
                {
                    particulars = particulars.Substring("WATER".Length).Trim();
                    particulars = particulars.Trim('@').Trim();
                    particulars = SkipChars(particulars);
                    amount = util.ExtractNo(particulars, 5);
                    r.WCharges = amount;
                    t.WCharges = amount;
                    r.TotalWCharges = entry.Debit;
                    r.Wpart = entry.Particulars;
                }
                particulars = particulars.Substring(amount.Length).Trim(',').Trim();
                particulars = SkipChars(particulars);


                string FDate, TDate;
                util.ExtractDateRange(particulars, out FDate, out TDate);
                r.FDate = FDate;
                r.TDate = TDate;

            }
            /*if (particulars.Length > 0)
            {
                string[] daterange = particulars.Split('-');
                string[] fromdate = daterange[0].Split('/');
                string[] todate = daterange[1].Split('/');
                int fromyear = Convert.ToInt32(fromdate[2].Trim());
                int frommonth = Convert.ToInt32(fromdate[1].Trim());
                int fromday = Convert.ToInt32(fromdate[0].Trim());
                int toyear = Convert.ToInt32(todate[2].Trim());
                int tomonth = Convert.ToInt32(todate[1].Trim());
                int today = Convert.ToInt32(todate[0].Trim());
                DateTime fromdatetime = new DateTime(fromyear, frommonth, fromday);
                DateTime todatetime = new DateTime(toyear, tomonth, today);
                r.FDate = util.FormatDate(fromdatetime);
                r.TDate = util.FormatDate(todatetime);
            }
            else
            {
                throw new TrustException("Unable to get date range");
            }*/

            /*string[] particulars = entry.Particulars.Split(',');
            string[] monthlyrent = particulars[1].Split(' ');
            string renttype = "";
            string rentamount = "";
            if (monthlyrent.Length > 1)
            {
                if (monthlyrent[0].Trim().Length != 0)
                {
                    renttype = monthlyrent[0].Trim();
                    rentamount = monthlyrent[1].Trim();
                }
                else if (monthlyrent.Length > 2)
                {
                    renttype = monthlyrent[1].Trim();
                    rentamount = monthlyrent[2].Trim();
                }
                else
                {
                    throw new TrustException("Unable to get monthly rent");
                }
            }
            else
            {
                throw new TrustException("Unable to get monthly rent");
            }

            if (particulars.Length > 2)
            {
                string[] daterange = particulars[2].Split('-');
                string[] fromdate = daterange[0].Split('/');
                string[] todate = daterange[1].Split('/');
                int fromyear = Convert.ToInt32(fromdate[2].Trim());
                int frommonth = Convert.ToInt32(fromdate[1].Trim());
                int fromday = Convert.ToInt32(fromdate[0].Trim());
                int toyear = Convert.ToInt32(todate[2].Trim());
                int tomonth = Convert.ToInt32(todate[1].Trim());
                int today = Convert.ToInt32(todate[0].Trim());
                DateTime fromdatetime = new DateTime(fromyear, frommonth, fromday);
                DateTime todatetime = new DateTime(toyear, tomonth, today);
                r.FDate = util.FormatDate(fromdatetime);
                r.TDate = util.FormatDate(todatetime);
            }
            else
            {
                throw new TrustException("Unable to get date range");
            }
            int index = rentamount.IndexOf('@');
            index = index + 1;
            string rentamount1 = "";
            if (index < rentamount.Length)
            {
                rentamount1 = rentamount.Substring(index);
            }
            else
            {
                throw new TrustException("Unable to get rent amount");
            }
            if (renttype.Contains("RENT"))
            {
                if (rentamount.Contains("AREARS"))
                {
                    r.RArears = rentamount1;
                    r.RApart = entry.Particulars;
                }
                else
                {
                    r.MRent = rentamount1;
                    r.Rpart = entry.Particulars;
                    r.TotalRent = entry.Debit;
                }
            }
            else if (renttype.Contains("WATER"))
            {
                if (rentamount.Contains("ARERS"))
                {
                    r.WArears = rentamount1;
                    r.WApart = entry.Particulars;
                }
                else
                {
                    r.WCharges = rentamount1;
                    r.Wpart = entry.Particulars;
                    r.TotalWCharges = entry.Debit;
                }
            }*/
            //r.Total =;
            //return r;
        }

        private string SkipChars(string particulars)
        {
            int skipchars = util.SkipChars(particulars);
            if (skipchars > 0 && skipchars < particulars.Length)
            {
                particulars = particulars.Substring(skipchars);
            }
            return particulars;
        }

        /*public Tenant GetTenant(Accounts entry)
        {
            Tenant t = new Tenant();
            //Shop 34, RENT @430, 1/1/2021-30/6/2021
            //Shop 34, WATER @170, 1/1/2021-30/6/2021
            //Shop 34, WATER AREARS JUL-DEC 2020 @60
            string[] particulars = entry.Particulars.Split(',');
            string[] rentspace = particulars[0].Split(' ');
            string spacetype = rentspace[0].Trim();
            string spacenumber = rentspace[1].Trim();
            //string[] monthlyrent = particulars[1].Split(' ');
            //string renttype = monthlyrent[0].Trim();
            //string rentamount = monthlyrent[1].Trim();
            t.Space = spacetype;

            //int index = rentamount.IndexOf('@');
            //string rentamount1 = rentamount.Substring(index);
            //if (renttype.Contains("RENT"))
            //{
                //t.MRent = rentamount1;
            //}
            //else if (renttype.Contains("WATER"))
            //{
                //t.WCharges = rentamount1;
            //}
            t.Name = entry.Name;
            t.No = spacenumber;
            //r.Total =;
            return t;
        }*/

        public void ValidateEntryDate(string Years, Accounts entry)
        {
            DateTime startDate = util.GetStartDate(Years);
            DateTime endDate = util.GetEndDate(Years);
            DateTime entryDate;
            if (DateTime.TryParse(entry.Date, out entryDate))
            {
                if (entryDate < startDate || entryDate > endDate)
                {
                    throw new TrustException("Rent from entry date is not within import period");
                }
            }
            else
            {
                throw new TrustException("Could not parse entry date");
            }
        }

        public Certificate GetCertificate(Accounts entry, string Years)
        {
            //JJ075306 DATED 23-09-2020 FOLIO SSC/401/2020
            Certificate c = new Certificate();

            ValidateEntryDate(Years, entry);

            string[] particulars = entry.Particulars.Split(' ');
            string date = "", folio = "", certificate = "";
            for (int i = 0; i < particulars.Length; i++)
            {
                if (particulars[i] == "DATED")
                {
                    date = particulars[i + 1];
                }
                if (particulars[i] == "FOLIO")
                {
                    folio = particulars[i + 1];
                }
                if (util.IsAlphaNumeric(particulars[i]))
                {
                    certificate = particulars[i];
                }
            }
            if (Convert.ToInt32(entry.Debit) > 0)
            {
                c.Amount = entry.Debit;
                c.SaleDate = entry.Date;
                c.Status = "MATURED";
            }
            else if (Convert.ToInt32(entry.Credit) > 0)
            {
                c.Amount = entry.Credit;
                c.Status = "ACTIVE";
            }
            string[] pdate = date.Split('-');
            DateTime purchasedate = new DateTime(Convert.ToInt32(pdate[2]), Convert.ToInt32(pdate[1]), Convert.ToInt32(pdate[0]));
            c.PurchaseDate = util.FormatDate(purchasedate);
            c.Date = entry.Date;
            c.Folio = folio;
            if (entry.Account == "SSC")
            {
                DateTime maturitydate = new DateTime(Convert.ToInt32(pdate[2]) + 3, Convert.ToInt32(pdate[1]), Convert.ToInt32(pdate[0]));
                c.Maturity = util.FormatDate(maturitydate);
            }
            else if (entry.Account == "DSC" || entry.Account == "BSC")
            {
                DateTime maturitydate = new DateTime(Convert.ToInt32(pdate[2]) + 10, Convert.ToInt32(pdate[1]), Convert.ToInt32(pdate[0]));
                c.Maturity = util.FormatDate(maturitydate);
            }
            c.No = certificate;
            return c;
        }
    }
}
