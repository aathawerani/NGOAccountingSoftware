using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustApplication.Exceptions;

namespace TrustApplication
{
    class HVHT : Trust
    {
        public override List<Accounts> GetOpeningEntries(LoadAccountsBL lbl, string newdate)
        {
            int dgk = GetDepreciation(lbl, "BGK6/1");
            int ibgk = lbl.GetAccountTotal("BGK6/1");
            string bgk = (ibgk - dgk).ToString();
            int ikhc = GetUpdatedCharity(lbl, "KHC");
            string khc = (ikhc).ToString();
            int iirc = GetUpdatedCharity(lbl, "IRC");
            string irc = (iirc).ToString();
            int igf = GetUpdatedGF(lbl);
            string gf = (igf).ToString();
            List<Accounts> openingentries = new List<Accounts>();
            int icash = lbl.GetAccountTotal("CASH");
            Accounts acash = CreateEntry("CASH", icash.ToString(), "0", newdate);
            openingentries.Add(acash);
            Accounts abgk = CreateEntry("BGK6/1", newdate, bgk, "0");
            openingentries.Add(abgk);
            Accounts akhc = CreateEntry("KHC", newdate, "0", khc);
            openingentries.Add(akhc);
            Accounts airc = CreateEntry("IRC", newdate, "0", irc);
            openingentries.Add(airc);
            Accounts agf = CreateEntry("GF", newdate, "0", gf);
            openingentries.Add(agf);
            int irrpm = lbl.GetAccountTotal("RRPM");
            Accounts arrpm = CreateEntry("RRPM", newdate, "0", irrpm.ToString());
            openingentries.Add(arrpm);
            int ilgk = lbl.GetAccountTotal("LGK6/1");
            Accounts algk = CreateEntry("LGK6/1", newdate, ilgk.ToString(), "0");
            openingentries.Add(algk);
            int iaud = lbl.GetAccountTotal("AUD2");
            Accounts aaud = CreateEntry("AUD2", newdate, "0", iaud.ToString());
            openingentries.Add(aaud);
            return openingentries;
        }
        public override List<Accounts> GetClosingEntries(LoadAccountsBL lbl, string newdate)
        {
            List<Accounts> closingentries = new List<Accounts>();
            int idep = GetDepreciation(lbl, "BGK6/1");
            Accounts adep = CreateEntry("BGK6/1", "DGK6/1", "0", idep.ToString(), "DEP", "DEPRECIATION EXPENSE", newdate);
            closingentries.Add(adep);
            int ibgk = GetDepreciation(lbl, "BGK6/1");
            Accounts abgk = CreateEntry("DGK6/1", "BGK6/1", ibgk.ToString(), "0", "DEP", "DEPRECIATION EXPENSE", newdate);
            closingentries.Add(abgk);
            int icharity = GetCharity(lbl);
            Accounts airc = CreateEntry("IRC", "", "0", icharity.ToString(), "IS", "FROM INCOME AND EXPENDITURE", newdate);
            closingentries.Add(airc);
            Accounts akhc = CreateEntry("KHC", "", "0", icharity.ToString(), "IS", "FROM INCOME AND EXPENDITURE", newdate);
            closingentries.Add(akhc);
            Accounts agf = CreateEntry("FUND", "", "0", GetGF(lbl).ToString(), "FUND", "FROM INCOME AND EXPENDITURE", newdate);
            closingentries.Add(agf);
            return closingentries;
        }
        public int GetDepreciation(LoadAccountsBL lbl, string Plot)
        {
            int dgk = (int)(Math.Ceiling(lbl.GetAccountTotal(Plot) * 2.5 / 100));
            return dgk;
        }

        public int GetIncome(LoadAccountsBL lbl)
        {
            int irgk = lbl.GetAccountTotal("RGK6");
            return irgk + lbl.GetAccountTotal("WGK6");
        }

        public int GetOPEX(LoadAccountsBL lbl)
        {
            int iptgk = lbl.GetAccountTotal("PTGK6");
            int iwht = lbl.GetAccountTotal("WHT");
            int iwtgk = lbl.GetAccountTotal("WTGK6");
            int ibgk = GetDepreciation(lbl, "BGK6/1");
            int ilegal = lbl.GetAccountTotal("LEGAL");
            int iaud = lbl.GetAccountTotal("AUD1");
            return iptgk + iwht + iwtgk + ibgk + ilegal + iaud;
        }

        public int GetSurplus(LoadAccountsBL lbl)
        {
            int surplus = GetIncome(lbl);
            surplus = surplus - GetOPEX(lbl);
            return surplus;
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
            int igf = lbl.GetAccountTotal("GF");
            return igf + lbl.GetAccountTotal("PROFIT");
        }

        public int GetGF(LoadAccountsBL lbl)
        {
            return lbl.GetAccountTotal("PROFIT");
        }
        public int GetFA(LoadAccountsBL lbl)
        {
            int ibgk = lbl.GetAccountTotal("BGK6/1");
            int ilgk = lbl.GetAccountTotal("LGK6/1");
            return ibgk + ilgk - GetDepreciation(lbl,"BGK6/1");
        }

        public int GetInvestments(LoadAccountsBL lbl)
        {
            int issc = lbl.GetAccountTotal("SSC");
            return issc + lbl.GetAccountTotal("DSC");
        }

        public override List<Accounts> GetRentEntries(string Plotcode, string Date, string SerialNo, string Name, 
            string RentParticulars, string RentTotalRent, string WaterParticulars, string RentTotalWaterCharges, string RentArearsParticulars,
            double RArears, string WaterArearsParticulars, double WArears)
        {
            List<Accounts> rententries = new List<Accounts>();
            Accounts argk = CreateEntry("RGK6", "CASH", "0", RentTotalRent, "RENT", RentParticulars, Date, Name, SerialNo);
            rententries.Add(argk);
            Accounts awgk = CreateEntry("WGK6", "CASH", "0", RentTotalWaterCharges, "RENT", WaterParticulars, Date, Name, SerialNo);
            rententries.Add(awgk);
            if (RArears > 0)
            {
                Accounts ararears = CreateEntry("RGK6", "CASH", "0", RArears.ToString(), "AREARS", RentArearsParticulars, Date, Name, SerialNo);
                rententries.Add(ararears);
            }
            if (WArears > 0)
            {
                Accounts awarears = CreateEntry("WGK6", "CASH", "0", WArears.ToString(), "AREARS", WaterArearsParticulars, Date, Name, SerialNo);
                rententries.Add(awarears);
            }
            return rententries;
        }
        public override Dictionary<string, string> GetYearlyTotals(LoadAccountsBL lbl)
        {
            Dictionary<string, string> statement = new Dictionary<string, string>();
            int irgk = lbl.GetAccountTotal("RGK6");
            statement.Add("RGK6IS", irgk.ToString());
            int igf = lbl.GetAccountTotal("GF");
            statement.Add("GFIS", igf.ToString());
            int irrpm = lbl.GetAccountTotal("RRPM");
            statement.Add("RRPMIS", irrpm.ToString());
            int iirc = lbl.GetAccountTotal("IRC");
            statement.Add("IRCIS", iirc.ToString());
            int iircpaid = lbl.GetTotalDebit("KHC");
            statement.Add("ICHARPAID", iircpaid.ToString());
            int ikhc = lbl.GetAccountTotal("KHC");
            statement.Add("KHCIS", ikhc.ToString());
            int ikhcpaid = lbl.GetTotalDebit("IRC");
            statement.Add("KCHARPAID", ikhcpaid.ToString());
            int issc = lbl.GetAccountTotal("SSC");
            statement.Add("SSCIS", issc.ToString());
            int idsc = lbl.GetAccountTotal("DSC");
            statement.Add("DSCIS", idsc.ToString());
            int icash = lbl.GetAccountTotal("CASH");
            statement.Add("CASHIS", icash.ToString());
            int iwht = lbl.GetAccountTotal("WHT");
            int iptgk = lbl.GetAccountTotal("PTGK6");
            statement.Add("WHTIS", (iwht + iptgk).ToString());
            int iwtgk = lbl.GetAccountTotal("WTGK6");
            statement.Add("WTGK6IS", iwtgk.ToString());
            int iwgk = lbl.GetAccountTotal("WGK6");
            statement.Add("WGK6IS", iwgk.ToString());
            int ibgk = GetDepreciation(lbl, "BGK6/1");
            statement.Add("DGKIS", ibgk.ToString());
            int ilegal = lbl.GetAccountTotal("LEGAL");
            statement.Add("LEGALIS", ilegal.ToString());
            int iaud = lbl.GetAccountTotal("AUD1");
            statement.Add("AUD1IS", iaud.ToString());
            int iprofit = lbl.GetAccountTotal("PROFIT");
            statement.Add("PROFITIS", iprofit.ToString());
            int idot = lbl.GetAccountTotal("DOT");
            statement.Add("DOTIS", idot.ToString());

            int ifund = GetUpdatedGF(lbl);
            statement.Add("FUNDBS", ifund.ToString());
            int ifa = GetFA(lbl);
            statement.Add("FABS", ifa.ToString());
            int isscdsc = GetInvestments(lbl);
            statement.Add("SSCDSCBS", isscdsc.ToString());
            statement.Add("CASHBS", icash.ToString());
            int iaud2 = lbl.GetAccountTotal("AUD2");
            statement.Add("AUD2BS", (0 - iaud2).ToString());
            return statement;
        }

        public int GetDebitTotal(LoadAccountsBL lbl)
        {
            int icash = lbl.GetAccountTotal("CASH");
            int ilgk = lbl.GetAccountTotal("LGK6/1");
            int ibgk = lbl.GetAccountTotal("BGK6/1");
            int iwht = lbl.GetAccountTotal("WHT");
            int iptgk = lbl.GetAccountTotal("PTGK6");
            int iwtgk = lbl.GetAccountTotal("WTGK6");
            int iaud1 = lbl.GetAccountTotal("AUD1");
            return icash + ilgk + ibgk + iwht + iptgk + iwtgk + iaud1;
        }
        
        public int GetCreditTotal(LoadAccountsBL lbl)
        {
            int iaud2 = lbl.GetAccountTotal("AUD2");
            int ikhc = lbl.GetAccountTotal("KHC");
            int iirc = lbl.GetAccountTotal("IRC");
            int idot = lbl.GetAccountTotal("DOT");
            int irrpm = lbl.GetAccountTotal("RRPM");
            int irgk = lbl.GetAccountTotal("RGK6");
            int iwgk = lbl.GetAccountTotal("WGK6");
            int iprofit = lbl.GetAccountTotal("PROFIT");
            int igf = lbl.GetAccountTotal("GF");
            return iaud2 + ikhc + iirc + idot + irrpm + irgk + iwgk + iprofit + igf;
        }
        public override List<Accounts> GetTrialBalance(LoadAccountsBL lbl)
        {
            List<Accounts> trialbalance = new List<Accounts>();
            int icash = lbl.GetAccountTotal("CASH");
            Accounts acash = CreateEntry("CASH", icash.ToString(), "0", "");
            trialbalance.Add(acash);
            int ilgk = lbl.GetAccountTotal("LGK6/1");
            Accounts algk = CreateEntry("LGK6/1", ilgk.ToString(), "0", "");
            trialbalance.Add(algk);
            int ibgk = lbl.GetAccountTotal("BGK6/1");
            Accounts abgk = CreateEntry("BGK6/1", ibgk.ToString(), "0", "");
            trialbalance.Add(abgk);
            int issc = lbl.GetAccountTotal("SSC");
            Accounts assc = CreateEntry("SSC", issc.ToString(), "0", "");
            trialbalance.Add(assc);
            int idsc = lbl.GetAccountTotal("DSC");
            Accounts adsc = CreateEntry("DSC", idsc.ToString(), "0", "");
            trialbalance.Add(adsc);
            int iwht = lbl.GetAccountTotal("WHT");
            Accounts awht = CreateEntry("WHT", iwht.ToString(), "0", "");
            trialbalance.Add(awht);
            int iptgk = lbl.GetAccountTotal("PTGK6");
            Accounts aptgk = CreateEntry("PTGK6", iptgk.ToString(), "0", "");
            trialbalance.Add(aptgk);
            int iwtgk = lbl.GetAccountTotal("WTGK6");
            Accounts awtgk = CreateEntry("WTGK6", iwtgk.ToString(), "0", "");
            trialbalance.Add(awtgk);
            int iaud1 = lbl.GetAccountTotal("AUD1");
            Accounts aaud1 = CreateEntry("AUD1", iaud1.ToString(), "0", "");
            trialbalance.Add(aaud1);
            int idebit = GetDebitTotal(lbl);
            Accounts adebit = CreateEntry("Total", idebit.ToString(), "0", "");
            trialbalance.Add(adebit);

            int iaud2 = lbl.GetAccountTotal("AUD2");
            Accounts aaud2 = CreateEntry("AUD2", "0", iaud2.ToString(), "");
            trialbalance.Add(aaud2);
            int iirc = lbl.GetAccountTotal("IRC");
            Accounts airc = CreateEntry("IRC", "0", iirc.ToString(), "");
            trialbalance.Add(airc);
            int ikhc = lbl.GetAccountTotal("KHC");
            Accounts akhc = CreateEntry("KHC", "0", ikhc.ToString(), "");
            trialbalance.Add(akhc);
            int idot = lbl.GetAccountTotal("DOT");
            Accounts adot = CreateEntry("DOT", "0", idot.ToString(), "");
            trialbalance.Add(adot);
            int irrpm = lbl.GetAccountTotal("RRPM");
            Accounts arrpm = CreateEntry("RRPM", "0", irrpm.ToString(), "");
            trialbalance.Add(arrpm);
            int irgk = lbl.GetAccountTotal("RGK6");
            Accounts argk = CreateEntry("RGK6", "0", irgk.ToString(), "");
            trialbalance.Add(argk);
            int iwgk = lbl.GetAccountTotal("WGK6");
            Accounts awgk = CreateEntry("WGK6", "0", iwgk.ToString(), "");
            trialbalance.Add(awgk);
            int iprofit = lbl.GetAccountTotal("PROFIT");
            Accounts aprofit = CreateEntry("PROFIT", "0", iprofit.ToString(), "");
            trialbalance.Add(aprofit);
            int igf = lbl.GetAccountTotal("GF");
            Accounts agf = CreateEntry("GF", "0", igf.ToString(), "");
            trialbalance.Add(agf);
            int icredit = GetCreditTotal(lbl);
            Accounts acredit = CreateEntry("Total", "0", icredit.ToString(), "");
            trialbalance.Add(acredit);
            return trialbalance;
        }
        public override string GetRentAccount(string PlotNo, int type)
        {
            return (type == 1) ? "RGK6" : "WGK6";
        }

        public override string GetAccountKey(string ContraAccount, string Particulars)
        {
            if (Particulars.Contains("AREARS"))
                return "AREARS";
            else if (Particulars.Contains("OPENING"))
                return "OPENING";
            else if (ContraAccount == "RGK6" || ContraAccount == "WGK6")
                return "RENT";
            else
                return ContraAccount;
        }

        public override bool IsRent(string AccountCode)
        {
            if (AccountCode == "RGK6" || AccountCode == "WGK6")
                return true;
            return false;
        }

        public override bool IsCertificate(string AccountCode)
        {
            if (AccountCode == "SSC" || AccountCode == "DSC")
                return true;
            return false;
        }

    }
}
