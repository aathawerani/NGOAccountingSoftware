using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TrustApplication
{
    class VoucherUI
    {
        AccountsBL abl = new AccountsBL(); VoucherBL bl = new VoucherBL();
        DatePicker Expense_Date; ComboBox Expense_Voucher_comboBox; TextBox Expense_Being_textBox;
        TextBox Expense_Amount_textBox; TextBox Expense_VoucherNumber_textBox;

        public void SetControls(DatePicker _Expense_Date, ComboBox _Expense_Voucher_comboBox, 
            TextBox _Expense_Being_textBox, TextBox _Expense_Amount_textBox, TextBox _Expense_VoucherNumber_textBox)
        {
            Expense_Date = _Expense_Date; Expense_Voucher_comboBox = _Expense_Voucher_comboBox;
            Expense_Being_textBox = _Expense_Being_textBox; Expense_Amount_textBox = _Expense_Amount_textBox;
            Expense_VoucherNumber_textBox = _Expense_VoucherNumber_textBox;
        }

        public void VoucherTab()
        {
            Expense_Date.Text = DateTime.Now.Date.ToString();
            Expense_Voucher_comboBox.ItemsSource = bl.GetExpenseAccounts(3);
            Expense_Being_textBox.Text = "BEING COST OF MILK, PISTACHIO &ICE & HADIYA TO MOLANA";
        }

        public void Expense_GenerateVoucher_button_Click()
        {
            if (!bl.ValidateAmount(Expense_Amount_textBox.Text))
            {
                return;
            }
            bl.InsertVoucher(Expense_Date.Text, Expense_VoucherNumber_textBox.Text, Expense_Being_textBox.Text, Expense_Amount_textBox.Text,
                Expense_Voucher_comboBox.SelectedItem);
        }

        public void Expense_Voucher_comboBox_SelectionChanged()
        {
            Expense_Being_textBox.Text = bl.GetMiscParticulars(Expense_Voucher_comboBox.SelectedItem);
        }
    }
}
