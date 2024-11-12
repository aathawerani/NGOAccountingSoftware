using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication
{
    class BIBAccountingSheet : AccountingSheets
    {
        public BIBAccountingSheet()
        {
            sourceFileName = "BIB.xlsx";
            destFileName = "BIB";
            accountingSheets.Add("CASH", new ExcelSheet("CASH", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("M-SUB", new ExcelSheet("M-SUB", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("M-EXP", new ExcelSheet("M-EXP", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("L-CHGS", new ExcelSheet("LOUD", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("ELEC", new ExcelSheet("ELEC", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("GAS", new ExcelSheet("GAS", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("DOT1", new ExcelSheet("DON", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("DOT2", new ExcelSheet("D MOH", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("BOX", new ExcelSheet("BOXES", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("S&W", new ExcelSheet("S&W", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("MISC", new ExcelSheet("MISC", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("INS", new ExcelSheet("INS", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("RENT", new ExcelSheet("RENT", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("WSC", new ExcelSheet("WATER", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("R&M", new ExcelSheet("REPAIR", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("PT", new ExcelSheet("TAXES", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("WT", new ExcelSheet("TAXES", 33, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("CT", new ExcelSheet("TAXES", 60, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("WHT", new ExcelSheet("TAXES", 79, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("LEGAL", new ExcelSheet("LEGAL", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("AWO", new ExcelSheet("WRITTEN OFF", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("BANK", new ExcelSheet("BANK", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("PROFIT", new ExcelSheet("PROFIT", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("SSC", new ExcelSheet("INVEST", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("DSC", new ExcelSheet("INVEST", 119, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("BEH", new ExcelSheet("INVEST", 257, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("AUD1", new ExcelSheet("AUDIT", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("AUD2", new ExcelSheet("AUDIT", 34, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("LAND", new ExcelSheet("FA", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("BLDG", new ExcelSheet("FA", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("F&F", new ExcelSheet("FA", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("L-INST", new ExcelSheet("FA", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("E-INST", new ExcelSheet("FA", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("W-COOL", new ExcelSheet("FA", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("DEP", new ExcelSheet("DEP", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("KESC", new ExcelSheet("CA", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("SSGC", new ExcelSheet("CA", 34, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("GF", new ExcelSheet("G FUND", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("CAP-DOT", new ExcelSheet("CAP-DOT", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("LOAN", new ExcelSheet("LOAN", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
        }
    }
}
