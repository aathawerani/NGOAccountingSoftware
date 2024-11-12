using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustApplication.Exceptions;

namespace TrustApplication
{
    class MajlisBL : BL
    {
        //Utils util = new Utils(); 
        MajlisDAL dal = new MajlisDAL(); AccountsBL bl = new AccountsBL();
        //AccountsDAL adal = new AccountsDAL();

        public string GetTotalPrice(string Unit, string Price)
        {
            double U; 
            if (!double.TryParse(Unit, out U))
                throw new MajlisBLException("Failed to parse Unit"); 
            double P; 
            if (!double.TryParse(Price, out P))
                throw new MajlisBLException("Failed to parse Price"); 
            double Total = U * P;
            return Total.ToString();
        }
        public string CalculateMajlisBill(string MilkUnit, string MilkUnitPrice, string SugarUnit, string SugarUnitPrice, string TeaUnit,
            string TeaUnitPrice, string SaffronPrice, string CardamomsPrice, string PistachiosPrice, string IcePrice,
            string EssenceColor, string MiscellaneousExpense, string LightFanExpense, string GasExpense, string LoudSpeakerExpense,
            string MolanaHadya)
        {
            double MilkU; 
            if (!double.TryParse(MilkUnit, out MilkU)) 
                throw new MajlisBLException("Failed to parse milk unit"); 
            double MilkUP; 
            if (!double.TryParse(MilkUnitPrice, out MilkUP))
                throw new MajlisBLException("Failed to parse milk unit price"); 
            double SugarU; 
            if (!double.TryParse(SugarUnit, out SugarU))
                throw new MajlisBLException("Failed to parse sugar unit"); 
            double SugarUP; 
            if (!double.TryParse(SugarUnitPrice, out SugarUP))
                throw new MajlisBLException("Failed to parse sugar unit price"); 
            double TeaU; 
            if (!double.TryParse(TeaUnit, out TeaU))
                throw new MajlisBLException("Failed to parse tea unit"); 
            double TeaUP; 
            if (!double.TryParse(TeaUnitPrice, out TeaUP))
                throw new MajlisBLException("Failed to parse tea unit price"); 
            double Saffron; 
            if (!double.TryParse(SaffronPrice, out Saffron))
                throw new MajlisBLException("Failed to parse saffron price"); 
            double Cardamoms; 
            if (!double.TryParse(CardamomsPrice, out Cardamoms))
                throw new MajlisBLException("Failed to parse cardamoms price"); 
            double Pistachios; 
            if (!double.TryParse(PistachiosPrice, out Pistachios))
                throw new MajlisBLException("Failed to parse pistachios price"); 
            double Ice; 
            if (!double.TryParse(IcePrice, out Ice))
                throw new MajlisBLException("Failed to parse ice price"); 
            double Essense; 
            if (!double.TryParse(EssenceColor, out Essense))
                throw new MajlisBLException("Failed to parse essence color"); 
            double Miscellaneous; 
            if (!double.TryParse(MiscellaneousExpense, out Miscellaneous))
                throw new MajlisBLException("Failed to parse miscellaneous expense"); 
            double LightFan; 
            if (!double.TryParse(LightFanExpense, out LightFan))
                throw new MajlisBLException("Failed to parse light fan expense"); 
            double Gas; 
            if (!double.TryParse(GasExpense, out Gas))
                throw new MajlisBLException("Failed to parse gas expense"); 
            double LoudSpeaker; 
            if (!double.TryParse(LoudSpeakerExpense, out LoudSpeaker))
                throw new MajlisBLException("Failed to parse loud speaker expense"); 
            double Molana; 
            if (!double.TryParse(MolanaHadya, out Molana))
                throw new MajlisBLException("Failed to parse molana expense"); 
            double TotalMilk = MilkU * MilkUP;
            double TotalSugar = SugarU * SugarUP;
            double TotalTea = TeaU * TeaUP;
            double TotalBill = TotalMilk + TotalSugar + TotalTea + Saffron + Cardamoms + Essense + Ice + Pistachios + Miscellaneous + LightFan
                + Gas + LoudSpeaker + Molana;
            return TotalBill.ToString();
        }
        public void InsertMajlisBill(string MajlisDate, string MajlisIslamicDay, string MajlisIslamicMonth, string MajlisIslamicYear, string MajlisSerialNo
            , string MajlistFromTime, string MajlisToTime, string MajlisName, string MajlisMilkQuantity, string MajlisMilkPricePerUnit, string MajlisMilkTotal,
            string MajlisSugarQuantity, string MajlisSugarPricePerUnit, string MajlisSugarTotal, string MajlisTeaQuantity, string MajlisTeaPricePerUnit,
            string MajlisTeaTotal, string MajlisSaffron, string MajlisCardamoms, string MajlisPistachios, string MajlisIce, string MajlisEssence,
            string MajlisMiscellaneous, string MajlisLightsFans, string MajlisGas, string MajlisLoudSpeaker, string MajlisMolana, string MajlisTotalAmount,
            string MajlisMiscDesc, string Particulars)
        {
            //double MilkU; double.TryParse(MajlisMilkQuantity, out MilkU);
            //double MilkUP; double.TryParse(MajlisMilkPricePerUnit, out MilkUP);
            //double SugarU; double.TryParse(MajlisSugarQuantity, out SugarU);
            //double SugarUP; double.TryParse(MajlisSugarPricePerUnit, out SugarUP);
            //double TeaU; double.TryParse(MajlisTeaQuantity, out TeaU);
            //double TeaUP; double.TryParse(MajlisTeaPricePerUnit, out TeaUP);
            double Saffron; 
            if (!double.TryParse(MajlisSaffron, out Saffron))
                throw new MajlisBLException("Failed to parse Saffron expense"); 
            double Cardamoms; 
            if (!double.TryParse(MajlisCardamoms, out Cardamoms))
                throw new MajlisBLException("Failed to parse Cardamoms expense"); 
            double Pistachios; 
            if (!double.TryParse(MajlisPistachios, out Pistachios))
                throw new MajlisBLException("Failed to parse Pistachios expense"); 
            double Ice; 
            if (!double.TryParse(MajlisIce, out Ice))
                throw new MajlisBLException("Failed to parse Ice expense"); 
            double Essense; 
            if (!double.TryParse(MajlisEssence, out Essense))
                throw new MajlisBLException("Failed to parse Essense expense"); 
            double Miscellaneous; 
            if (!double.TryParse(MajlisMiscellaneous, out Miscellaneous))
                throw new MajlisBLException("Failed to parse Miscellaneous expense"); 
            double LightFan; 
            if (!double.TryParse(MajlisLightsFans, out LightFan))
                throw new MajlisBLException("Failed to parse Light Fan expense"); 
            double Gas; 
            if (!double.TryParse(MajlisGas, out Gas))
                throw new MajlisBLException("Failed to parse Gas expense"); 
            double LoudSpeaker; 
            if (!double.TryParse(MajlisLoudSpeaker, out LoudSpeaker))
                throw new MajlisBLException("Failed to parse LoudSpeaker expense"); 
            double Molana; 
            if (!double.TryParse(MajlisMolana, out Molana))
                throw new MajlisBLException("Failed to parse molana expense"); 
            double TotalMilk; 
            if (!double.TryParse(MajlisMilkTotal, out TotalMilk))
                throw new MajlisBLException("Failed to parse Total Milk expense"); 
            double TotalSugar; 
            if (!double.TryParse(MajlisSugarTotal, out TotalSugar))
                throw new MajlisBLException("Failed to parse Total Sugar expense"); 
            double TotalTea; 
            if (!double.TryParse(MajlisTeaTotal, out TotalTea))
                throw new MajlisBLException("Failed to parse Total Tea expense"); 

            dal.InsertMajlisBill(MajlisDate, MajlisIslamicDay, MajlisIslamicMonth, MajlisIslamicYear, MajlisSerialNo
            , MajlistFromTime, MajlisToTime, MajlisName, MajlisMilkQuantity, MajlisMilkPricePerUnit, MajlisMilkTotal,
            MajlisSugarQuantity, MajlisSugarPricePerUnit, MajlisSugarTotal, MajlisTeaQuantity, MajlisTeaPricePerUnit,
            MajlisTeaTotal, MajlisSaffron, MajlisCardamoms, MajlisPistachios, MajlisIce, MajlisEssence,
            MajlisMiscellaneous, MajlisLightsFans, MajlisGas, MajlisLoudSpeaker, MajlisMolana, MajlisTotalAmount, MajlisMiscDesc);

            double BillExceptLS = TotalMilk + TotalSugar + TotalTea + Saffron + Cardamoms + Essense + Ice + Pistachios + Miscellaneous + LightFan
                + Gas + Molana;

            string Particulars1 = Particulars;
            int TrustCode = 3;
            string AccountCode = bl.GetAccountTypeCodesByAccountCode(TrustCode, "M-SUB");
            List<string> accounttpecodeslist = bl.GetContraAccountTypeCodes(TrustCode, AccountCode);
            string ContraAccount = accounttpecodeslist.FirstOrDefault();
            string majlisdate = util.FormatDate(MajlisDate);
            bl.AccountingEntries(TrustCode, AccountCode, majlisdate, MajlisSerialNo, MajlisName, ContraAccount, 
                Particulars1, "0", BillExceptLS.ToString(), "2", "M-SUB");
            if (LoudSpeaker > 0)
            {
                Particulars1 = "RECEIVED FROM " + MajlisName + " L/S CHGS";
                AccountCode = bl.GetAccountTypeCodesByAccountCode(TrustCode, "L-CHGS");
                accounttpecodeslist = bl.GetContraAccountTypeCodes(TrustCode, AccountCode);
                ContraAccount = accounttpecodeslist.FirstOrDefault();
                bl.AccountingEntries(TrustCode, AccountCode, majlisdate, MajlisSerialNo, MajlisName, ContraAccount, 
                    Particulars1, "0", LoudSpeaker.ToString(), "2", "L-CHGS");
            }
        }
        public void PrintMajlisBill(string MajlisDate, string MajlisIslamicDay, string MajlisIslamicMonth, string MajlisIslamicYear, string MajlisSerialNo
            , string MajlistFromTime, string MajlisToTime, string MajlisName, string MajlisMilkQuantity, string MajlisMilkPricePerUnit, string MajlisMilkTotal,
            string MajlisSugarQuantity, string MajlisSugarPricePerUnit, string MajlisSugarTotal, string MajlisTeaQuantity, string MajlisTeaPricePerUnit,
            string MajlisTeaTotal, string MajlisSaffron, string MajlisCardamoms, string MajlisPistachios, string MajlisIce, string MajlisEssence,
            string MajlisMiscellaneous, string MajlisLightsFans, string MajlisGas, string MajlisLoudSpeaker, string MajlisMolana, string MajlisTotalAmount,
            string MajlisMiscDesc, string Particulars)
        {
            string FileName = "";
            string DestFileName = "";
            Dictionary<string, string> StringToReplace = new Dictionary<string, string>();

            FileName = "BIBReceipt.docx";
            DestFileName = "BIBReceipt" + DateTime.Now.ToString("yyyyMMddhhmm") + ".docx";
            StringToReplace.Add("AAAAA", MajlisSerialNo);
            StringToReplace.Add("BBBBBBBBBB", util.TrimTimeDateGB(MajlisDate));
            StringToReplace.Add("NNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN", MajlisName);
            StringToReplace.Add("CCCCCCCCCCCCCCCCCCCCCCCCC", MajlisIslamicDay + " " + MajlisIslamicMonth + " " + MajlisIslamicYear);
            StringToReplace.Add("DDDDDD", MajlistFromTime);
            StringToReplace.Add("EEEEEE", MajlisToTime);
            StringToReplace.Add("FFF", MajlisMilkQuantity);
            StringToReplace.Add("GGG", MajlisSugarQuantity);
            StringToReplace.Add("HHH", MajlisTeaQuantity);
            StringToReplace.Add("IIIII", MajlisLightsFans);
            StringToReplace.Add("JJJJJ", MajlisMilkTotal);
            StringToReplace.Add("KKKKK", MajlisSugarTotal);
            StringToReplace.Add("LLLLL", MajlisTeaTotal);
            StringToReplace.Add("MMMMM", MajlisSaffron);
            StringToReplace.Add("NNNNN", MajlisCardamoms);
            StringToReplace.Add("OOOOO", MajlisPistachios);
            StringToReplace.Add("PPPPP", MajlisIce);
            StringToReplace.Add("QQQQQ", MajlisEssence);
            StringToReplace.Add("RRRRR", MajlisGas);
            StringToReplace.Add("SSSSS", MajlisMiscellaneous);
            StringToReplace.Add("TTTTT", MajlisMolana);
            StringToReplace.Add("UUUUU", MajlisLoudSpeaker);
            StringToReplace.Add("VVVVV", MajlisTotalAmount);

            FileManager fm = new FileManager("receipt", DestFileName);
            fm.CreateFile();
            fm.CopyTo("master/", FileName);
            string FullName = fm.GetFileName();
            DocWriter dw = new DocWriter();
            dw.SearchAndReplace(FullName, StringToReplace);
            PrintDocument pd = new PrintDocument();
            FullName = fm.GetAbsolutePath();
            pd.Print(FullName);
        }

        public string GetMajlisParticulars(string Name)
        {
            return "RECEIVED FROM " + Name + " FOR MAJLIS";
        }
    }
}
