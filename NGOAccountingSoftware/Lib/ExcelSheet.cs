using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication
{
    class ExcelSheet
    {
        public string SheetName;
        public int Row;
        public int Column;
        public string[] ColumnNames;

        public ExcelSheet(string sheetName, int row, int column, string[] columnNames)
        {
            SheetName = sheetName;
            Row = row;
            Column = column;
            ColumnNames = columnNames;
        }
    }
}
