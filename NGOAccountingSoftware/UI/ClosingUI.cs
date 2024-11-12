using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TrustApplication
{
    class ClosingUI
    {
        ComboBox Closing_Trust_comboBox; ComboBox Closing_Year_comboBox; DataGrid Closing_Grid;
        ClosingBL bl = new ClosingBL();
        public void SetControls(ComboBox _Closing_Trust_comboBox, ComboBox _Closing_Year_comboBox, DataGrid _Closing_Grid)
        {
            Closing_Trust_comboBox = _Closing_Trust_comboBox; Closing_Year_comboBox = _Closing_Year_comboBox;
            Closing_Grid = _Closing_Grid;
        }
        public void ClosingTab()
        {
            Closing_Trust_comboBox.ItemsSource = bl.GetTrustNames();
            Closing_Year_comboBox.ItemsSource = bl.GetAccountingYears();
        }
        public void Closing_Closing_button_Click()
        {
            int TrustCode = Closing_Trust_comboBox.SelectedIndex;
            TrustCode++;
            int year = Closing_Year_comboBox.SelectedIndex;
            string Year = Closing_Year_comboBox.SelectedValue.ToString();
            bl.OpeningEntries(TrustCode, Year);
            Message.ShowSuccess("Generated Opening Entries");
        }
        public void Closing_Generate_button_Copy_Click()
        {
            int TrustCode = Closing_Trust_comboBox.SelectedIndex;
            TrustCode++;
            string Year = Closing_Year_comboBox.SelectedValue.ToString();
            //Closing_Grid.ItemsSource = bl.ClosingEntries(TrustCode, Year);
            bl.ClosingEntries(TrustCode, Year);
            Message.ShowSuccess("Generated Closing Entries");
        }
    }
}
