using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication
{
    class HVHTYearlyStatement : HVHTAccountingSheet
    {
        public HVHTYearlyStatement()
        {
            accountingSheets.Add("RGK6IS", new ExcelSheet("IS", 14, 5, new string[] { "" }));
            accountingSheets.Add("GFIS", new ExcelSheet("IS", 79, 5, new string[] { "" }));
            accountingSheets.Add("RRPMIS", new ExcelSheet("IS", 81, 5, new string[] { "" }));
            accountingSheets.Add("IRCIS", new ExcelSheet("IS", 86, 5, new string[] { "" }));
            accountingSheets.Add("ICHARPAID", new ExcelSheet("IS", 88, 5, new string[] { "" }));
            accountingSheets.Add("KHCIS", new ExcelSheet("IS", 91, 5, new string[] { "" }));
            accountingSheets.Add("KCHARPAID", new ExcelSheet("IS", 93, 5, new string[] { "" }));
            accountingSheets.Add("SSCIS", new ExcelSheet("IS", 97, 5, new string[] { "" }));
            accountingSheets.Add("DSCIS", new ExcelSheet("IS", 98, 5, new string[] { "" }));
            accountingSheets.Add("CASHIS", new ExcelSheet("IS", 102, 5, new string[] { "" }));
            accountingSheets.Add("WHTIS", new ExcelSheet("IS", 105, 5, new string[] { "" }));
            accountingSheets.Add("WTGK6IS", new ExcelSheet("IS", 106, 5, new string[] { "" }));
            accountingSheets.Add("WGK6IS", new ExcelSheet("IS", 109, 5, new string[] { "" }));
            accountingSheets.Add("DGKIS", new ExcelSheet("IS", 111, 5, new string[] { "" }));
            accountingSheets.Add("LEGALIS", new ExcelSheet("IS", 112, 5, new string[] { "" }));
            accountingSheets.Add("AUD1IS", new ExcelSheet("IS", 113, 5, new string[] { "" }));
            accountingSheets.Add("PROFITIS", new ExcelSheet("IS", 117, 5, new string[] { "" }));
            accountingSheets.Add("DOTIS", new ExcelSheet("IS", 118, 5, new string[] { "" }));

            accountingSheets.Add("FUNDBS", new ExcelSheet("BS", 8, 5, new string[] { "" }));
            accountingSheets.Add("FABS", new ExcelSheet("BS", 18, 5, new string[] { "" }));
            accountingSheets.Add("SSCDSCBS", new ExcelSheet("BS", 20, 5, new string[] { "" }));
            accountingSheets.Add("CASHBS", new ExcelSheet("BS", 23, 5, new string[] { "" }));
            accountingSheets.Add("AUD2BS", new ExcelSheet("BS", 26, 5, new string[] { "" }));
        }

    }
}
