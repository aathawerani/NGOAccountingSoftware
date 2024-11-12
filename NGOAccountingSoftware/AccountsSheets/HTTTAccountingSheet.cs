using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication 
{
    class HTTTAccountingSheet : AccountingSheets
    {
        public HTTTAccountingSheet()
        {
            sourceFileName = "HTTT.xlsx";
            destFileName = "HTTT";
            accountingSheets.Add("CASH", new ExcelSheet("CASH", 13, 1, new string[]{"Date", "No", "Account", "Name", "Particulars", "Debit", "Credit"}));
            accountingSheets.Add("R46GK7", new ExcelSheet("RENT 46 GK7", 13, 1, new string[] { "Date", "No", "Account", "Name", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("R2BR1", new ExcelSheet("RENT 2BR1", 13, 1, new string[] { "Date", "No", "Account", "Name", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("R4BR1", new ExcelSheet("RENT 4BR1", 13, 1, new string[] { "Date", "No", "Account", "Name", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("R21BR1", new ExcelSheet("RENT 21BR1", 13, 1, new string[] { "Date", "No", "Account", "Name", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("W46GK7", new ExcelSheet("W 46GK7", 13, 1, new string[] { "Date", "No", "Account", "Name", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("W2BR1", new ExcelSheet("W 2BR1", 13, 1, new string[] { "Date", "No", "Account", "Name", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("W4BR1", new ExcelSheet("W 4BR1", 13, 1, new string[] { "Date", "No", "Account", "Name", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("W21BR1", new ExcelSheet("W 21BR1", 13, 1, new string[] { "Date", "No", "Account", "Name", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("PT4BR1", new ExcelSheet("TAX 4BR1", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("WT4BR1", new ExcelSheet("TAX 4BR1", 30, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("CT4BR1", new ExcelSheet("TAX 4BR1", 49, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("PT46GK7", new ExcelSheet("TAX 46GK7", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("WT46GK7", new ExcelSheet("TAX 46GK7", 30, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("CT46GK7", new ExcelSheet("TAX 46GK7", 49, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("PT2BR1", new ExcelSheet("TAX 2BR1", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("WT2BR1", new ExcelSheet("TAX 2BR1", 30, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("CT2BR1", new ExcelSheet("TAX 2BR1", 49, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("PT21BR1", new ExcelSheet("TAX 21BR1", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("WT21BR1", new ExcelSheet("TAX 21BR1", 30, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("CT21BR1", new ExcelSheet("TAX 21BR1", 49, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("PROFIT", new ExcelSheet("PROFIT", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("WHT", new ExcelSheet("WHT", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("BANK", new ExcelSheet("BANK", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("M&S", new ExcelSheet("M&S", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("GF", new ExcelSheet("GFUND", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("DEP", new ExcelSheet("DEP", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("AUD1", new ExcelSheet("AUDIT", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("AUD2", new ExcelSheet("AUDIT", 34, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("L46GK7", new ExcelSheet("FA 46GK7", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("B46GK7", new ExcelSheet("FA 46GK7", 30, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("L2BR1", new ExcelSheet("FA 2BR1", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("B2BR1", new ExcelSheet("FA 2BR1", 30, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("L4BR1", new ExcelSheet("FA 4BR1", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("B4BR1", new ExcelSheet("FA 4BR1", 30, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("L21BR1", new ExcelSheet("FA 21BR1", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("B21BR1", new ExcelSheet("FA 21BR1", 30, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("SSC", new ExcelSheet("SSC", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("DSC", new ExcelSheet("DSC", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("LOAN", new ExcelSheet("LOAN", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));
            accountingSheets.Add("LEGAL", new ExcelSheet("LEGAL", 13, 1, new string[] { "Date", "No", "Account", "Particulars", "Debit", "Credit" }));

        }
    }
}
