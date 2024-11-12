using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication
{
    abstract class RentReceipt : Receipt
    {
        public abstract void PrintReceipt(string RentDate, string RentSerialNo, int RentTrustCode, string RentTrustPlotCode,
            string RentSpaceType, string RentSpaceNo, string MonthlyRent, string MonthlyWater, string RentTenantName,
            string RentArears, string WaterArears, string RentTotalRent, string RentTotalWaterCharges, string RentTotalAmount,
            DateTime FromDate, DateTime ToDate, string CNIC);
    }
}
