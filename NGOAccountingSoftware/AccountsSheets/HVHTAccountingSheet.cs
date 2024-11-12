using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication
{
    class HVHTAccountingSheet: AccountingSheets
    {
        public HVHTAccountingSheet()
        {
            sourceFileName = "HVHT.xlsx";
            destFileName = "HVHT";

            accountingSheets.Add("CASH", new ExcelSheet("CASH", 13, 1, new string[] { "Date", "No", "Account", "Name", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("RGK6", new ExcelSheet("RENT GK6", 13, 1, new string[] { "Date", "No", "Account", "Name", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("WGK6", new ExcelSheet("W GK6", 14, 1, new string[] { "Date", "No", "Account", "Name", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("PTGK6", new ExcelSheet("TAX GK6", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("WTGK6", new ExcelSheet("TAX GK6", 30, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("CTGK6", new ExcelSheet("TAX GK6", 49, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("PROFIT", new ExcelSheet("PROFIT", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("WHT", new ExcelSheet("WHT", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("LEGAL", new ExcelSheet("LEGAL", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("IRC", new ExcelSheet("IRAQ", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("KHC", new ExcelSheet("KARACHI", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("RRPM", new ExcelSheet("RES", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("GF", new ExcelSheet("FUND", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("LOAN", new ExcelSheet("LOAN", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("SSC", new ExcelSheet("SSC", 14, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("DSC", new ExcelSheet("DSC", 14, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("MOD", new ExcelSheet("MOD", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("AUD1", new ExcelSheet("AUDIT", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("AUD2", new ExcelSheet("AUDIT", 34, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("LGK6/1", new ExcelSheet("FA", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("BGK6/1", new ExcelSheet("FA", 29, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("DGK6/1", new ExcelSheet("FA", 46, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("DOT", new ExcelSheet("DON", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
        }
    }
}
