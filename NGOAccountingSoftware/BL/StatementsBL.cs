using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustApplication.Exceptions;

namespace TrustApplication
{
    class StatementsBL : BL
    {
        AccountsBL Abl = new AccountsBL(); ReportBL rbl = new ReportBL(); LoadAccountsBL lbl = new LoadAccountsBL();
        ExcelWriter xlwriter;
        public List<Accounts> TrialBalance(int _TrustCode, string Year)
        {
            string[] years = Year.Split('-');
            int endyear = 0;
            if (!int.TryParse(years[1], out endyear))
            {
                throw new StatementsBLException("Failed to parse year");
            }
            DateTime enddate = new DateTime(endyear, 6, 30);
            DateTime newdate = new DateTime(endyear, 7, 1);
            List<Accounts> CE = new List<Accounts>();
            string newdatestr = util.FormatDate(newdate);
            lbl.LoadData(_TrustCode, Year);
            if (_TrustCode == 1)
            {
                string ssc = Abl.GetAccountTotal(_TrustCode, "SSC", new DateTime(2000, 1, 1), enddate);
                string dsc = Abl.GetAccountTotal(_TrustCode, "DSC", new DateTime(2000, 1, 1), enddate);

                int sscint = 0, dscint = 0;
                int.TryParse(ssc, out sscint);
                int.TryParse(dsc, out dscint);

                int cash = lbl.GetAccountTotal("CASH");
                int lgk = lbl.GetAccountTotal("LGK6/1");
                int bgktotal = lbl.GetAccountTotal("BGK6/1");
                int wht = lbl.GetAccountTotal("WHT");
                int pt = lbl.GetAccountTotal("PTGK6");
                int wt = lbl.GetAccountTotal("WTGK6");
                int ad1 = lbl.GetAccountTotal("AUD1");
                int debittotal = cash + lgk + bgktotal + wht + pt + wt + ad1 + sscint + dscint;

                int aud2 = lbl.GetAccountTotal("AUD2");
                int khc = lbl.GetAccountTotal("KHC");
                int irc = lbl.GetAccountTotal("IRC");
                int don = lbl.GetAccountTotal("DOT");
                int rrpm = lbl.GetAccountTotal("RRPM");
                int rgk6 = lbl.GetAccountTotal("RGK6");
                int wgk6 = lbl.GetAccountTotal("WGK6");
                int profit = lbl.GetAccountTotal("PROFIT");
                int gf = lbl.GetAccountTotal("GF");
                int credittotal = aud2 + khc + irc + don + rrpm + rgk6 + wgk6 + profit + gf;

                CE.Add(new Accounts(newdatestr, "", "CASH", cash.ToString(), "0", "CASH"));
                CE.Add(new Accounts(newdatestr, "", "LAND", lgk.ToString(), "0", "LGK6/1"));
                CE.Add(new Accounts(newdatestr, "", "BUILDING", bgktotal.ToString(), "0", "BGK6/1"));
                CE.Add(new Accounts(newdatestr, "", "SPECIAL SAVING CERTIFICATES", ssc, "0", "SSC"));
                CE.Add(new Accounts(newdatestr, "", "DEFENCE SAVING CERTIFICATES", dsc, "0", "DSC"));
                CE.Add(new Accounts(newdatestr, "", "WITHOLDING TAX", wht.ToString(), "0", "WHT"));
                CE.Add(new Accounts(newdatestr, "", "PROPERTY TAX GK6/1", pt.ToString(), "0", "PTGK6"));
                CE.Add(new Accounts(newdatestr, "", "WATER TAX GK6/1", wt.ToString(), "0", "WTGK6"));
                CE.Add(new Accounts(newdatestr, "", "AUDIT FEE EXPENSE", ad1.ToString(), "0", "AUD1"));
                CE.Add(new Accounts(newdatestr, "", "DEBIT TOTAL", debittotal.ToString(), "0", "TOTAL"));

                CE.Add(new Accounts(newdatestr, "", "AUDIT FEE PAYABLE", "0", aud2.ToString(), "AUD2"));
                CE.Add(new Accounts(newdatestr, "", "IRAQ CHARITY ACCOUNT", "0", irc.ToString(), "IRC"));
                CE.Add(new Accounts(newdatestr, "", "KARACHI CHARITY ACCOUNT", "0", khc.ToString(), "KHC"));
                CE.Add(new Accounts(newdatestr, "", "DONATION", "0", don.ToString(), "DON"));
                CE.Add(new Accounts(newdatestr, "", "RESERVE REPAIR AND MAINTENANCE", "0", rrpm.ToString(), "RRPM"));
                CE.Add(new Accounts(newdatestr, "", "RENT GK6/1", "0", rgk6.ToString(), "RGK6"));
                CE.Add(new Accounts(newdatestr, "", "WATER CHARGES RECEIVED GK6/1", "0", wgk6.ToString(), "WGK6"));
                CE.Add(new Accounts(newdatestr, "", "PROFIT ", "0", profit.ToString(), "PROFIT"));
                CE.Add(new Accounts(newdatestr, "", "GENERAL FUND", "0", gf.ToString(), "GF"));
                CE.Add(new Accounts(newdatestr, "", "CREDIT TOTAL", "0", credittotal.ToString(), "TOTAL"));

                CE.Add(new Accounts(newdatestr, "", "DIFFERENCE", (debittotal - credittotal).ToString(), "0", "DIFF"));
            }
            else if (_TrustCode == 2)
            {
            }
            else if (_TrustCode == 3)
            {
            }
            return CE;
        }

        public void YearlyStatement(int _TrustCode, DateTime enddate, string FileName, int column)
        {
            if (_TrustCode == 1)
            {
                string ssc = Abl.GetAccountTotal(_TrustCode, "SSC", new DateTime(2000, 1, 1), enddate);
                string dsc = Abl.GetAccountTotal(_TrustCode, "DSC", new DateTime(2000, 1, 1), enddate);
                int dgk = (int)(lbl.GetAccountTotal("BGK6/1") * 2.5 / 100);
                int kcharpaid = lbl.GetTotalDebit("KHC");
                int icharpaid = lbl.GetTotalDebit("IRC");
                string wht = (lbl.GetAccountTotal("WHT") + lbl.GetAccountTotal("PTGK6")).ToString();
                int opex = lbl.GetAccountTotal("PTGK6") + lbl.GetAccountTotal("WTGK6")
                    + lbl.GetAccountTotal("WHT") - lbl.GetAccountTotal("WGK6") + dgk
                    + lbl.GetAccountTotal("LEGAL") + lbl.GetAccountTotal("AUD1");
                int charamount = lbl.GetAccountTotal("RGK6") - opex;
                int Fund = lbl.GetAccountTotal("GF") + lbl.GetAccountTotal("PROFIT")
                    + lbl.GetAccountTotal("RRPM") + lbl.GetAccountTotal("IRC")
                    + lbl.GetAccountTotal("KHC") + charamount + lbl.GetAccountTotal("DON");
                string FA  = (lbl.GetAccountTotal("BGK6/1") + lbl.GetAccountTotal("LGK6/1") - dgk).ToString();

                //xlwriter = new ExcelWriter(FileName);
                xlwriter.OpenWorksheet("IS");
                int row = 14; xlwriter.Write(row, column, lbl.GetAccountTotal("RGK6").ToString()); 
                row = 79; xlwriter.Write(row, column, lbl.GetAccountTotal("GF").ToString()); 
                row = 81; xlwriter.Write(row, column, lbl.GetAccountTotal("RRPM").ToString()); 
                row = 86; xlwriter.Write(row, column, lbl.GetAccountOpening("IRC")); 
                row = 88; xlwriter.Write(row, column, icharpaid.ToString()); 
                row = 91; xlwriter.Write(row, column, lbl.GetAccountOpening("KHC")); 
                row = 93; xlwriter.Write(row, column, kcharpaid.ToString()); 
                row = 97; xlwriter.Write(row, column, ssc); 
                row = 98; xlwriter.Write(row, column, dsc); 
                row = 102; xlwriter.Write(row, column, lbl.GetAccountTotal("CASH").ToString()); 
                row = 105; xlwriter.Write(row, column, wht); 
                row = 106; xlwriter.Write(row, column, lbl.GetAccountTotal("WTGK6").ToString()); 
                row = 109; xlwriter.Write(row, column, lbl.GetAccountTotal("WGK6").ToString()); 
                row = 111; xlwriter.Write(row, column, dgk.ToString()); 
                row = 112; xlwriter.Write(row, column, lbl.GetAccountTotal("LEGAL").ToString()); 
                row = 113; xlwriter.Write(row, column, lbl.GetAccountTotal("AUD1").ToString()); 
                row = 117; xlwriter.Write(row, column, lbl.GetAccountTotal("PROFIT").ToString()); 
                row = 118; xlwriter.Write(row, column, lbl.GetAccountTotal("DOT").ToString()); 

                xlwriter.OpenWorksheet("BS"); 
                row = 8; column = 5; xlwriter.Write(row, column, Fund.ToString()); 
                row = 18; xlwriter.Write(row, column, FA); 
                row = 20; xlwriter.Write(row, column, (int.Parse(ssc) + int.Parse(dsc)).ToString()); 
                row = 23; xlwriter.Write(row, column, lbl.GetAccountTotal("CASH").ToString()); 
                row = 26; xlwriter.Write(row, column, (0 - lbl.GetAccountTotal("AUD2")).ToString()); 

                xlwriter.Save(); 
            }
        }

        /*public int WriteStatements(int _TrustCode, string Year, string FileName)
        {
            string TrustCode = _TrustCode.ToString();
            int result = rbl.WriteAccounts(_TrustCode, Year, FileName);
            xlwriter = rbl.GetXLWriter();
            lbl = rbl.GetAllAccount();
            if (result < 0) return result;
            string[] years = Year.Split('-');
            if (years.Length < 2) return -1;
            int startyear = 0;
            if (!int.TryParse(years[0], out startyear)) return -1;
            DateTime startdate = new DateTime(startyear, 7, 1);
            int endyear = 0;
            if (!int.TryParse(years[1], out endyear)) return -1;
            DateTime enddate = new DateTime(endyear, 6, 30);
            DateTime newdate = new DateTime(endyear, 7, 1);
            //int result = dal.AccountsInsert(TrustCode, GetAccountTypeCode("CASH"), FormatDate(newdate), "", "",
            //"", "OPENING BALANCE", GetAccountTotal("CASH"), "0", "1");
            if (_TrustCode == 1)
            {
                //string ssc = Abl.GetAccountTotal(_TrustCode, "SSC", new DateTime(2000, 1, 1), enddate);
                //string dsc = Abl.GetAccountTotal(_TrustCode, "DSC", new DateTime(2000, 1, 1), enddate);

                int dgk = (int)(lbl.GetAccountTotal("BGK6/1") * 2.5 / 100);

                //int kcharpaid = GetTotalDebit("KHC");
                //int icharpaid = GetTotalDebit("IRC");

                string wht = (lbl.GetAccountTotal("WHT") + lbl.GetAccountTotal("PTGK6")).ToString();

                xlwriter = new ExcelWriter(FileName);
                int row = 14, column = 4;
                /*xlwriter.OpenWorksheet("IS");
                xlwriter.Write(row, column, GetAccountTotal("RGK6"));
                row = 79; xlwriter.Write(row, column, GetAccountTotal("GF"));
                row = 81; xlwriter.Write(row, column, GetAccountTotal("RRPM"));
                row = 86; xlwriter.Write(row, column, GetAccountOpening("IRC"));
                row = 88; xlwriter.Write(row, column, icharpaid.ToString());
                row = 91; xlwriter.Write(row, column, GetAccountOpening("KHC"));
                row = 93; xlwriter.Write(row, column, kcharpaid.ToString());
                row = 97; xlwriter.Write(row, column, ssc);
                row = 98; xlwriter.Write(row, column, dsc);
                row = 102; xlwriter.Write(row, column, GetAccountTotal("CASH"));
                row = 105; xlwriter.Write(row, column, wht);
                row = 106; xlwriter.Write(row, column, GetAccountTotal("WTGK6"));
                row = 109; xlwriter.Write(row, column, GetAccountTotal("WGK6"));
                row = 111; xlwriter.Write(row, column, dgk.ToString());
                row = 112; xlwriter.Write(row, column, GetAccountTotal("LEGAL"));
                row = 113; xlwriter.Write(row, column, GetAccountTotal("AUD1"));
                row = 117; xlwriter.Write(row, column, GetAccountTotal("PROFIT"));
                */
        /*xlwriter.OpenWorksheet("FA");
        row = 30; column = 1; xlwriter.Write(row, column, FormatDate(enddate));
        column = 3; xlwriter.Write(row, column, "DGK6/1");
        column = 5; xlwriter.Write(row, column, "DEPRECIATION EXPENSE");
        column = 7; xlwriter.Write(row, column, dgk.ToString());
        row = 46; column = 1; xlwriter.Write(row, column, FormatDate(enddate));
        column = 3; xlwriter.Write(row, column, "BGK6/1");
        column = 5; xlwriter.Write(row, column, "DEPRECIATION EXPENSE");
        column = 6; xlwriter.Write(row, column, dgk.ToString());

        int opex = lbl.GetAccountTotal("PTGK6") + lbl.GetAccountTotal("WTGK6")
            + lbl.GetAccountTotal("WHT") - lbl.GetAccountTotal("WGK6") + dgk
            + lbl.GetAccountTotal("LEGAL") + lbl.GetAccountTotal("AUD1");
        int charamount = lbl.GetAccountTotal("RGK6") - opex;

        /*int Fund = int.Parse(GetAccountTotal("GF")) + int.Parse(GetAccountTotal("PROFIT")) 
            + int.Parse(GetAccountTotal("RRPM")) + int.Parse(GetAccountTotal("IRC"))
            + int.Parse(GetAccountTotal("KHC")) + charamount;

        xlwriter.OpenWorksheet("BS");
        row = 8; column = 5; xlwriter.Write(row, column, Fund.ToString());
        string fa = (int.Parse(GetAccountTotal("BGK6/1")) + int.Parse(GetAccountTotal("LGK6/1")) - dgk).ToString();
        row = 18; xlwriter.Write(row, column, fa);
        row = 20; xlwriter.Write(row, column, (int.Parse(ssc) + int.Parse(dsc)).ToString());
        row = 23; xlwriter.Write(row, column, GetAccountTotal("CASH"));
        row = 26; xlwriter.Write(row, column, (0 - int.Parse(GetAccountTotal("AUD2"))).ToString());
        */

        /*charamount /= 2;
        xlwriter.OpenWorksheet("IRAQ");
        row = 15; column = 1; xlwriter.Write(row, column, FormatDate(enddate));
        column = 5; xlwriter.Write(row, column, "FROM INCOME AND EXPENDITURE");
        column = 7; xlwriter.Write(row, column, charamount.ToString());

        xlwriter.OpenWorksheet("KARACHI");
        row = 51; column = 1; xlwriter.Write(row, column, FormatDate(enddate));
        column = 5; xlwriter.Write(row, column, "FROM INCOME AND EXPENDITURE");
        column = 7; xlwriter.Write(row, column, charamount.ToString());

        xlwriter.OpenWorksheet("FUND");
        row = 14; column = 1; xlwriter.Write(row, column, FormatDate(enddate));
        column = 5; xlwriter.Write(row, column, "FROM INCOME AND EXPENDITURE");
        column = 7; xlwriter.Write(row, column, lbl.GetAccountTotal("PROFIT").ToString());

        xlwriter.Save();

        /*string bgk = (int.Parse(GetAccountTotal("BGK6/1")) - dgk).ToString();
        result = dal.AccountsInsert(TrustCode, GetAccountTypeCode("BGK6/1"), FormatDate(newdate), "", "", "", 
            "OPENING BALANCE", bgk, "0", "1");
        string khc = (int.Parse(GetAccountTotal("KHC")) - kcharpaid + charamount).ToString();
        result = dal.AccountsInsert(TrustCode, GetAccountTypeCode("KHC"), FormatDate(newdate), "", "", "", 
            "OPENING BALANCE", "0", khc, "1");
        string irc = (int.Parse(GetAccountTotal("IRC")) - icharpaid + charamount).ToString();
        result = dal.AccountsInsert(TrustCode, GetAccountTypeCode("IRC"), FormatDate(newdate), "", 
            "", "", "OPENING BALANCE", "0", irc, "1");
        string gf = (int.Parse(GetAccountTotal("GF")) + int.Parse(GetAccountTotal("PROFIT"))).ToString();
        result = dal.AccountsInsert(TrustCode, GetAccountTypeCode("GF"), FormatDate(newdate), "", 
            "", "", "OPENING BALANCE", "0", gf, "1");

        result = dal.AccountsInsert(TrustCode, GetAccountTypeCode("RRPM"), FormatDate(newdate), "", 
            "", "", "OPENING BALANCE", "0", GetAccountTotal("RRPM"), "1");
        result = dal.AccountsInsert(TrustCode, GetAccountTypeCode("LGK6/1"), FormatDate(newdate), "", 
            "", "", "OPENING BALANCE", GetAccountTotal("LGK6/1"), "0", "1");
        result = dal.AccountsInsert(TrustCode, GetAccountTypeCode("AUD2"), FormatDate(newdate), "", 
            "", "", "OPENING BALANCE", "0", GetAccountTotal("AUD2"), "1");

        result = dal.AccountsInsert(TrustCode, GetAccountTypeCode("SSC"), FormatDate(newdate), "", 
            "", "", "OPENING BALANCE", "0", "0", "1");
        result = dal.AccountsInsert(TrustCode, GetAccountTypeCode("DSC"), FormatDate(newdate), "", 
            "", "", "OPENING BALANCE", "0", "0", "1");*/
        /*}
        else if (_TrustCode == 2)
        {
            string ssc = Abl.GetAccountTotal(_TrustCode, "SSC", new DateTime(2000, 1, 1), enddate);
            string dsc = Abl.GetAccountTotal(_TrustCode, "DSC", new DateTime(2000, 1, 1), enddate);

            int ptaxes = lbl.GetAccountTotal("PT4BR1") + lbl.GetAccountTotal("PT46GK7")
                + lbl.GetAccountTotal("PT2BR1") + lbl.GetAccountTotal("PT21BR1")
                + lbl.GetAccountTotal("WHT");
            int wtaxes = lbl.GetAccountTotal("WT4BR1") + lbl.GetAccountTotal("WT46GK7")
                + lbl.GetAccountTotal("WT2BR1") + lbl.GetAccountTotal("WT21BR1");
            int wcharges = lbl.GetAccountTotal("W46GK7") + lbl.GetAccountTotal("W2BR1")
                + lbl.GetAccountTotal("W4BR1") + lbl.GetAccountTotal("W21BR1");
            int dep1 = (int)Math.Ceiling((lbl.GetAccountTotal("B46GK7") * 2.5 / 100));
            int dep2 = (int)Math.Ceiling((lbl.GetAccountTotal("B2BR1") * 2.5 / 100));
            int dep3 = (int)Math.Ceiling((lbl.GetAccountTotal("B4BR1") * 2.5 / 100));
            int dep4 = (int)Math.Ceiling((lbl.GetAccountTotal("B21BR1") * 2.5 / 100));
            int dep = dep1 + dep2 + dep3 + dep4;

            xlwriter = new ExcelWriter(FileName);
            xlwriter.OpenWorksheet("IS");
            int row = 82, column = 4; xlwriter.Write(row, column, lbl.GetAccountTotal("GF").ToString());
            row = 89; xlwriter.Write(row, column, ssc);
            row = 90; xlwriter.Write(row, column, dsc);
            row = 94; xlwriter.Write(row, column, lbl.GetAccountTotal("CASH").ToString());
            row = 95; xlwriter.Write(row, column, lbl.GetAccountTotal("BANK").ToString());
            row = 99; xlwriter.Write(row, column, lbl.GetAccountTotal("R21BR1").ToString());
            row = 100; xlwriter.Write(row, column, lbl.GetAccountTotal("R4BR1").ToString());
            row = 101; xlwriter.Write(row, column, lbl.GetAccountTotal("R2BR1").ToString());
            row = 102; xlwriter.Write(row, column, lbl.GetAccountTotal("R46GK7").ToString());
            row = 106; xlwriter.Write(row, column, ptaxes.ToString());
            row = 107; xlwriter.Write(row, column, wtaxes.ToString());
            row = 110; xlwriter.Write(row, column, wcharges.ToString());
            row = 112; xlwriter.Write(row, column, dep.ToString());
            row = 114; xlwriter.Write(row, column, lbl.GetAccountTotal("M&S").ToString());
            row = 115; xlwriter.Write(row, column, lbl.GetAccountTotal("LEGAL").ToString());
            row = 116; xlwriter.Write(row, column, lbl.GetAccountTotal("AUD1").ToString());
            row = 120; xlwriter.Write(row, column, lbl.GetAccountTotal("PROFIT").ToString());

            xlwriter.OpenWorksheet("FA 46GK7");
            row = 31; column = 1; xlwriter.Write(row, column, FormatDate(enddate));
            column = 3; xlwriter.Write(row, column, "DEP");
            column = 5; xlwriter.Write(row, column, "DEPRECIATION EXPENSE");
            column = 7; xlwriter.Write(row, column, dep1.ToString());

            xlwriter.OpenWorksheet("FA 2BR1");
            row = 31; column = 1; xlwriter.Write(row, column, FormatDate(enddate));
            column = 3; xlwriter.Write(row, column, "DEP");
            column = 5; xlwriter.Write(row, column, "DEPRECIATION EXPENSE");
            column = 7; xlwriter.Write(row, column, dep2.ToString());

            xlwriter.OpenWorksheet("FA 4BR1");
            row = 31; column = 1; xlwriter.Write(row, column, FormatDate(enddate));
            column = 3; xlwriter.Write(row, column, "DEP");
            column = 5; xlwriter.Write(row, column, "DEPRECIATION EXPENSE");
            column = 7; xlwriter.Write(row, column, dep3.ToString());

            xlwriter.OpenWorksheet("FA 21BR1");
            row = 31; column = 1; xlwriter.Write(row, column, FormatDate(enddate));
            column = 3; xlwriter.Write(row, column, "DEP");
            column = 5; xlwriter.Write(row, column, "DEPRECIATION EXPENSE");
            column = 7; xlwriter.Write(row, column, dep4.ToString());

            xlwriter.OpenWorksheet("DEP");
            row = 13; column = 1; xlwriter.Write(row, column, FormatDate(enddate));
            column = 3; xlwriter.Write(row, column, "B46GK7");
            column = 5; xlwriter.Write(row, column, "DEPRECIATION EXPENSE");
            column = 6; xlwriter.Write(row, column, dep1.ToString());
            row = 14; column = 1; xlwriter.Write(row, column, FormatDate(enddate));
            column = 3; xlwriter.Write(row, column, "B2BR1");
            column = 5; xlwriter.Write(row, column, "DEPRECIATION EXPENSE");
            column = 6; xlwriter.Write(row, column, dep2.ToString());
            row = 15; column = 1; xlwriter.Write(row, column, FormatDate(enddate));
            column = 3; xlwriter.Write(row, column, "B4BR1");
            column = 5; xlwriter.Write(row, column, "DEPRECIATION EXPENSE");
            column = 6; xlwriter.Write(row, column, dep3.ToString());
            row = 16; column = 1; xlwriter.Write(row, column, FormatDate(enddate));
            column = 3; xlwriter.Write(row, column, "B21BR1");
            column = 5; xlwriter.Write(row, column, "DEPRECIATION EXPENSE");
            column = 6; xlwriter.Write(row, column, dep4.ToString());

            int rent = lbl.GetAccountTotal("R46GK7") + lbl.GetAccountTotal("R2BR1")
                + lbl.GetAccountTotal("R4BR1") + lbl.GetAccountTotal("R21BR1");
            int opex = ptaxes + wtaxes - wcharges + dep + lbl.GetAccountTotal("M&S")
                + lbl.GetAccountTotal("LEGAL") + lbl.GetAccountTotal("AUD1");
            int surplus = rent - opex + lbl.GetAccountTotal("PROFIT");
            int Fund = lbl.GetAccountTotal("GF") + surplus;
            int fassets = lbl.GetAccountTotal("L46GK7") + lbl.GetAccountTotal("L2BR1")
                + lbl.GetAccountTotal("L4BR1") + lbl.GetAccountTotal("L21BR1")
                + lbl.GetAccountTotal("B46GK7") + lbl.GetAccountTotal("B2BR1")
                + lbl.GetAccountTotal("B4BR1") + lbl.GetAccountTotal("B21BR1") - dep;

            xlwriter.OpenWorksheet("BS");
            row = 8; column = 4; xlwriter.Write(row, column, Fund.ToString());
            row = 18; xlwriter.Write(row, column, fassets.ToString());
            row = 20; xlwriter.Write(row, column, (int.Parse(ssc) + int.Parse(dsc)).ToString());
            row = 23; xlwriter.Write(row, column, lbl.GetAccountTotal("CASH").ToString());
            row = 26; xlwriter.Write(row, column, (0 - lbl.GetAccountTotal("AUD2")).ToString());

            xlwriter.OpenWorksheet("GFUND");
            row = 14; column = 1; xlwriter.Write(row, column, FormatDate(enddate));
            column = 5; xlwriter.Write(row, column, "FROM INCOME AND EXPENDITURE");
            column = 7; xlwriter.Write(row, column, lbl.GetAccountTotal("PROFIT").ToString());

            xlwriter.Save();

        }
        else if (_TrustCode == 3)
        {
            string ssc = Abl.GetAccountTotal(_TrustCode, "SSC", new DateTime(2000, 1, 1), enddate);
            string dsc = Abl.GetAccountTotal(_TrustCode, "DSC", new DateTime(2000, 1, 1), enddate);
            string bsc = Abl.GetAccountTotal(_TrustCode, "BSC", new DateTime(2000, 1, 1), enddate);

            int dep1 = (int)Math.Ceiling((lbl.GetAccountTotal("BLDG") * 2.5 / 100));
            int dep2 = (int)Math.Ceiling((lbl.GetAccountTotal("F&F") * 2.5 / 100));
            int dep3 = (int)Math.Ceiling((lbl.GetAccountTotal("L-INST") * 2.5 / 100));
            int dep = dep1 + dep2 + dep3;

            xlwriter = new ExcelWriter(FileName);

            xlwriter.OpenWorksheet("NOTES");
            int row = 97, column = 4; xlwriter.Write(row, column, ssc);
            row = 98; xlwriter.Write(row, column, dsc);
            row = 99; xlwriter.Write(row, column, bsc);
            row = 105; xlwriter.Write(row, column, lbl.GetAccountTotal("KESC").ToString());
            row = 106; xlwriter.Write(row, column, lbl.GetAccountTotal("SSGC").ToString());
            row = 112; xlwriter.Write(row, column, lbl.GetAccountTotal("CASH").ToString());
            row = 113; xlwriter.Write(row, column, lbl.GetAccountTotal("BANK").ToString());
            row = 119; xlwriter.Write(row, column, lbl.GetAccountTotal("DOT1").ToString());
            row = 120; xlwriter.Write(row, column, lbl.GetAccountTotal("BOX").ToString());
            row = 121; xlwriter.Write(row, column, lbl.GetAccountTotal("DOT2").ToString());
            row = 128; xlwriter.Write(row, column, lbl.GetAccountTotal("CAP-DOT").ToString());

            xlwriter.OpenWorksheet("IS");
            row = 14; column = 4; xlwriter.Write(row, column, (lbl.GetAccountTotal("DOT1")
                + lbl.GetAccountTotal("DOT2") + lbl.GetAccountTotal("BOX")).ToString());
            row = 15; xlwriter.Write(row, column, lbl.GetAccountTotal("M-SUB").ToString());
            row = 16; xlwriter.Write(row, column, lbl.GetAccountTotal("RENT").ToString());
            row = 17; xlwriter.Write(row, column, lbl.GetAccountTotal("L21BR1").ToString());
            row = 18; xlwriter.Write(row, column, lbl.GetAccountTotal("WSC").ToString());
            row = 24; xlwriter.Write(row, column, lbl.GetAccountTotal("M-EXP").ToString());
            row = 25; xlwriter.Write(row, column, lbl.GetAccountTotal("R&M").ToString());
            row = 26; xlwriter.Write(row, column, lbl.GetAccountTotal("INS").ToString());
            row = 27; xlwriter.Write(row, column, lbl.GetAccountTotal("S&W").ToString());
            row = 28; xlwriter.Write(row, column, (dep1 + dep2 + dep3).ToString());
            row = 29; xlwriter.Write(row, column, lbl.GetAccountTotal("ELEC").ToString());
            row = 30; xlwriter.Write(row, column, lbl.GetAccountTotal("GAS").ToString());
            row = 32; xlwriter.Write(row, column, lbl.GetAccountTotal("MISC").ToString());
            row = 33; xlwriter.Write(row, column, lbl.GetAccountTotal("LEGAL").ToString());
            row = 41; xlwriter.Write(row, column, lbl.GetAccountTotal("PROFIT").ToString());
            row = 47; xlwriter.Write(row, column, lbl.GetAccountTotal("WT").ToString());
            row = 49; xlwriter.Write(row, column, lbl.GetAccountTotal("WHT").ToString());

            xlwriter.OpenWorksheet("FA");
            row = 35; column = 1; xlwriter.Write(row, column, FormatDate(enddate));
            column = 3; xlwriter.Write(row, column, "DEP");
            column = 5; xlwriter.Write(row, column, "DEPRECIATION EXPENSE");
            column = 7; xlwriter.Write(row, column, dep1.ToString());
            row = 51; column = 1; xlwriter.Write(row, column, FormatDate(enddate));
            column = 3; xlwriter.Write(row, column, "DEP");
            column = 5; xlwriter.Write(row, column, "DEPRECIATION EXPENSE");
            column = 7; xlwriter.Write(row, column, dep2.ToString());
            row = 68; column = 1; xlwriter.Write(row, column, FormatDate(enddate));
            column = 3; xlwriter.Write(row, column, "DEP");
            column = 5; xlwriter.Write(row, column, "DEPRECIATION EXPENSE");
            column = 7; xlwriter.Write(row, column, dep3.ToString());

            xlwriter.OpenWorksheet("DEP");
            row = 13; column = 1; xlwriter.Write(row, column, FormatDate(enddate));
            column = 3; xlwriter.Write(row, column, "BLDG");
            column = 5; xlwriter.Write(row, column, "DEPRECIATION EXPENSE");
            column = 6; xlwriter.Write(row, column, dep1.ToString());
            row = 14; column = 1; xlwriter.Write(row, column, FormatDate(enddate));
            column = 3; xlwriter.Write(row, column, "F&F");
            column = 5; xlwriter.Write(row, column, "DEPRECIATION EXPENSE");
            column = 6; xlwriter.Write(row, column, dep2.ToString());
            row = 15; column = 1; xlwriter.Write(row, column, FormatDate(enddate));
            column = 3; xlwriter.Write(row, column, "L-INST");
            column = 5; xlwriter.Write(row, column, "DEPRECIATION EXPENSE");
            column = 6; xlwriter.Write(row, column, dep3.ToString());

            int income = lbl.GetAccountTotal("DOT1") + lbl.GetAccountTotal("DOT2")
                + lbl.GetAccountTotal("BOX") + lbl.GetAccountTotal("M-SUB")
                + lbl.GetAccountTotal("RENT") + lbl.GetAccountTotal("LOUD")
                + lbl.GetAccountTotal("WSC");
            int opex = lbl.GetAccountTotal("M-EXP") + lbl.GetAccountTotal("R&M")
                + lbl.GetAccountTotal("INS") + lbl.GetAccountTotal("SW")
                + dep1 + dep2 + dep3 + lbl.GetAccountTotal("ELEC") + lbl.GetAccountTotal("GAS")
                + lbl.GetAccountTotal("MISC") + lbl.GetAccountTotal("LEGAL");
            int surplus = income - opex + lbl.GetAccountTotal("PROFIT")
                - lbl.GetAccountTotal("WT") - lbl.GetAccountTotal("WHT");

            int fassets = lbl.GetAccountTotal("LAND") + lbl.GetAccountTotal("BLDG")
                + lbl.GetAccountTotal("F&F") + lbl.GetAccountTotal("L-INST")
                + lbl.GetAccountTotal("E-INST") + lbl.GetAccountTotal("W-COOL") - dep;

            xlwriter.OpenWorksheet("BS");
            row = 12; column = 4; xlwriter.Write(row, column, lbl.GetAccountTotal("GF").ToString());
            row = 13; column = 4; xlwriter.Write(row, column, surplus.ToString());
            row = 15; xlwriter.Write(row, column, lbl.GetAccountTotal("CAP-DOT").ToString());
            row = 20; xlwriter.Write(row, column, (int.Parse(ssc) + int.Parse(dsc)).ToString());
            row = 23; xlwriter.Write(row, column, lbl.GetAccountTotal("CASH").ToString());
            row = 26; xlwriter.Write(row, column, (0 - lbl.GetAccountTotal("AUD2")).ToString());

            xlwriter.OpenWorksheet("GFUND");
            row = 14; column = 1; xlwriter.Write(row, column, FormatDate(enddate));
            column = 5; xlwriter.Write(row, column, "FROM INCOME AND EXPENDITURE");
            column = 7; xlwriter.Write(row, column, lbl.GetAccountTotal("PROFIT").ToString());

            xlwriter.Save();

        }
        return 0;
    }*/


    }
}
