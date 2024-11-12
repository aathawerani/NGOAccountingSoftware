using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TrustApplication
{
    class AccountUI
    {
        //AccountsDAL dal = new AccountsDAL();
        AccountsBL bl = new AccountsBL(); Utils util = new Utils();
        ComboBox Accounts_Trust_comboBox; ComboBox Accounts_Account_comboBox; ComboBox Accounts_RefAccount_comboBox;
        DataGrid Accounts_Grid; DatePicker Accounts_Date; TextBox Accounts_SerialNo_textBox; TextBox Accounts_Name_textBox;
        TextBox Accounts_Particulars_textBox; TextBox Accounts_Debit_textBox; TextBox Accounts_Credit_textBox;
        TextBox Accounts_ID_textBox;

        public void SetAccountControls(ComboBox _Accounts_Trust_comboBox, ComboBox _Accounts_Account_comboBox,
        ComboBox _Accounts_RefAccount_comboBox, DataGrid _Accounts_Grid, DatePicker _Accounts_Date, 
        TextBox _Accounts_SerialNo_textBox, TextBox _Accounts_Name_textBox, TextBox _Accounts_Particulars_textBox, 
        TextBox _Accounts_Debit_textBox, TextBox _Accounts_Credit_textBox, TextBox _Accounts_ID_textBox)
        {
            Accounts_Trust_comboBox = _Accounts_Trust_comboBox; Accounts_Account_comboBox = _Accounts_Account_comboBox;
            Accounts_RefAccount_comboBox = _Accounts_RefAccount_comboBox; Accounts_Grid = _Accounts_Grid;
            Accounts_Date = _Accounts_Date; Accounts_SerialNo_textBox = _Accounts_SerialNo_textBox;
            Accounts_Name_textBox = _Accounts_Name_textBox; Accounts_Particulars_textBox = _Accounts_Particulars_textBox;
            Accounts_Debit_textBox = _Accounts_Debit_textBox; Accounts_Credit_textBox = _Accounts_Credit_textBox;
            Accounts_ID_textBox = _Accounts_ID_textBox; 
        }

        public void AccountsTab()
        {
            List<string> trustnames = bl.GetTrustNames();
            if (trustnames.Count == 0)
            {
                Message.ShowError("Could not load Trust names");
                return;
            }
            Accounts_Trust_comboBox.ItemsSource = trustnames;
        }

        public void Accounts_Trust_comboBox_SelectionChanged()
        {
            if (Accounts_Trust_comboBox.SelectedIndex < 0) return;
            
            int TrustCode = Accounts_Trust_comboBox.SelectedIndex + 1;
            List<string> trustnames = bl.GetTrustAccountNames(TrustCode);
            if(trustnames.Count == 0)
            {
                Message.ShowError("Could not load Trust account names");
                return;
            }
            Accounts_Account_comboBox.ItemsSource = trustnames;
            List<string> trustacccounts = bl.GetTrustAccountNames(TrustCode);
            if (trustacccounts.Count == 0)
            {
                Message.ShowError("Could not load Trust accounts");
                return;
            }
            Accounts_RefAccount_comboBox.ItemsSource = trustacccounts;
        }

        public void ResetAccounts()
        {
            int TrustCode = Accounts_Trust_comboBox.SelectedIndex + 1;
            string AccountCode = Accounts_Account_comboBox.SelectedItem.ToString();
            List<Accounts> accountslist = bl.GetAccountsList(TrustCode, AccountCode);
            if (accountslist.Count == 0)
            {
                Message.ShowError("Could not load Trust accounts list");
                return;
            }
            Accounts_Grid.ItemsSource = accountslist;
        }

        private string GetRefAccountTypecode()
        {
            int TrustCode = Accounts_Trust_comboBox.SelectedIndex + 1;
            string AccountTypeCode = "";
            if (Accounts_RefAccount_comboBox.SelectedItem != null)
            {
                string AccountCode = Accounts_RefAccount_comboBox.SelectedItem.ToString();
                AccountTypeCode = bl.GetAccountTypeCodesByAccountCode(TrustCode, AccountCode);
                if (AccountTypeCode == "")
                {
                    Message.ShowError("Could not get accounttypecode");
                }
            }
            return AccountTypeCode;
        }

        private string GetAccountTypecode()
        {
            int TrustCode = Accounts_Trust_comboBox.SelectedIndex + 1;
            string AccountCode = Accounts_Account_comboBox.SelectedItem.ToString();
            string AccountTypeCode = bl.GetAccountTypeCodesByAccountCode(TrustCode, AccountCode);
            if (AccountTypeCode == "")
            {
                Message.ShowError("Could not get accounttypecode");
            }
            return AccountTypeCode;
        }

        private string GetAccountCode()
        {
            //int TrustCode = Accounts_Trust_comboBox.SelectedIndex + 1;
            string AccountCode = Accounts_Account_comboBox.SelectedItem.ToString();
            return AccountCode;
        }

        public void Accounts_Account_comboBox_SelectionChanged()
        {
            if (Accounts_Trust_comboBox.SelectedIndex < 0 || Accounts_Account_comboBox.SelectedIndex < 0) return;
            ResetAccounts();
        }

        public void Account_Update_button_Click()
        {
            bl.AccountsUpdate(Accounts_Date.Text, Accounts_SerialNo_textBox.Text, Accounts_Name_textBox.Text,
                GetRefAccountTypecode(), Accounts_Particulars_textBox.Text, Accounts_Debit_textBox.Text,
                Accounts_Credit_textBox.Text, Accounts_ID_textBox.Text);
            Message.ShowSuccess("Accounts updated successfully");
            ResetAccounts();
        }

        public void Account_Add_button_Click()
        {
            bl.AccountsInsert(Accounts_Trust_comboBox.SelectedIndex + 1, GetAccountTypecode()
                , Accounts_Date.SelectedDate.ToString(), Accounts_SerialNo_textBox.Text, Accounts_Name_textBox.Text,
                GetRefAccountTypecode(), Accounts_Particulars_textBox.Text, Accounts_Debit_textBox.Text, Accounts_Credit_textBox.Text, GetAccountCode());
            Message.ShowSuccess("Accounting entry added successfully");
            ResetAccounts();
        }

        public void Account_Delete_button_Click()
        {
            bl.AccountsDelete(Accounts_ID_textBox.Text);
            Message.ShowSuccess("Accounting entry deleted successfully");
            ResetAccounts();
        }

        public void Accounts_Grid_SelectionChanged()
        {
            int SelectedIndex = Accounts_Grid.SelectedIndex;
            if (SelectedIndex > -1 && Accounts_Grid.Items.Count > 0 && Accounts_Grid.Items[SelectedIndex] != null
                && (Accounts_Grid.Items[SelectedIndex] as Accounts) != null)
            {
                Accounts_Date.Text = util.FormatDateUS((Accounts_Grid.Items[SelectedIndex] as Accounts).Date);
            }
        }
    }
}
