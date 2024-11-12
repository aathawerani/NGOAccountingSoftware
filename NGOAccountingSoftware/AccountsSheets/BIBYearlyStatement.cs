using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication
{
    class BIBYearlyStatement : BIBAccountingSheet
    {
        public BIBYearlyStatement()
        {
            accountingSheets.Add("SSCNOTES", new ExcelSheet("NOTES", 97, 4, new string[] { "" }));
            accountingSheets.Add("DSCNOTES", new ExcelSheet("NOTES", 98, 4, new string[] { "" }));
            accountingSheets.Add("BEHNOTES", new ExcelSheet("NOTES", 99, 4, new string[] { "" }));
            accountingSheets.Add("KESCNOTES", new ExcelSheet("NOTES", 105, 4, new string[] { "" }));
            accountingSheets.Add("SSGCNOTES", new ExcelSheet("NOTES", 106, 4, new string[] { "" }));
            accountingSheets.Add("CASHNOTES", new ExcelSheet("NOTES", 112, 4, new string[] { "" }));
            accountingSheets.Add("BANKNOTES", new ExcelSheet("NOTES", 113, 4, new string[] { "" }));
            accountingSheets.Add("DOT1NOTES", new ExcelSheet("NOTES", 119, 4, new string[] { "" }));
            accountingSheets.Add("BOXNOTES", new ExcelSheet("NOTES", 120, 4, new string[] { "" }));
            accountingSheets.Add("DOT2NOTES", new ExcelSheet("NOTES", 121, 4, new string[] { "" }));
            accountingSheets.Add("CAP-DOTNOTES", new ExcelSheet("NOTES", 128, 4, new string[] { "" }));

            accountingSheets.Add("DOTIS", new ExcelSheet("IS", 14, 4, new string[] { "" }));
            accountingSheets.Add("M-SUBIS", new ExcelSheet("IS", 15, 4, new string[] { "" }));
            accountingSheets.Add("RENTIS", new ExcelSheet("IS", 16, 4, new string[] { "" }));
            accountingSheets.Add("L21BR1IS", new ExcelSheet("IS", 17, 4, new string[] { "" }));
            accountingSheets.Add("WSCIS", new ExcelSheet("IS", 18, 4, new string[] { "" }));
            accountingSheets.Add("M-EXPIS", new ExcelSheet("IS", 24, 4, new string[] { "" }));
            accountingSheets.Add("R&MIS", new ExcelSheet("IS", 25, 4, new string[] { "" }));
            accountingSheets.Add("INSIS", new ExcelSheet("IS", 26, 4, new string[] { "" }));
            accountingSheets.Add("S&WIS", new ExcelSheet("IS", 27, 4, new string[] { "" }));
            accountingSheets.Add("DEPIS", new ExcelSheet("IS", 28, 4, new string[] { "" }));
            accountingSheets.Add("ELECIS", new ExcelSheet("IS", 29, 4, new string[] { "" }));
            accountingSheets.Add("GASIS", new ExcelSheet("IS", 30, 4, new string[] { "" }));
            accountingSheets.Add("MISCIS", new ExcelSheet("IS", 32, 4, new string[] { "" }));
            accountingSheets.Add("LEGALIS", new ExcelSheet("IS", 33, 4, new string[] { "" }));
            accountingSheets.Add("PROFITIS", new ExcelSheet("IS", 41, 4, new string[] { "" }));
            accountingSheets.Add("WTIS", new ExcelSheet("IS", 47, 4, new string[] { "" }));
            accountingSheets.Add("WHTIS", new ExcelSheet("IS", 49, 4, new string[] { "" }));

            accountingSheets.Add("GFBS", new ExcelSheet("BS", 12, 4, new string[] { "" }));
            accountingSheets.Add("SURPLUSBS", new ExcelSheet("BS", 13, 4, new string[] { "" }));
            accountingSheets.Add("CAP-DOTBS", new ExcelSheet("BS", 15, 4, new string[] { "" }));
            accountingSheets.Add("INVESTBS", new ExcelSheet("BS", 20, 4, new string[] { "" }));
            accountingSheets.Add("CASHBS", new ExcelSheet("BS", 23, 4, new string[] { "" }));
            accountingSheets.Add("AUD2", new ExcelSheet("BS", 26, 4, new string[] { "" }));
        }
    }
}
