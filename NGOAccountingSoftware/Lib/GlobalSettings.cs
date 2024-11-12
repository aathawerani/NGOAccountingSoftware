using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication
{
    class GlobalSettings
    {
        static GlobalSettings gs = new GlobalSettings();

        public DateTime StartDate, EndDate;
        public string[] Months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        public string[] NumMonths = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24" };
        public string[] days = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30" };
        public string[] IslamicMonths = { "Moharram", "Safar", "Rabi Ul Awwal", "Rabi Ul Aakhir", "Jamadi Ul Awwal", "Jamadi Ul Aakhir", "Rajab", "Shaban", "Ramazan", "Shawwal", "Zil Quad", "Zil Hajj" };
        public string[] time = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
        public string[] AM = { "AM", "PM" };
        public string sourceDIR = "master/";
        public string destDIR = "export";
        private GlobalSettings() { }

        public static GlobalSettings GetInstance()
        {
            return gs;
        }
    }
}
