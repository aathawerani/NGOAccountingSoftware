using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication
{
    class BIB : Trust
    {
        public int GetDepreciation(LoadAccountsBL lbl, string Plot)
        {
            int dgk = (int)(Math.Ceiling(lbl.GetAccountTotal(Plot) * 2.5 / 100));
            return dgk;
        }

        public int GetTotalDonation(LoadAccountsBL lbl)
        {
            return lbl.GetAccountTotal("DOT1") + lbl.GetAccountTotal("DOT2") + lbl.GetAccountTotal("BOX");
        }

        public int GetIncome(LoadAccountsBL lbl)
        {
                return lbl.GetAccountTotal("DOT1") + lbl.GetAccountTotal("DOT2")
                                    + lbl.GetAccountTotal("BOX") + lbl.GetAccountTotal("M-SUB")
                                    + lbl.GetAccountTotal("RENT") + lbl.GetAccountTotal("LOUD")
                                    + lbl.GetAccountTotal("WSC");
        }

        public int GetTotalDepreciation(LoadAccountsBL lbl)
        {
            return GetDepreciation(lbl, "BLDG") + GetDepreciation(lbl, "F&F") + GetDepreciation(lbl, "L-INST");
        }
        public int GetOPEX(LoadAccountsBL lbl)
        {
                return lbl.GetAccountTotal("M-EXP") + lbl.GetAccountTotal("R&M")
                    + lbl.GetAccountTotal("INS") + lbl.GetAccountTotal("SW")
                    + GetTotalDepreciation(lbl) + lbl.GetAccountTotal("ELEC") + lbl.GetAccountTotal("GAS")
                    + lbl.GetAccountTotal("MISC") + lbl.GetAccountTotal("LEGAL");
        }

        public int GetSurplus(LoadAccountsBL lbl)
        {
            int surplus;
            surplus = GetIncome(lbl) - GetOPEX(lbl);
            return surplus;
        }

        public int GetUpdatedGF(LoadAccountsBL lbl)
        {
                return lbl.GetAccountTotal("GF") + GetSurplus(lbl);
        }

        /*public int GetGF(LoadAccountsBL lbl)
        {
                return GetSurplus(lbl);
        }
        public int GetFA(LoadAccountsBL lbl)
        {
            return lbl.GetAccountTotal("LAND") + lbl.GetAccountTotal("BLDG")
                    + lbl.GetAccountTotal("F&F") + lbl.GetAccountTotal("L-INST")
                    + lbl.GetAccountTotal("E-INST") + lbl.GetAccountTotal("W-COOL") - GetTotalDepreciation(lbl);
        }*/

        public int GetInvestments(LoadAccountsBL lbl)
        {
            return lbl.GetAccountTotal("SSC") + lbl.GetAccountTotal("DSC") + lbl.GetAccountTotal("BEH");
        }

        public override List<Accounts> GetOpeningEntries(LoadAccountsBL lbl, string newdate)
        {
            List<Accounts> openingentries = new List<Accounts>();
            CreateEntry("CASH", lbl.GetAccountTotal("CASH").ToString(), "0", newdate);
            CreateEntry("BANK", lbl.GetAccountTotal("BANK").ToString(), "0", newdate);
            CreateEntry("GF", "0", (GetUpdatedGF(lbl)).ToString(), newdate);
            return openingentries;
        }

        public override List<Accounts> GetClosingEntries(LoadAccountsBL lbl, string newdate)
        {
            List<Accounts> closingentries = new List<Accounts>();
            closingentries.Add(CreateEntry("BLDG", "DEP", "0", GetDepreciation(lbl, "BLDG").ToString(), "DEP", "DEPRECIATION EXPENSE", newdate));
            closingentries.Add(CreateEntry("DEP", "BLDG", GetDepreciation(lbl, "BLDG").ToString(), "0", "DEP", "DEPRECIATION EXPENSE", newdate));
            closingentries.Add(CreateEntry("F&F", "DEP", "0", GetDepreciation(lbl, "F&F").ToString(), "DEP", "DEPRECIATION EXPENSE", newdate));
            closingentries.Add(CreateEntry("DEP", "F&F", GetDepreciation(lbl, "F&F").ToString(), "0", "DEP", "DEPRECIATION EXPENSE", newdate));
            closingentries.Add(CreateEntry("L-INST", "DEP", "0", GetDepreciation(lbl, "L-INST").ToString(), "DEP", "DEPRECIATION EXPENSE", newdate));
            closingentries.Add(CreateEntry("DEP", "L-INST", GetDepreciation(lbl, "L-INST").ToString(), "0", "DEP", "DEPRECIATION EXPENSE", newdate));
            closingentries.Add(CreateEntry("GF", "", lbl.GetAccountTotal("PROFIT").ToString(), "0", "GF", "DEPRECIATION EXPENSE", newdate));
            return closingentries;
        }

        public override List<Accounts> GetRentEntries(string Plotcode, string Date, string SerialNo, string Name,
            string RentParticulars, string RentTotalRent, string WaterParticulars, string RentTotalWaterCharges, string RentArearsParticulars,
            double RArears, string WaterArearsParticulars, double WArears)
        {
            throw new NotImplementedException();
        }

        public override Dictionary<string, string> GetYearlyTotals(LoadAccountsBL lbl)
        {
            Dictionary<string, string> statement = new Dictionary<string, string>();
            statement.Add("SSCNOTES", lbl.GetAccountTotal("SSC").ToString());
            statement.Add("DSCNOTES", lbl.GetAccountTotal("DSC").ToString());
            statement.Add("BEHNOTES", lbl.GetAccountTotal("BEH").ToString());
            statement.Add("KESCNOTES", lbl.GetAccountTotal("KESC").ToString());
            statement.Add("SSGCNOTES", lbl.GetAccountTotal("SSGC").ToString());
            statement.Add("CASHNOTES", lbl.GetAccountTotal("CASH").ToString());
            statement.Add("BANKNOTES", lbl.GetAccountTotal("BANK").ToString());
            statement.Add("DOT1NOTES", lbl.GetAccountTotal("DOT1").ToString());
            statement.Add("BOXNOTES", lbl.GetAccountTotal("BOX").ToString());
            statement.Add("DOT2NOTES", lbl.GetAccountTotal("DOT2").ToString());
            statement.Add("CAP-DOTNOTES", lbl.GetAccountTotal("DOT2").ToString());

            statement.Add("DOTIS", GetTotalDonation(lbl).ToString());
            statement.Add("M-SUBIS", lbl.GetAccountTotal("M-SUB").ToString());
            statement.Add("RENTIS", lbl.GetAccountTotal("RENT").ToString());
            statement.Add("L21BR1IS", lbl.GetAccountTotal("L21BR1").ToString());
            statement.Add("WSCIS", lbl.GetAccountTotal("WSC").ToString());
            statement.Add("M-EXPIS", lbl.GetAccountTotal("M-EXP").ToString());
            statement.Add("R&MIS", lbl.GetAccountTotal("R&M").ToString());
            statement.Add("INSIS", lbl.GetAccountTotal("INS").ToString());
            statement.Add("S&WIS", lbl.GetAccountTotal("S&W").ToString());
            statement.Add("DEPIS", GetTotalDepreciation(lbl).ToString());
            statement.Add("ELECIS", lbl.GetAccountTotal("ELEC").ToString());
            statement.Add("MISCIS", lbl.GetAccountTotal("MISC").ToString());
            statement.Add("LEGALIS", lbl.GetAccountTotal("LEGAL").ToString());
            statement.Add("PROFITIS", lbl.GetAccountTotal("PROFIT").ToString());
            statement.Add("WTIS", lbl.GetAccountTotal("WT").ToString());
            statement.Add("WHTIS", lbl.GetAccountTotal("WHT").ToString());

            statement.Add("GFBS", lbl.GetAccountTotal("GF").ToString());
            statement.Add("SURPLUSBS", GetSurplus(lbl).ToString());
            statement.Add("CAP-DOTBS", lbl.GetAccountTotal("CAP-DOT").ToString());
            statement.Add("INVESTBS", GetInvestments(lbl).ToString());
            statement.Add("CASHBS", lbl.GetAccountTotal("CASH").ToString());
            statement.Add("AUD2", lbl.GetAccountTotal("AUD2").ToString());
            return statement;
        }

        public override List<Accounts> GetTrialBalance(LoadAccountsBL lbl)
        {
            throw new NotImplementedException();
        }

        public override string GetRentAccount(string PlotNo, int type)
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
