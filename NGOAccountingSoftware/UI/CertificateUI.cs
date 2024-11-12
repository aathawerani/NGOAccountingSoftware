using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TrustApplication
{
    class CertificateUI
    {
        //CertificateDAL dal = new CertificateDAL();
        CertificateBL bl = new CertificateBL(); Utils util = new Utils();

        DatePicker Investment_Date; ComboBox Investment_Trust_comboBox; ComboBox Investment_CertPurchaseMaturity_comboBox;
        ComboBox Investment_CertType_comboBox; DataGrid Investment_Grid; TextBox Investment_Amount_textBox;
        TextBox Investment_AmountCertProfit_textBox; TextBox Investment_WHTCertProfit_textBox;
        TextBox Investment_CertNo_textBox; TextBox Investment_Folio_textBox; TextBox Investment_NetCertProfit_textBox;
        TextBox Investment_ID_textBox; CheckBox Matured_checkBox; DatePicker Investment_Purchase_Date;

        public void SetControls(DatePicker _Investment_Date, ComboBox _Investment_Trust_comboBox, 
            ComboBox _Investment_CertPurchaseMaturity_comboBox, ComboBox _Investment_CertType_comboBox, 
            DataGrid _Investment_Grid, TextBox _Investment_Amount_textBox, TextBox _Investment_AmountCertProfit_textBox, 
            TextBox _Investment_WHTCertProfit_textBox, TextBox _Investment_CertNo_textBox, TextBox _Investment_Folio_textBox, 
            TextBox _Investment_NetCertProfit_textBox, TextBox _Investment_ID_textBox, CheckBox _Matured_checkBox,
            DatePicker _Investment_Purchase_Date)
        {
            Investment_Date = _Investment_Date; Investment_Trust_comboBox = _Investment_Trust_comboBox;
            Investment_CertPurchaseMaturity_comboBox = _Investment_CertPurchaseMaturity_comboBox;
            Investment_CertType_comboBox = _Investment_CertType_comboBox; Investment_Grid = _Investment_Grid;
            Investment_Amount_textBox = _Investment_Amount_textBox;
            Investment_AmountCertProfit_textBox = _Investment_AmountCertProfit_textBox;
            Investment_WHTCertProfit_textBox = _Investment_WHTCertProfit_textBox;
            Investment_CertNo_textBox = _Investment_CertNo_textBox; Investment_Folio_textBox = _Investment_Folio_textBox;
            Investment_NetCertProfit_textBox = _Investment_NetCertProfit_textBox;
            Investment_ID_textBox = _Investment_ID_textBox; Matured_checkBox = _Matured_checkBox;
            Investment_Purchase_Date = _Investment_Purchase_Date;
        }

        public void InvestTab()
        {
            Investment_Date.Text = DateTime.Now.Date.ToString();
            Investment_Trust_comboBox.ItemsSource = bl.GetTrustNames();
            Investment_CertPurchaseMaturity_comboBox.ItemsSource = bl.GetMaturityYears();
        }

        public void Investment_Trust_comboBox_SelectionChanged()
        {
            if (Investment_Trust_comboBox.SelectedIndex < 0) return;
            Investment_CertType_comboBox.ItemsSource = bl.GetCertificateTypes((Investment_Trust_comboBox.SelectedIndex + 1).ToString());
        }

        public void InitializeInvestments()
        {
            Investment_Amount_textBox.Text = "";
            Investment_AmountCertProfit_textBox.Text = "";
            Investment_WHTCertProfit_textBox.Text = "";
            Investment_CertNo_textBox.Text = "";
            Investment_Folio_textBox.Text = "";
            Investment_Date.Text = DateTime.Now.Date.ToString();
        }

        public void LoadInvestments()
        {
            if (Investment_Trust_comboBox.SelectedIndex < 0) return;
            if (Investment_CertType_comboBox.SelectedIndex < 0) return;
            Investment_Grid.ItemsSource = bl.GetCertificates(Investment_Trust_comboBox.SelectedIndex + 1,
                Investment_CertType_comboBox.SelectedValue.ToString());
            InitializeInvestments();
        }

        public void LoadAllInvestments()
        {
            if(Matured_checkBox.IsChecked == false)
            {
                LoadInvestments();
                return;
            }
            if (Investment_Trust_comboBox.SelectedIndex < 0) return;
            if (Investment_CertType_comboBox.SelectedIndex < 0) return;
            Investment_Grid.ItemsSource = bl.GetAllCertificates(Investment_Trust_comboBox.SelectedIndex + 1,
                Investment_CertType_comboBox.SelectedValue.ToString());
            InitializeInvestments();
        }

        public void Investment_CertSoldSave_button_Click()
        {
            string certNo = "", folioNo = "";
            if (Investment_CertNo_textBox.Text != "") certNo = Investment_CertNo_textBox.Text;
            if (Investment_Folio_textBox.Text != "") folioNo = Investment_Folio_textBox.Text;
            if (bl.ValidateAmount(Investment_AmountCertProfit_textBox.Text))
            {
                bl.CertificateProfit(Investment_Trust_comboBox.SelectedIndex + 1, Investment_Date.SelectedDate.ToString(), certNo, folioNo,
                    Investment_AmountCertProfit_textBox.Text, Investment_CertType_comboBox.SelectedValue.ToString(),
                    Investment_WHTCertProfit_textBox.Text);
            }
            bl.CertificateSold(Investment_Trust_comboBox.SelectedIndex + 1, Investment_Date.SelectedDate.ToString(), certNo, folioNo,
                Investment_Amount_textBox.Text, Investment_CertType_comboBox.SelectedValue.ToString());
            Message.ShowSuccess("Accounting entry added successfully");
            LoadInvestments();
        }

        public void Investment_CertProfitSave_button_Click()
        {
            string certNo = "", folioNo = "";
            if (Investment_CertNo_textBox.Text != null) certNo = Investment_CertNo_textBox.Text;
            if (Investment_Folio_textBox.Text != null) folioNo = Investment_Folio_textBox.Text;
            if (!bl.ValidateAmount(Investment_AmountCertProfit_textBox.Text))
            {
                Message.ShowWarning("Please enter valid amount");
                return;
            }
            bl.CertificateProfit(Investment_Trust_comboBox.SelectedIndex + 1, Investment_Date.SelectedDate.ToString(), certNo, folioNo,
                Investment_AmountCertProfit_textBox.Text, Investment_CertType_comboBox.SelectedValue.ToString(),
                Investment_WHTCertProfit_textBox.Text);
            Investment_AmountCertProfit_textBox.Text = "";
            Investment_WHTCertProfit_textBox.Text = "";
            Message.ShowSuccess("Accounting entry added successfully");
        }

        public void Investment_CertPurchasedSave_button_Click()
        {
                if (!bl.ValidateAmount(Investment_Amount_textBox.Text))
                {
                    Message.ShowWarning("Please enter valid amount");
                    return;
                }
                bl.CertificatePurchased(Investment_Trust_comboBox.SelectedIndex + 1, Investment_Date.SelectedDate.ToString(),
                    Investment_CertNo_textBox.Text, Investment_Folio_textBox.Text,
                    Investment_Amount_textBox.Text, Investment_CertType_comboBox.SelectedValue.ToString(),
                    Investment_CertPurchaseMaturity_comboBox.SelectedValue.ToString(), Investment_Purchase_Date.SelectedDate.ToString());
                Message.ShowSuccess("Accounting entry added successfully");
                LoadInvestments();
        }

        public void Investment_AmountCertProfit_textBox_TextChanged()
        {
            Investment_NetCertProfit_textBox.Text = bl.GetNetProfit(Investment_AmountCertProfit_textBox.Text, Investment_WHTCertProfit_textBox.Text);
        }

        public void Investment_Grid_SelectionChanged()
        {
            int SelectedIndex = Investment_Grid.SelectedIndex;
            if (SelectedIndex > -1 && Investment_Grid.Items.Count > 0 && Investment_Grid.Items[SelectedIndex] != null
                && (Investment_Grid.Items[SelectedIndex] as Certificate) != null)
            {
                Investment_Purchase_Date.Text = util.FormatDateUS((Investment_Grid.Items[SelectedIndex] as Certificate).Date);
                string[] Maturity = util.Splitdate((Investment_Grid.Items[SelectedIndex] as Certificate).Maturity);
                Investment_CertPurchaseMaturity_comboBox.SelectedValue = Maturity[2];
            }
        }

        public void Investment_CertUpdate_button_Click()
        {
            if (!bl.ValidateAmount(Investment_Amount_textBox.Text))
            {
                Message.ShowWarning("Please enter valid amount");
                return;
            }
            bl.CertificateUpdate(Investment_Purchase_Date.SelectedDate.ToString(),
                Investment_CertNo_textBox.Text, Investment_Folio_textBox.Text,
                Investment_Amount_textBox.Text,
                Investment_CertPurchaseMaturity_comboBox.SelectedValue.ToString(),
                Investment_ID_textBox.Text);
            Message.ShowSuccess("Accounting entry added successfully");
            LoadInvestments();
        }
    }
}
