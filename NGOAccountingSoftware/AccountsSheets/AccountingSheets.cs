using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustApplication.Exceptions;

namespace TrustApplication
{
    class AccountingSheets
    {
        protected string sourceFileName = "", destFileName = "";
        protected GlobalSettings gs = GlobalSettings.GetInstance();
        protected Dictionary<string, ExcelSheet> accountingSheets = new Dictionary<string, ExcelSheet>();
        ExcelWriter xlwriter;
        ExcelReader xlreader;
        Utils util = new Utils();
        int row = 0;
        ExcelSheet accSheet;
        protected string FileName;

        public class XLvalue
        {
            public int row = 0, col = 0;
            public string value;
            public XLvalue(int row, int col, string value)
            {
                this.row = row;
                this.col = col;
                this.value = value;
            }
        }

        public AccountingSheets()
        {
            row = 0;
        }

        public List<string> GetAccounts()
        {
            List<string> list = accountingSheets.Keys.ToList<string>();
            if (list.Count < 0) 
                throw new AccountingSheetsExceptions("GetAccounts: Unable to Get Accounts");
            return accountingSheets.Keys.ToList<string>();
        }

        public List<string> GetAllSheets()
        {
            return accountingSheets.Keys.ToList<string>();
        }

        public void SetAccountingSheet(string str)
        {
            if (!accountingSheets.ContainsKey(str))
            {
                throw new AccountingSheetsExceptions("SetAccountingSheet: Sheet name not found");
            }
            accSheet = accountingSheets[str];
        }

        public void SetReaderFile(string FileName)
        {
            xlreader = new ExcelReader(FileName);
            if (xlreader == null)
                throw new AccountingSheetsExceptions("SetReaderFile: Unable to open excel file");
        }

        public void SetWriterFile()
        {
            destFileName += DateTime.Now.ToString("yyyyMMddhhmm") + ".xlsx";
            FileManager fm = new FileManager(gs.destDIR, destFileName);
            fm.CreateFile();
            fm.CopyTo(gs.sourceDIR, sourceFileName);
            FileName = fm.GetFileName();
            xlwriter = new ExcelWriter(FileName);
            if (xlwriter == null)
                throw new AccountingSheetsExceptions("SetWriterFile: Unable to open excel file");
        }

        public void WriteAccounts(string Date, string No, string Account, string Name, string Particulars,
            string Debit, string Credit)
        {
            WriteAccounts(Date, No, Account, Name, Particulars, Debit, Credit, 0);
        }

        public void WriteAccounts(string Date, string No, string Account, string Name, string Particulars,
        string Debit, string Credit, int Count)
        {
            xlwriter.OpenWorksheet(accSheet.SheetName);
            int row = accSheet.Row + Count, column = accSheet.Column;
            xlwriter.Write(row, column, Date);
            column++;
            if (No != "")
            {
                xlwriter.Write(row, column, No);
            }
            column++;
            if (Account != "")
            {
                xlwriter.Write(row, column, Account);
            }
            column++;
            //if (accSheet.NumColumns < 7)
            //{
                //if (Name != "")
                //{
                    //Particulars = "Received from " + Name + " " + Particulars;
                //}
            //}
            //else
            //{
                //xlwriter.Write(row, column, Name);
            //}
            column++;
            xlwriter.Write(row, column, Particulars);
            column++;
            if (Debit != "" && Debit != "0")
            {
                string amount = util.RawAmount(Debit); 
                xlwriter.Write(row, column, amount); 
            }
            column++;
            if (Credit != "" && Credit != "0")
            {
                string amount = util.RawAmount(Debit); 
                xlwriter.Write(row, column, amount); 
            }
            row++;
            xlwriter.Save(); 
        }

        public void VerifyExcelSheets()
        {
            foreach (string str in accountingSheets.Keys)
            {
                string sheetname = accountingSheets[str].SheetName;
                xlreader.OpenWorksheet(sheetname);
            }
        }

        public void OpenExcelSheet()
        {
            xlreader.OpenWorksheet(accSheet.SheetName);
        }

        public Dictionary<string, string> ReadLine(int row, int column)
        {
            Dictionary<string, string> XLRow = new Dictionary<string, string>();
            //xlreader.OpenWorksheet(accSheet.SheetName);
            //int row = accSheet.Row, column = accSheet.Column;
            foreach(string columnName in accSheet.ColumnNames)
            {
                XLRow.Add(columnName, xlreader.Read(row, column));
                column++;
            }
            //XLRow.Add("Date", xlreader.Read(row, column));
            //column++;
            //XLRow.Add("No", xlreader.Read(row, column));
            //column++;
            //XLRow.Add("Account", xlreader.Read(row, column));
            //column++;
            //XLRow.Add("Name", xlreader.Read(row, column));
            //column++;
            //XLRow.Add("Particulars", xlreader.Read(row, column));
            //column++;
            //XLRow.Add("Debit", xlreader.Read(row, column));
            //column++;
            //XLRow.Add("Credit", xlreader.Read(row, column));
            return XLRow;
        }

        public List<Dictionary<string, string>> ReadAllLines()
        {
            List<Dictionary<string, string>> XLRows = new List<Dictionary<string, string>>();
            int row = accSheet.Row, column = accSheet.Column;
            while (true)
            {
                Dictionary<string, string> XLRow = ReadLine(row, column);
                if(string.IsNullOrEmpty(XLRow["Date"]) && string.IsNullOrEmpty(XLRow["No"]) && string.IsNullOrEmpty(XLRow["Account"])
                    && string.IsNullOrEmpty(XLRow["Name"]) && string.IsNullOrEmpty(XLRow["Particulars"]) && string.IsNullOrEmpty(XLRow["Debit"])
                    && string.IsNullOrEmpty(XLRow["Credit"]))
                {
                    break;
                }
                XLRows.Add(XLRow);
                row++;
            }
            return XLRows;
        }

        public Dictionary<string, List<Dictionary<string, string>>> ReadWholeXL()
        {
            List<XLvalue> XLValues = ReadXLSheetHeader();
            List<Dictionary<string, string>> XLRows = ReadAllLines();
            Dictionary<string, List<Dictionary<string, string>>> XLData = new Dictionary<string, List<Dictionary<string, string>>>();
            Dictionary<string, string> XLHeader = new Dictionary<string, string>();
            foreach(XLvalue xlValue in XLValues)
            {
                if (xlValue.value == "TYPE OF ACCOUNT")
                {
                    for(int i =  0; i < XLValues.Count;i++)
                    {
                        if(XLValues[i].row == xlValue.row && XLValues[i].col != xlValue.col)
                        {
                            string accountType = XLValues[i].value;
                            XLHeader.Add("AccountType", accountType);
                        }
                    }
                }
                else if (xlValue.value == "ACCOUNT CODE")
                {
                    for (int i = 0; i < XLValues.Count; i++)
                    {
                        if (XLValues[i].row == xlValue.row && XLValues[i].col != xlValue.col)
                        {
                            string accountCode = XLValues[i].value;
                            XLHeader.Add("AccountCode", accountCode);
                        }
                    }
                }
                List<Dictionary<string, string>> XLHeaderList = new List<Dictionary<string, string>>();
                XLHeaderList.Add(XLHeader);
                XLData.Add("Header", XLHeaderList);
                XLData.Add("Rows", XLRows);
            }
            return XLData;
        }

        public List<XLvalue> ReadXLSheetHeader()
        {
            int maxColumn = 12, maxRow = 500;
            List<XLvalue> XLValues = new List<XLvalue>();
            for (int row = 1; row < maxRow; row++)
            {
                for (int column = 0; column < maxColumn; column++)
                {
                    string value = xlreader.Read(row, column);
                    if (!string.IsNullOrEmpty(value))
                    {
                        XLValues.Add(new XLvalue(row, column, value));
                    }
                }
            }
            return XLValues;
        }

        public void WriteStatement(string Amount)
        {
            xlwriter.OpenWorksheet(accSheet.SheetName);
            int row = accSheet.Row, column = accSheet.Column;
            xlwriter.Write(row, column, Amount);
            xlwriter.Save();
        }
    }
}
