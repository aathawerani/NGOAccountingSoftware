using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication
{
    class HTTT : Trust
    {
        public int GetDepreciation(LoadAccountsBL lbl, string Plot)
        {
            int dgk = (int)(Math.Ceiling(lbl.GetAccountTotal(Plot) * 2.5 / 100));
            return dgk;
        }

        public int GetIncome(LoadAccountsBL lbl)
        {
            return lbl.GetAccountTotal("R46GK7") + lbl.GetAccountTotal("R2BR1") + lbl.GetAccountTotal("R4BR1") +
                    lbl.GetAccountTotal("R21BR1") + lbl.GetAccountTotal("W46GK7") + lbl.GetAccountTotal("W2BR1") +
                    lbl.GetAccountTotal("W4BR1") + lbl.GetAccountTotal("W21BR1");
        }

        public int GetTotalDepreciation(LoadAccountsBL lbl)
        {
            return GetDepreciation(lbl, "B46GK7") + GetDepreciation(lbl, "B2BR1") + GetDepreciation(lbl, "B4BR1") 
                + GetDepreciation(lbl, "B21BR1");
        }

        public int GetTotalPTaxes(LoadAccountsBL lbl)
        {
            return lbl.GetAccountTotal("PT4BR1") + lbl.GetAccountTotal("PT46GK7") + lbl.GetAccountTotal("PT2BR1") +
                    lbl.GetAccountTotal("PT21BR1") + lbl.GetAccountTotal("WHT");
        }
        public int GetTotalWTaxes(LoadAccountsBL lbl)
        { 
            return lbl.GetAccountTotal("WT4BR1") + lbl.GetAccountTotal("WT46GK7") + lbl.GetAccountTotal("WT2BR1") +
                    lbl.GetAccountTotal("WT21BR1");
        }

        public int GetTotalWCharges(LoadAccountsBL lbl)
        {
            return lbl.GetAccountTotal("W46GK7") + lbl.GetAccountTotal("W2BR1") + lbl.GetAccountTotal("W4BR1") 
                + lbl.GetAccountTotal("W21BR1");
        }

        public int GetOPEX(LoadAccountsBL lbl)
        {
            return GetTotalPTaxes(lbl) + GetTotalWTaxes(lbl) + GetTotalDepreciation(lbl) + 
                lbl.GetAccountTotal("M&S") + lbl.GetAccountTotal("LEGAL") + lbl.GetAccountTotal("AUD1");
        }

        public int GetSurplus(LoadAccountsBL lbl)
        {
            return GetIncome(lbl) - GetOPEX(lbl);
        }

        public int GetUpdatedCharity(LoadAccountsBL lbl, string AccountType)
        {
            int charamount = GetSurplus(lbl);
            charamount /= 2;
            int charpaid = lbl.GetTotalDebit(AccountType);
            return lbl.GetAccountTotal(AccountType) + charamount - charpaid;
        }
        public int GetCharity(LoadAccountsBL lbl)
        {
            int charamount = GetSurplus(lbl);
            charamount /= 2;
            return charamount;
        }

        public int GetUpdatedGF(LoadAccountsBL lbl)
        {
            return lbl.GetAccountTotal("GF") + GetSurplus(lbl);
        }

        public int GetGF(LoadAccountsBL lbl)
        {
            return GetSurplus(lbl);
        }
        public int GetUpdatedFA(LoadAccountsBL lbl)
        {
            return lbl.GetAccountTotal("L46GK7") + lbl.GetAccountTotal("L2BR1")
                    + lbl.GetAccountTotal("L4BR1") + lbl.GetAccountTotal("L21BR1")
                    + lbl.GetAccountTotal("B46GK7") + lbl.GetAccountTotal("B2BR1")
                    + lbl.GetAccountTotal("B4BR1") + lbl.GetAccountTotal("B21BR1") - GetTotalDepreciation(lbl);
        }

        public int GetInvestments(LoadAccountsBL lbl)
        {
            return lbl.GetAccountTotal("SSC") + lbl.GetAccountTotal("DSC");
        }

        public override List<Accounts> GetOpeningEntries(LoadAccountsBL lbl, string newdate)
        {
            int dep1 = GetDepreciation(lbl, "B46GK7");
            int dep2 = GetDepreciation(lbl, "B2BR1");
            int dep3 = GetDepreciation(lbl, "B4BR1");
            int dep4 = GetDepreciation(lbl, "B21BR1");

            List<Accounts> openingentries = new List<Accounts>();
            CreateEntry("CASH", lbl.GetAccountTotal("CASH").ToString(), "0", newdate);
            CreateEntry("BANK", lbl.GetAccountTotal("BANK").ToString(), "0", newdate);
            CreateEntry("L46GK7", lbl.GetAccountTotal("L46GK7").ToString(), "0", newdate);
            CreateEntry("L2BR1", lbl.GetAccountTotal("L2BR1").ToString(), "0", newdate);
            CreateEntry("L4BR1", lbl.GetAccountTotal("L4BR1").ToString(), "0", newdate);
            CreateEntry("L21BR1", lbl.GetAccountTotal("L21BR1").ToString(), "0", newdate);
            CreateEntry("B46GK7", (lbl.GetAccountTotal("B46GK7") - dep1).ToString(), "0", newdate);
            CreateEntry("B2BR1", (lbl.GetAccountTotal("B2BR1") - dep2).ToString(), "0", newdate);
            CreateEntry("B4BR1", (lbl.GetAccountTotal("B4BR1") - dep3).ToString(), "0", newdate);
            CreateEntry("B21BR1", (lbl.GetAccountTotal("B21BR1") - dep4).ToString(), "0", newdate);

            CreateEntry("GF", "0", (GetUpdatedGF(lbl)).ToString(), newdate);

            return openingentries;
        }

        public override List<Accounts> GetClosingEntries(LoadAccountsBL lbl, string newdate)
        {
            List<Accounts> yearlyentries = new List<Accounts>();
            yearlyentries.Add(CreateEntry("B46GK7", "DEP", "0", GetDepreciation(lbl, "B46GK7").ToString(), "DEP", "DEPRECIATION EXPENSE", newdate));
            yearlyentries.Add(CreateEntry("DEP", "B46GK7", GetDepreciation(lbl, "B46GK7").ToString(), "0", "DEP", "DEPRECIATION EXPENSE", newdate));

            yearlyentries.Add(CreateEntry("B2BR1", "DEP", "0", GetDepreciation(lbl, "B2BR1").ToString(), "DEP", "DEPRECIATION EXPENSE", newdate));
            yearlyentries.Add(CreateEntry("DEP", "B2BR1", GetDepreciation(lbl, "B2BR1").ToString(), "0", "DEP", "DEPRECIATION EXPENSE", newdate));

            yearlyentries.Add(CreateEntry("B4BR1", "DEP", "0", GetDepreciation(lbl, "B4BR1").ToString(), "DEP", "DEPRECIATION EXPENSE", newdate));
            yearlyentries.Add(CreateEntry("DEP", "B4BR1", GetDepreciation(lbl, "B4BR1").ToString(), "0", "DEP", "DEPRECIATION EXPENSE", newdate));

            yearlyentries.Add(CreateEntry("B21BR1", "DEP", "0", GetDepreciation(lbl, "B21BR1").ToString(), "DEP", "DEPRECIATION EXPENSE", newdate));
            yearlyentries.Add(CreateEntry("DEP", "B21BR1", GetDepreciation(lbl, "B21BR1").ToString(), "0", "DEP", "DEPRECIATION EXPENSE", newdate));

            yearlyentries.Add(CreateEntry("GF", "", "0", GetGF(lbl).ToString(), "GF", "FROM INCOME AND EXPENDITURE", newdate));
            return yearlyentries;

        }
        public override string GetRentAccount(string PlotNo, int type)
        {
            if(PlotNo == "46GK7")
            {
                return (type==1) ? "R46GK7" : "W46GK7";
            }
            else if (PlotNo == "2BR1")
            {
                return (type == 1) ? "R2BR1" : "W2BR1";
            }
            else if (PlotNo == "4BR1")
            {
                return (type == 1) ? "R4BR1" : "W4BR1";
            }
            else if (PlotNo == "21BR1")
            {
                return (type == 1) ? "R21BR1" : "W21BR1";
            }
            return "";
        }
        public override List<Accounts> GetRentEntries(string Plotcode, string Date, string SerialNo, string Name,
            string RentParticulars, string RentTotalRent, string WaterParticulars, string RentTotalWaterCharges, string RentArearsParticulars,
            double RArears, string WaterArearsParticulars, double WArears)
        {
            List<Accounts> rententries = new List<Accounts>();
            rententries.Add(CreateEntry(GetRentAccount(Plotcode, 1), "CASH", "0", RentTotalRent, "RENT", RentParticulars, Date, Name, SerialNo));
            rententries.Add(CreateEntry(GetRentAccount(Plotcode, 2), "CASH", "0", RentTotalWaterCharges, "RENT", WaterParticulars, Date, Name, SerialNo));
            if (RArears > 0)
            {
                rententries.Add(CreateEntry(GetRentAccount(Plotcode, 1), "CASH", "0", RArears.ToString(), "AREARS", RentArearsParticulars, Date, Name, SerialNo));
            }
            if (WArears > 0)
            {
                rententries.Add(CreateEntry(GetRentAccount(Plotcode, 2), "CASH", "0", WArears.ToString(), "AREARS", WaterArearsParticulars, Date, Name, SerialNo));
            }
            return rententries;
        }

        public override Dictionary<string, string> GetYearlyTotals(LoadAccountsBL lbl)
        {
            Dictionary<string, string> statement = new Dictionary<string, string>();
            statement.Add("GFIS", GetUpdatedGF(lbl).ToString());
            statement.Add("SSCIS", lbl.GetAccountTotal("SSC").ToString());
            statement.Add("DSCIS", lbl.GetAccountTotal("DSC").ToString());
            statement.Add("CASHIS", lbl.GetAccountTotal("CASH").ToString());
            statement.Add("BANKIS", lbl.GetAccountTotal("BANK").ToString());
            statement.Add("R21BR1IS", lbl.GetAccountTotal("R21BR1").ToString());
            statement.Add("R4BR1IS", lbl.GetAccountTotal("R4BR1").ToString());
            statement.Add("R2BR1IS", lbl.GetAccountTotal("R2BR1").ToString());
            statement.Add("R46GK7IS", lbl.GetAccountTotal("R46GK7").ToString());
            statement.Add("PTAXES", GetTotalPTaxes(lbl).ToString());
            statement.Add("WTAXES", GetTotalWTaxes(lbl).ToString());
            statement.Add("WCHARGES", GetTotalWCharges(lbl).ToString());
            statement.Add("DEP", GetTotalDepreciation(lbl).ToString());
            statement.Add("M&SIS", lbl.GetAccountTotal("M&S").ToString());
            statement.Add("LEGALIS", lbl.GetAccountTotal("LEGAL").ToString());
            statement.Add("AUD1IS", lbl.GetAccountTotal("AUD1").ToString());
            statement.Add("PROFITIS", lbl.GetAccountTotal("PROFIT").ToString());

            statement.Add("FUNDBS", GetUpdatedGF(lbl).ToString());
            statement.Add("FABS", GetUpdatedFA(lbl).ToString());
            statement.Add("INVESTBS", GetInvestments(lbl).ToString());
            statement.Add("CASHBS", lbl.GetAccountTotal("CASH").ToString());
            statement.Add("AUD2", (0 - lbl.GetAccountTotal("AUD2")).ToString());

            return statement;
        }

        public override List<Accounts> GetTrialBalance(LoadAccountsBL lbl)
        {
            throw new NotImplementedException();
        }

        public override string GetAccountKey(string ContraAccount, string Particulars)
        {
            throw new NotImplementedException();
        }

        public override bool IsRent(string AccountCode)
        {
            throw new NotImplementedException();
        }

        public override bool IsCertificate(string AccountCode)
        {
            throw new NotImplementedException();
        }
    }
}
