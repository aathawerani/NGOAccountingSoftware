using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustApplication.Exceptions;

namespace TrustApplication
{
    class RentBL : BL
    {
        RentDAL dal = new RentDAL(); 
        ReceivablesBL rbl = new ReceivablesBL();

        public double CalculateArears(string RentArears, string WaterArears)
        {
            double RArears;
            if (RentArears == "")
                return 0;
            if (!double.TryParse(RentArears, out RArears))
            {
                throw new RentBLException("Could not parse rent arears");
            }
            double WArears;
            if (WaterArears == "")
                return RArears;
            if (!double.TryParse(WaterArears, out WArears))
            {
                throw new RentBLException("Could not parse water arears");
            }
            return RArears + WArears;
        }
        public double CalculateRent(string MonthlyRent, int NumMonths)
        {
            double RentPerMonth;
            if (MonthlyRent == "")
                return 0;
            if (!double.TryParse(MonthlyRent, out RentPerMonth))
            {
                throw new RentBLException("Could not parse rent arears");
            }
            double TotalRent = RentPerMonth * NumMonths;
            return TotalRent;
        }
        public double CalculateWater(string MonthlyWater, int NumMonths)
        {
            double WaterPerMonth;
            if (MonthlyWater == "")
                return 0;
            if (!double.TryParse(MonthlyWater, out WaterPerMonth))
            {
                throw new RentBLException("Could not calculate water charges");
            }
            double TotalWater = WaterPerMonth * NumMonths;
            return TotalWater;
        }
        public void PrintReceipt(string RentDate, string RentSerialNo, int RentTrustCode, string RentTrustPlotCode,
            string RentSpaceType, string RentSpaceNo, string MonthlyRent, string MonthlyWater, string RentTenantName,
            string RentArears, string WaterArears, string RentTotalRent, string RentTotalWaterCharges, string RentTotalAmount,
            int FromM, string FromY, int ToM, string ToY, int NumMonths, string CNIC)
        {
            int FrY, TY;
            if (!int.TryParse(FromY, out FrY))
            {
                throw new RentBLException("Could not parse rent arears");
            }
            int.TryParse(ToY, out TY);
            DateTime FromDate = new DateTime(FrY, FromM, 1);
            DateTime ToDate = new DateTime(TY, ToM, DateTime.DaysInMonth(TY, ToM));
            th.PrintReceipt(RentTrustCode, RentDate, RentSerialNo, RentTrustCode, RentTrustPlotCode,
                                    RentSpaceType, RentSpaceNo, MonthlyRent, MonthlyWater, RentTenantName,
                                    RentArears, WaterArears, RentTotalRent, RentTotalWaterCharges, RentTotalAmount,
                                    FromDate, ToDate, CNIC);
            //string PlotNo = dal.GetPlotNoByPlotCode(RentTrustCode.ToString(), RentTrustPlotCode.ToString()).First();
        }

        public Dictionary<string, string> GetRentContraAccounts(int RentTrustCode, string RentTrustPlotCode)
        {
            string rentaccount = th.GetRentAccounts(RentTrustCode, RentTrustPlotCode);
            return dal.GetContraAccounts(RentTrustCode.ToString(), rentaccount);
        }

        public string GetRentParticulars(string RentSpaceType, string RentSpaceNo, string MonthlyRent, int FromM, string FromY,
            int ToM, string ToY)
        {
            return th.GetRentParticulars(RentSpaceType, RentSpaceNo, MonthlyRent, FromM, FromY, ToM, ToY);
        }

        public string GetRentArearsParticulars(string RentSpaceType, string RentSpaceNo, int FromM, string FromY,
            int ToM, string ToY)
        {
            return th.GetRentArearsParticulars(RentSpaceType, RentSpaceNo, FromM, FromY, ToM, ToY);
        }

        public string GetWaterParticulars(string RentSpaceType, string RentSpaceNo, string WaterPerMonth, int FromM, string FromY,
            int ToM, string ToY)
        {
            return th.GetWaterParticulars(RentSpaceType, RentSpaceNo, WaterPerMonth, FromM, FromY, ToM, ToY);
        }

        public string GetWaterArearsParticulars(string RentSpaceType, string RentSpaceNo, int FromM, string FromY,
            int ToM, string ToY)
        {
            return th.GetWaterArearsParticulars(RentSpaceType, RentSpaceNo, FromM, FromY, ToM, ToY);
        }

        public void InsertRent(string RentDate, string RentSerialNo, int RentTrustCode, int RentTrustPlotCode, string RentTrustPlotAccountCode,
            string RentSpaceType, string RentSpaceNo, string MonthlyRent, string MonthlyWater, string RentTenantName,
            string RentArears, string WaterArears, string RentTotalRent, string RentTotalWaterCharges, string RentTotalAmount,
            int FromM, string FromY, int ToM, string ToY, int NumMonths, string CNIC, Object PaymentModeItem,
            string RentParticulars, string WaterParticulars, string RentArearsParticulars, string WaterArearsParticulars,
            string BankReference, string Rent_TotalPaid_textBox)
        {
            List<string> receipts = GetReceiptNo(RentTrustCode.ToString(), RentSerialNo);
            //dal.GetReceiptNo(RentTrustCode.ToString(), RentSerialNo);
            if (receipts.Count > 0)
            {
                throw new RentBLException("Duplicate receipt");
            }

            double RentPerMonth = util.GetAmount(MonthlyRent);
            double WaterPerMonth = util.GetAmount(MonthlyWater);
            double TotalRent = util.GetAmount(RentTotalRent);
            double TotalWater = util.GetAmount(RentTotalWaterCharges);
            double RArears = util.GetAmount(RentArears);
            double WArears = util.GetAmount(WaterArears);
            double TotalAmount = util.GetAmount(RentTotalAmount);
            DateTime FromDate = util.ConstructFromDate(FromY, FromM);
            DateTime ToDate = util.ConstructToDate(ToY, ToM);

            /*string RentParticulars = _RentParticulars;
            if (BankReference != "")
            {
                RentParticulars = _RentParticulars + ", BANK REFERENCE NO." + BankReference;
            }*/
            string rentdate = util.FormatDate(RentDate);
            string fromdate = util.FormatDate(FromDate);
            string todate = util.FormatDate(ToDate);
            InsertRent(rentdate, RentSerialNo, RentTrustCode.ToString(), RentTrustPlotCode.ToString(),
                RentSpaceType, RentSpaceNo, RentPerMonth.ToString(), WaterPerMonth.ToString(), RentTenantName.ToUpper(),
                fromdate, todate, RArears.ToString(), WArears.ToString(), TotalRent.ToString(),
                TotalWater.ToString(), TotalAmount.ToString(), RentParticulars.ToUpper(), 
                WaterParticulars.ToUpper(), 
                RentArearsParticulars, WaterArearsParticulars.ToUpper());

            List<Accounts> accountlist = th.GetRentEntries(RentTrustCode, RentTrustPlotAccountCode, RentDate, RentSerialNo, RentTenantName,
                RentParticulars, RentTotalRent, WaterParticulars, RentTotalWaterCharges, RentArearsParticulars, RArears,
                WaterArearsParticulars, WArears);
            AccountsDualInsert(RentTrustCode, accountlist);

            UpdateTenant(RentTenantName.ToUpper(), RentPerMonth.ToString(), WaterPerMonth.ToString(), ToM.ToString(), ToY.ToString(),
                RentTrustCode.ToString(), RentTrustPlotCode.ToString(), RentSpaceType, RentSpaceNo, CNIC);

            if (Rent_TotalPaid_textBox == "")
            {
                rbl.InsertReceivable(RentTrustCode.ToString(), rentdate, RentSerialNo, RentTenantName, 
                    RentParticulars, TotalAmount.ToString());
            }
            else
            {
                double TotalPaid = util.GetAmount(Rent_TotalPaid_textBox);
                if (TotalPaid < TotalAmount)
                {
                    double remaining = TotalAmount - TotalPaid;
                    rbl.InsertReceivable(RentTrustCode.ToString(), rentdate, RentSerialNo, RentTenantName, 
                        RentParticulars, remaining.ToString());
                }
            }
        }

        public void InsertRent(string RentDate, string RentSerialNo, string RentTrustCode, string RentTrustPlotCode, 
            string RentSpaceType, string RentSpaceNo, string RentPerMonth, string WaterPerMonth, string RentTenantName,
            string RentArears, string WaterArears, string RentTotalRent, string RentTotalWaterCharges, string RentTotalAmount,
            string FromDate, string ToDate, string RentParticulars, string WaterParticulars, string RentArearsParticulars, string WaterArearsParticulars)
        {
             dal.InsertRent(RentDate, RentSerialNo, RentTrustCode, RentTrustPlotCode, 
                RentSpaceType, RentSpaceNo, RentPerMonth, WaterPerMonth, RentTenantName,
                FromDate, ToDate, RentArears, WaterArears, RentTotalRent,
                RentTotalWaterCharges, RentTotalAmount, RentParticulars,
                WaterParticulars.ToUpper(),
                RentArearsParticulars, WaterArearsParticulars);
        }

        public List<Accounts> GetAccountsByRentReceipt(int TrustCode, string ReceiptNo)
        {
            DataSet ds = dal.GetAccountsByRentReceipt(TrustCode.ToString(), ReceiptNo);
            return GetAccounts(ds);
        }

        public void UpdateTenant(string RentTenantName, string RentPerMonth, string WaterPerMonth, string ToM, string ToY,
            string RentTrustCode, string RentTrustPlotCode, string RentSpaceType, string RentSpaceNo, string CNIC)
        {
            dal.UpdateTenant(RentTenantName.ToUpper(), RentPerMonth, WaterPerMonth, ToM, ToY,
                RentTrustCode, RentTrustPlotCode, RentSpaceType, RentSpaceNo, CNIC);
        }

        public void UpdateRent(string RentDate, string RentSerialNo, string RentPerMonth, string RentWaterChargesPerMonth, string RentTenantName,
            string RentFromDate, string RentToDate, string RentArears, string RentWaterChargesArears, string RentTotalRent, string RentTotalWaterCharges,
            string RentTotalAmount, string ID, string RentParticulars, string WaterParticulars, string RentArearsParticulars, string WaterArearsParticulars)
        {
            dal.UpdateRent(RentDate, RentSerialNo, RentPerMonth, RentWaterChargesPerMonth, RentTenantName,
                RentFromDate, RentToDate, RentArears, RentWaterChargesArears, RentTotalRent,
                RentTotalWaterCharges, RentTotalAmount, ID, RentParticulars, WaterParticulars, RentArearsParticulars,
                WaterArearsParticulars);
        }

        public void UpdateRent(string RentDate, string RentSerialNo, int RentTrustCode, int RentTrustPlotCode, string RentTrustPlotAccountCode,
            string RentSpaceType, string RentSpaceNo, string MonthlyRent, string MonthlyWater, string RentTenantName,
            string RentArears, string WaterArears, string RentTotalRent, string RentTotalWaterCharges, string RentTotalAmount,
            int FromM, string FromY, int ToM, string ToY, int NumMonths, string CNIC, string ID, Object PaymentModeItem, 
            string RentParticulars, string WaterParticulars, string RentArearsParticulars, string WaterArearsParticulars,
            string BankReference)
        {
            double RentPerMonth;
            if (!double.TryParse(MonthlyRent, out RentPerMonth))
            {
                throw new RentBLException("Could not parse monthly rent charges");
            }
            double WaterPerMonth;
            if (!double.TryParse(MonthlyWater, out WaterPerMonth))
            {
                throw new RentBLException("Could not parse monthly water charges");
            }
            double TotalRent; 
            if (!double.TryParse(RentTotalRent, out TotalRent))
            {
                throw new RentBLException("Could not parse total rent");
            }
            double TotalWater; 
            if (!double.TryParse(RentTotalWaterCharges, out TotalWater))
            {
                throw new RentBLException("Could not parse total water charges");
            }
            double RArears; 
            if (!double.TryParse(RentArears, out RArears))
            {
                throw new RentBLException("Could not parse rent arears");
            }
            double WArears; 
            if (!double.TryParse(WaterArears, out WArears))
            {
                throw new RentBLException("Could not parse water arears");
            }
            double TotalAmount; 
            if (!double.TryParse(RentTotalAmount, out TotalAmount))
            {
                throw new RentBLException("Could not parse Total Amount");
            }
            int FrY, TY;
            int.TryParse(FromY, out FrY);
            int.TryParse(ToY, out TY);
            DateTime FromDate = new DateTime(FrY, FromM, 1);
            DateTime ToDate = new DateTime(TY, ToM, DateTime.DaysInMonth(TY, ToM));

            string rentdate = util.FormatDate(RentDate);
            string fromdate = util.FormatDate(FromDate);
            string todate = util.FormatDate(ToDate);
            UpdateRent(rentdate, RentSerialNo, RentPerMonth.ToString(), WaterPerMonth.ToString(), RentTenantName,
                fromdate, todate, RArears.ToString(), WArears.ToString(), TotalRent.ToString(),
                TotalWater.ToString(), TotalAmount.ToString(), ID, RentParticulars, WaterParticulars, RentArearsParticulars,
                WaterArearsParticulars);

            AccountsDelete(GetAccountsByRentReceipt(RentTrustCode, RentSerialNo));

            List<Accounts> listaccounts = th.GetRentEntries(RentTrustCode, RentTrustPlotAccountCode, RentDate, RentSerialNo, RentTenantName,
                RentParticulars, RentTotalRent, WaterParticulars, RentTotalWaterCharges, RentArearsParticulars, RArears,
                WaterArearsParticulars, WArears);
            AccountsDualInsert(RentTrustCode, listaccounts);

            UpdateTenant(RentTenantName, RentPerMonth.ToString(), WaterPerMonth.ToString(), ToM.ToString(), ToY.ToString(),
                RentTrustCode.ToString(), RentTrustPlotCode.ToString(), RentSpaceType, RentSpaceNo, CNIC);
        }

        public List<Rent> GetRent(string TrustCode, string PlotCode, string SpaceType, string SpaceNo)
        {
            return Rent.GetRent(dal.GetRent(TrustCode, PlotCode, SpaceType, SpaceNo));
        }

        public string GetNextReceiptNo(string TrustCode)
        {
            string receiptno = "";
            List<string> receipts = dal.GetLastReceiptNo(TrustCode);
            string temp = receipts.FirstOrDefault();
            int num;
            if (temp.StartsWith("RR"))
            {
                if (!int.TryParse(temp.Substring(2), out num))
                {
                    throw new RentBLException("Failed to parse receipt number");
                }
                receiptno = "RR" + util.GetSerialNumber(num);
            }
            else
            {
                if (!int.TryParse(temp, out num))
                {
                    throw new RentBLException("Could not parse receipt number");
                }
                receiptno = util.GetSerialNumber(num);
            }
            return receiptno;
        }

        public List<string> GetTenantInfo(string TrustCode, string PlotCode, string SpaceType, string SpaceNo)
        {
            return dal.GetTenantInfo(TrustCode, PlotCode, SpaceType, SpaceNo);
        }
        public List<string> GetTenantInfo(string TrustCode, string PlotCode)
        {
            return dal.GetTenantInfo(TrustCode, PlotCode);
        }
        public List<string> GetPlots(string TrustCode)
        {
            return dal.GetPlots(TrustCode);
        }
        public List<string> GetSpaceType(string TrustCode, string PlotCode)
        {
            return dal.GetSpaceType(TrustCode, PlotCode);
        }
        public List<string> GetSpaceNo(string TrustCode, string PlotCode, string SpaceType)
        {
            return dal.GetSpaceNo(TrustCode, PlotCode, SpaceType);
        }
        //public Dictionary<string, string> GetContraAccounts(string TrustCode, string AccountCode)
        //{
            //return bl.GetContraAccounts(TrustCode, AccountCode);
        //}
        public string GetBankReference(string RentParticulars)
        {
            string BankRefernece = "";
            string value = "Bank Reference No.";
            if (RentParticulars.Contains(value))
            {
                int offset = RentParticulars.IndexOf("Bank Reference No.");
                offset += value.Length;
                BankRefernece = RentParticulars.Substring(offset);
            }
            return BankRefernece;
        }
        public List<Tenant> GetTenants(string TrustCode, string PlotCode)
        {
            return Tenant.GetTenant(dal.GetTenants(TrustCode, PlotCode));
        }

        public List<string> GetReceiptNo(string TrustCode, string ReceiptNo)
        {
            return dal.GetReceiptNo(TrustCode, ReceiptNo);
        }
        public DataSet GetReceiptEntry(string TrustCode, string ReceiptNo)
        {
            return dal.GetReceiptEntry(TrustCode, ReceiptNo);
        }
        public int CalculateNumMonths(object FromM, object FromY, object ToM, object ToY)
        {
            return util.CalculateNumMonths((int)FromM, int.Parse((string)FromY), (int)ToM, int.Parse((string)ToY));
        }
    }
}
