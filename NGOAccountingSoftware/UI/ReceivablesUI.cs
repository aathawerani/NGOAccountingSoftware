using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TrustApplication.Exceptions;

namespace TrustApplication
{
    class ReceivablesUI
    {
        ReceivablesBL bl = new ReceivablesBL();
        ComboBox Receivable_Trust_comboBox; DataGrid Receivable_Grid; TextBox Receivable_ID_textBox;
        public void SetRentControls(ComboBox _Receivable_Trust_comboBox, DataGrid _Receivable_Grid, TextBox _Receivable_ID_textBox)
        {
            Receivable_Trust_comboBox = _Receivable_Trust_comboBox; Receivable_Grid = _Receivable_Grid;
            Receivable_ID_textBox = _Receivable_ID_textBox;
        }

        public void ReceivablesTab()
        {
            Receivable_Trust_comboBox.ItemsSource = bl.GetTrustNames();
        }

        public void Receivable_Trust_comboBox_SelectionChanged()
        {
            if (Receivable_Trust_comboBox.SelectedIndex < 0)
            {
                throw new ReceivablesUIException("Please select Trust");
            }
            int index = Receivable_Trust_comboBox.SelectedIndex + 1;
            Receivable_Grid.ItemsSource = bl.GetReceivables(index.ToString());
        }
        public void Receivables_Receivedbutton_Click()
        {
            if (Receivable_Trust_comboBox.SelectedIndex < 0)
            {
                throw new ReceivablesUIException("Please select Trust");
            }
            if (Receivable_Grid.SelectedIndex < 0)
            {
                throw new ReceivablesUIException("Please selet grid row");
            }
            string ReceivableID = Receivable_ID_textBox.Text;
            bl.UpdateReceivables(ReceivableID);
        }
    }
}
