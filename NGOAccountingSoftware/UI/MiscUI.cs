using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TrustApplication.Exceptions;

namespace TrustApplication
{
    class MiscUI
    {
        //AccountsDAL dal = new AccountsDAL();
        AccountsBL abl = new AccountsBL(); Utils util = new Utils();
        //MiscBL mbl = new MiscBL();
        DatePicker Misc_Date; ComboBox Misc_Trust_comboBox; ComboBox Debit_Account_comboBox;
        ComboBox Credit_Account_comboBox; TextBox Transfer_Amount_textBox_Copy; TextBox Expense_Particulars_textBox;

        public void setMiscControls(DatePicker _Misc_Date, ComboBox _Misc_Trust_comboBox, ComboBox _Debit_Account_comboBox,
        ComboBox _Credit_Account_comboBox, TextBox _Transfer_Amount_textBox_Copy, TextBox _Expense_Particulars_textBox)
        {
            Misc_Date = _Misc_Date; Misc_Trust_comboBox = _Misc_Trust_comboBox;
            Debit_Account_comboBox = _Debit_Account_comboBox; Credit_Account_comboBox = _Credit_Account_comboBox;
            Transfer_Amount_textBox_Copy = _Transfer_Amount_textBox_Copy;
            Expense_Particulars_textBox = _Expense_Particulars_textBox;
        }

        public void MiscTab()
        {
            Misc_Date.Text = DateTime.Now.Date.ToString();
            Misc_Trust_comboBox.ItemsSource = abl.GetTrustNames();
        }

        public void Misc_Trust_comboBox_SelectionChanged()
        {
            if (Misc_Trust_comboBox.SelectedIndex < 0)
            {
                throw new MiscUIException("Please select Trust");
            }
            int TrustCode = Misc_Trust_comboBox.SelectedIndex + 1;
            Debit_Account_comboBox.ItemsSource = abl.GetAccountsByTrust(TrustCode);
            Credit_Account_comboBox.ItemsSource = abl.GetAccountsByTrust(TrustCode);
        }

        public void Transfer_Save_button_Click()
        {
            if (Misc_Trust_comboBox.SelectedIndex < 0)
            {
                throw new MiscUIException("Please select Trust");
            }
            if (!abl.ValidateAmount(Transfer_Amount_textBox_Copy.Text))
            {
                throw new MiscUIException("Please enter valid amount");
            }
            if (Debit_Account_comboBox.SelectedIndex < 0 || Credit_Account_comboBox.SelectedIndex < 0) return;
            abl.TransferAmount(Misc_Trust_comboBox.SelectedIndex + 1, Misc_Date.SelectedDate.ToString(),
                Transfer_Amount_textBox_Copy.Text, Debit_Account_comboBox.SelectedItem,
                Credit_Account_comboBox.SelectedItem, Expense_Particulars_textBox.Text);
        }
    }
}
