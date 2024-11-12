using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustApplication.Exceptions;

namespace TrustApplication
{
    class TrustHandler
    {
        Trust T; RentReceipt R; AccountingSheets accSheet;
        Utils util = new Utils();

        public Trust GetTrust(int TrustCode)
        {
            if(TrustCode == 1) {T = new HVHT();}
            else if (TrustCode == 2){T = new HTTT();}
            else if (TrustCode == 3){T = new BIB();}
            else
            {
                throw new TrustHandlerException("Could not find Trust");
            }
            return T;
        }
        public RentReceipt GetRentTrust(int TrustCode)
        {
            if (TrustCode == 1){R = new HVHTRent();}
            else if (TrustCode == 2){R = new HTTTRent();}
            else
            {
                throw new TrustHandlerException("Could not find rent Trust");
            }
            return R;
        }
        public AccountingSheets GetSheet(int TrustCode)
        {
            if (TrustCode == 1){accSheet = new HVHTAccountingSheet();}
            else if (TrustCode == 2){accSheet = new HTTTAccountingSheet();}
            else if (TrustCode == 3){accSheet = new BIBAccountingSheet();}
            else
            {
                throw new TrustHandlerException("Could not find Trust accounting sheet");
            }
            return accSheet;
        }
        public string GetCashAccount(int TrustCode)
        {
            return "CASH";
        }
        public AccountingSheets GetYearlyStatement(int TrustCode)
        {
            if (TrustCode == 1){accSheet = new HVHTYearlyStatement();}
            else if (TrustCode == 2){accSheet = new HTTTYearlyStatement();}
            else if (TrustCode == 3){accSheet = new BIBYearlyStatement();}
            else
            {
                throw new TrustHandlerException("Could not find yearly Trust sheet");
            }
            return accSheet;
        }

        public List<string> GetCertificates(int TrustCode)
        {
            Trust t = GetTrust(TrustCode);
            return t.GetCertificates();
        }
        public List<Accounts> GetCertificateProfit(int TrustCode, string CertList, string ProfitAmount, string TaxAmount,
            string FolioNo, string Date)
        {
            Trust t = GetTrust(TrustCode);
            return t.GetCertificateProfit(CertList, ProfitAmount, TaxAmount, FolioNo, Date);
        }
        public List<Accounts> GetCertificateMatured(int TrustCode, string Type, string CertList, string CertificateAmount, 
            string FolioNo, string Date)
        {
            Trust t = GetTrust(TrustCode);
            return t.GetCertificateMatured(CertList, Type, CertificateAmount, FolioNo, Date);
        }
        public List<Accounts> GetCertificatePurchased(int TrustCode, string Type, string CertificateNo, string CertificateAmount,
            string FolioNo, string Date)
        {
            Trust t = GetTrust(TrustCode);
            return t.GetCertificatePurchased(CertificateNo, Type, CertificateAmount, FolioNo, Date);
        }
        public List<Accounts> GetOpeningEntries(int TrustCode, LoadAccountsBL lbl, string newdate)
        {
            Trust t = GetTrust(TrustCode);
            return t.GetOpeningEntries(lbl, newdate);
        }
        public List<Accounts> GetClosingEntries(int TrustCode, LoadAccountsBL lbl, string newdate)
        {
            Trust t = GetTrust(TrustCode);
            return t.GetClosingEntries(lbl, newdate);
        }
        public void PrintReceipt(int TrustCode, string RentDate, string RentSerialNo, int RentTrustCode, string RentTrustPlotCode,
            string RentSpaceType, string RentSpaceNo, string MonthlyRent, string MonthlyWater, string RentTenantName,
            string RentArears, string WaterArears, string RentTotalRent, string RentTotalWaterCharges, string RentTotalAmount,
            DateTime FromDate, DateTime ToDate, string CNIC)
        {
            RentReceipt r = GetRentTrust(TrustCode);
            r.PrintReceipt(RentDate, RentSerialNo, RentTrustCode, RentTrustPlotCode,
            RentSpaceType, RentSpaceNo, MonthlyRent, MonthlyWater, RentTenantName,
            RentArears, WaterArears, RentTotalRent, RentTotalWaterCharges, RentTotalAmount,
            FromDate, ToDate, CNIC);
        }
        public string GetRentAccounts(int TrustCode, string PlotNo)
        {
            Trust t = GetTrust(TrustCode);
            return t.GetRentAccount(PlotNo, 1);
        }
        public string GetRentParticulars(string RentSpaceType, string RentSpaceNo, string MonthlyRent, int FromM, string FromY,
            int ToM, string ToY)
        {
            double RentPerMonth = util.GetAmount(MonthlyRent);
            DateTime FromDate = util.ConstructFromDate(FromY, FromM);
            DateTime ToDate = util.ConstructToDate(ToY, ToM);
            string formattedstartdate = util.FormatDate2(FromDate);
            string formattedenddate = util.FormatDate2(ToDate);
            string rentparticulars = RentSpaceType + " " + RentSpaceNo + ", " + "RENT @" + RentPerMonth + ", "
                + formattedstartdate + "-" + formattedenddate;
            return rentparticulars;
        }

        public string GetRentArearsParticulars(string RentSpaceType, string RentSpaceNo, int FromM, string FromY,
            int ToM, string ToY)
        {
            DateTime FromDate = util.ConstructFromDate(FromY, FromM);
            DateTime ToDate = util.ConstructToDate(ToY, ToM);
            string rentarearsparticulars = RentSpaceType + " " + RentSpaceNo + ", " + "RENT AREARS";
            return rentarearsparticulars;
        }

        public string GetWaterParticulars(string RentSpaceType, string RentSpaceNo, string WaterPerMonth, int FromM, string FromY,
            int ToM, string ToY)
        {
            double RentPerMonth = util.GetAmount(WaterPerMonth);
            DateTime FromDate = util.ConstructFromDate(FromY, FromM);
            DateTime ToDate = util.ConstructToDate(ToY, ToM);
            string formattedstartdate = util.FormatDate2(FromDate);
            string formattedenddate = util.FormatDate2(ToDate);
            string waterparticulars = RentSpaceType + " " + RentSpaceNo + ", " + "WATER @" + WaterPerMonth + ", "
                + formattedstartdate + "-" + formattedenddate;
            return waterparticulars;
        }

        public string GetWaterArearsParticulars(string RentSpaceType, string RentSpaceNo, int FromM, string FromY,
            int ToM, string ToY)
        {
            DateTime FromDate = util.ConstructFromDate(FromY, FromM);
            DateTime ToDate = util.ConstructToDate(ToY, ToM);
            string waterarearsparticulars = RentSpaceType + " " + RentSpaceNo + ", " + "WATER AREARS";
            return waterarearsparticulars;
        }
        public List<Accounts> GetRentEntries(int TrustCode, string Plotcode, string Date, string SerialNo, string Name,
            string RentParticulars, string RentTotalRent, string WaterParticulars, string RentTotalWaterCharges, string RentArearsParticulars,
            double RArears, string WaterArearsParticulars, double WArears)
        {
            Trust t = GetTrust(TrustCode);
            return t.GetRentEntries(Plotcode, Date, SerialNo, Name, RentParticulars, RentTotalRent, WaterParticulars, 
                RentTotalWaterCharges, RentArearsParticulars, RArears, WaterArearsParticulars, WArears);
        }
        public Dictionary<string, string> GetYearlyTotals(int TrustCode, LoadAccountsBL lbl)
        {
            Trust t = GetTrust(TrustCode);
            return t.GetYearlyTotals(lbl);
        }

        public List<Accounts> GetTrialBalance(int TrustCode, LoadAccountsBL lbl)
        {
            Trust t = GetTrust(TrustCode);
            return t.GetTrialBalance(lbl);
        }

        public string GetAccountOrder(string Particulars)
        {
            if (Particulars.Contains("OPENING"))
                return "1";
            else
                return "2";
        }

        public string GetAccountKey(int TrustCode, string ContraAccount, string Particulars)
        {
            Trust t = GetTrust(TrustCode);
            return t.GetAccountKey(ContraAccount, Particulars);
        }

        public bool IsRent(int TrustCode, string AccountCode)
        {
            Trust t = GetTrust(TrustCode);
            return t.IsRent(AccountCode);
        }

        public bool IsCertificate(int TrustCode, string AccountCode)
        {
            Trust t = GetTrust(TrustCode);
            return t.IsCertificate(AccountCode);
        }
        public void GetRent(int TrustCode, Accounts entry, string Years, out Tenant t, out Rent r)
        {
            Trust tr = GetTrust(TrustCode);
            tr.GetRent(entry, Years, out t, out r);
        }
        /*public Tenant GetTenant(int TrustCode, Accounts entry)
        {
            Trust t = GetTrust(TrustCode);
            return t.GetTenant(entry);
        }*/
        public Certificate GetCertificate(int TrustCode, Accounts entry, string Years)
        {
            Trust t = GetTrust(TrustCode);
            return t.GetCertificate(entry, Years);
        }
    }
}
