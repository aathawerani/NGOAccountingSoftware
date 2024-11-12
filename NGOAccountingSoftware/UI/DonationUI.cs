using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TrustApplication
{
    class DonationUI
    {
        AccountsBL abl = new AccountsBL(); DonationBL dbl = new DonationBL();
        DatePicker Donation_Date; ComboBox Donation_Type_comboBox; TextBox Donation_Amount_textBox;
        TextBox Donation_SerialNo_textBox; TextBox Donation_Name_textBox; TextBox Donation_OnAccountOf_textBox;
        ComboBox Donation_PaymentMode_comboBox;

        public void SetControls(DatePicker _Donation_Date, ComboBox _Donation_Type_comboBox, 
            TextBox _Donation_Amount_textBox, TextBox _Donation_SerialNo_textBox, TextBox _Donation_Name_textBox, 
            TextBox _Donation_OnAccountOf_textBox, ComboBox _Donation_PaymentMode_comboBox)
        {
            Donation_Date = _Donation_Date; Donation_Type_comboBox = _Donation_Type_comboBox;
            Donation_Amount_textBox = _Donation_Amount_textBox; Donation_SerialNo_textBox = _Donation_SerialNo_textBox;
            Donation_Name_textBox = _Donation_Name_textBox; Donation_OnAccountOf_textBox = _Donation_OnAccountOf_textBox;
            Donation_PaymentMode_comboBox = _Donation_PaymentMode_comboBox;
        }
        public void DonationTab()
        {
            Donation_Date.Text = DateTime.Now.Date.ToString();
            Donation_Type_comboBox.ItemsSource = dbl.GetDonationAccounts();
        }

        public void Donation_GenerateReceipt_button_Click()
        {
            if (!abl.ValidateAmount(Donation_Amount_textBox.Text))
            {
                Message.ShowWarning("Please enter valid amount");
                return;
            }
            dbl.InsertDonation(Donation_Date.Text, Donation_SerialNo_textBox.Text, Donation_Name_textBox.Text,
                Donation_OnAccountOf_textBox.Text, Donation_Type_comboBox.SelectedItem,
                Donation_PaymentMode_comboBox.SelectedItem, Donation_Amount_textBox.Text);
            Message.ShowSuccess("Account entry created successfully");
        }

        public void Donation_Name_textBox_TextChanged()
        {
            Donation_OnAccountOf_textBox.Text = "RECEIVED FROM " + Donation_Name_textBox.Text;
        }

        public void Donation_Type_comboBox_SelectionChanged()
        {
            Donation_OnAccountOf_textBox.Text = dbl.GetDonationParticulars(Donation_Type_comboBox.SelectedItem);
            Donation_PaymentMode_comboBox.ItemsSource = abl.GetContraAccountCodes(Donation_Type_comboBox.SelectedItem);
        }
    }
}
