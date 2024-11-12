using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using TrustApplication.Exceptions;

namespace TrustApplication
{
    class ExcelReader
    {
        XLWorkbook xlWorkBook;
        IXLWorksheet xlWorkSheet;
        public ExcelReader(string FileName)
        {
            xlWorkBook = new XLWorkbook(FileName);
        }

        public void OpenWorksheet(string SheetName)
        {
            if (!xlWorkBook.Worksheets.TryGetWorksheet(SheetName, out xlWorkSheet))
            {
                throw new ExcelReaderException("Could not open worksheet");
            }
        }
        public string Read(int row, int column)
        {
            //xlWorkSheet.Cells[row, column].Value = value;
            //xlWorkBook.Save();
            return xlWorkSheet.Cell(row, column).Value.ToString(); ;
        }

    }
}
