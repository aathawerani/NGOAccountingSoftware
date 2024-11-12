using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication
{
    class HTTTRent : RentReceipt
    {
        public override void PrintReceipt(string RentDate, string RentSerialNo, int RentTrustCode, string RentTrustPlotCode, 
            string RentSpaceType, string RentSpaceNo, string MonthlyRent, string MonthlyWater, string RentTenantName, 
            string RentArears, string WaterArears, string RentTotalRent, string RentTotalWaterCharges, string RentTotalAmount, 
            DateTime FromDate, DateTime ToDate, string CNIC)
        {
            FileName = "HTReceipt.docx";
            DestFileName = "HTReceipt" + DateTime.Now.ToString("yyyyMMddhhmm") + ".docx";
            //string PlotNo = _PlotNo;
            StringToReplace.Add("AAAAA", RentSerialNo);
            StringToReplace.Add("HHHHHHHHHH", RentTrustPlotCode);
            StringToReplace.Add("BBBBBBBBBB", util.TrimTimeDateGB(RentDate));
            StringToReplace.Add("NNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN", RentTenantName);
            StringToReplace.Add("JJJJJ", RentSpaceType);
            StringToReplace.Add("CC", RentSpaceNo);
            StringToReplace.Add("GGGGGGGGGG", MonthlyRent + " + " + MonthlyWater);
            StringToReplace.Add("DDDDDDDDDD", util.FormatDate(FromDate));
            StringToReplace.Add("FFFFFFFFFF", util.FormatDate(ToDate));
            StringToReplace.Add("EEEEE", RentTotalRent);
            StringToReplace.Add("UUU", RentTotalWaterCharges);
            StringToReplace.Add("LLLLL", RentArears);
            StringToReplace.Add("MMM", WaterArears);
            StringToReplace.Add("PPPPP", RentTotalAmount);
            PrintReceipt();
        }
    }
}
