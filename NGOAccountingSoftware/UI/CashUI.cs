using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TrustApplication
{
    class CashUI
    {
        ComboBox Cash_Trust_comboBox; DataGrid Cash_Grid;
        //DAL dal = new DAL();
        AccountsBL bl = new AccountsBL();

        public void SetControls(ComboBox _Cash_Trust_comboBox, DataGrid _Cash_Grid)
        {

            Cash_Trust_comboBox = _Cash_Trust_comboBox; Cash_Grid = _Cash_Grid;
        }

        public void CashTab()
        {
            List<string> trustnames = bl.GetTrustNames();
            if (trustnames.Count == 0)
            {
                Message.ShowError("Could not load Trust names");
                return;
            }
            Cash_Trust_comboBox.ItemsSource = trustnames;
        }

        public void Cash_Trust_comboBox_SelectionChanged()
        {
            if (Cash_Trust_comboBox.SelectedIndex < 0) return;
            int TrustCode = Cash_Trust_comboBox.SelectedIndex + 1;
            List<Accounts> cashaccounts = bl.GetCashAccount(TrustCode);
            if (cashaccounts.Count == 0)
            {
                Message.ShowError("Could not load Trust accounts list");
                return;
            }
            Cash_Grid.ItemsSource = cashaccounts;
        }

        public void Cash_GotFocus()
        {
            if (Cash_Trust_comboBox.SelectedIndex < 0) return;
            int TrustCode = Cash_Trust_comboBox.SelectedIndex + 1;
            List<Accounts> cashaccounts = bl.GetCashAccount(TrustCode);
            if (cashaccounts.Count == 0)
            {
                Message.ShowError("Could not load Trust accounts list");
                return;
            }
            Cash_Grid.ItemsSource = cashaccounts;
        }
    }
}
