using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication
{
    class HTTTYearlyStatement : HTTTAccountingSheet
    {
        public HTTTYearlyStatement()
        {
            accountingSheets.Add("GFIS", new ExcelSheet("IS", 82, 4, new string[] { "" });
            accountingSheets.Add("SSCIS", new ExcelSheet("IS", 89, 4, new string[] { "" }));
            accountingSheets.Add("DSCIS", new ExcelSheet("IS", 90, 4, new string[] { "" }));
            accountingSheets.Add("CASHIS", new ExcelSheet("IS", 94, 4, new string[] { "" }));
            accountingSheets.Add("BANKIS", new ExcelSheet("IS", 95, 4, new string[] { "" }));
            accountingSheets.Add("R21BR1IS", new ExcelSheet("IS", 99, 4, new string[] { "" }));
            accountingSheets.Add("R4BR1IS", new ExcelSheet("IS", 100, 4, new string[] { "" }));
            accountingSheets.Add("R2BR1IS", new ExcelSheet("IS", 101, 4, new string[] { "" }));
            accountingSheets.Add("R46GK7IS", new ExcelSheet("IS", 102, 4, new string[] { "" }));
            accountingSheets.Add("PTAXESIS", new ExcelSheet("IS", 106, 4, new string[] { "" }));
            accountingSheets.Add("WTAXESIS", new ExcelSheet("IS", 107, 4, new string[] { "" }));
            accountingSheets.Add("WCHARGESIS", new ExcelSheet("IS", 110, 4, new string[] { "" }));
            accountingSheets.Add("DEPIS", new ExcelSheet("IS", 112, 4, new string[] { "" }));
            accountingSheets.Add("M&SIS", new ExcelSheet("IS", 114, 4, new string[] { "" }));
            accountingSheets.Add("LEGALIS", new ExcelSheet("IS", 115, 4, new string[] { "" }));
            accountingSheets.Add("AUD1IS", new ExcelSheet("IS", 116, 4, new string[] { "" }));
            accountingSheets.Add("PROFITIS", new ExcelSheet("IS", 120, 4, new string[] { "" }));

            accountingSheets.Add("FUNDBS", new ExcelSheet("BS", 8, 4, new string[] { "" }));
            accountingSheets.Add("FABS", new ExcelSheet("BS", 18, 4, new string[] { "" }));
            accountingSheets.Add("INVESTBS", new ExcelSheet("BS", 20, 4, new string[] { "" }));
            accountingSheets.Add("CASHBS", new ExcelSheet("BS", 23, 4, new string[] { "" }));
            accountingSheets.Add("AUD2", new ExcelSheet("BS", 26, 4, new string[] { "" }));
        }
    }
}
