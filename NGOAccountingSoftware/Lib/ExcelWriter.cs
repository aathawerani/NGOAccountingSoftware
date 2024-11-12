using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using TrustApplication.Exceptions;

namespace TrustApplication
{
    class ExcelWriter
    {
        XLWorkbook xlWorkBook;
        IXLWorksheet xlWorkSheet;
        string FileName;
        public ExcelWriter(string _FileName)
        {
            FileName = _FileName;
        }
        public void OpenWorksheet(string SheetName)
        {
            xlWorkBook = new XLWorkbook(FileName);
            if (xlWorkBook == null)
            {
                throw new ExcelWriterException("Could not open excel work book");
            }
            xlWorkSheet = xlWorkBook.Worksheet(SheetName);
            if (xlWorkSheet == null)
            {
                throw new ExcelWriterException("Could not open excel sheet");
            }
        }
        public void Write(int row, int column, string value)
        {
            //xlWorkSheet.Cells[row, column].Value = value;
            xlWorkSheet.Cell(row, column).Value = value;
            //xlWorkBook.Save();
        }
        public void Save()
        {
            xlWorkBook.Save();
        }
    }
}
