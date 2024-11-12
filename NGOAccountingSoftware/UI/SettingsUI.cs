using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TrustApplication.Exceptions;

namespace TrustApplication
{
    class SettingsUI
    {
        //AccountsDAL dal = new AccountsDAL();
        ComboBox Settings_Year_comboBox; ComboBox Settings_Trust_comboBox; TextBox Settings_AccountCode_textBox;
        ComboBox Settings_AccountType_comboBox; TextBox Settings_AccountName_textBox; TextBox Import_ImportFilePath;
        ComboBox Import_Trust_comboBox; ComboBox Settings_ImportYear_comboBox;

        AccountsBL bl = new AccountsBL(); Utils util = new Utils(); ImportBL ibl = new ImportBL();
        //SettingBL sbl = new SettingBL();

        public void SetControls(ComboBox _Settings_Year_comboBox, ComboBox _Settings_Trust_comboBox, TextBox _Settings_AccountCode_textBox,
                ComboBox _Settings_AccountType_comboBox, TextBox _Settings_AccountName_textBox, TextBox _Import_ImportFilePath,
                ComboBox _Import_Trust_comboBox, ComboBox _Settings_ImportYear_comboBox)
        {
            Settings_Year_comboBox = _Settings_Year_comboBox;
            Settings_Trust_comboBox = _Settings_Trust_comboBox;
            Settings_AccountCode_textBox = _Settings_AccountCode_textBox;
            Settings_AccountType_comboBox = _Settings_AccountType_comboBox;
            Settings_AccountName_textBox = _Settings_AccountName_textBox;
            Import_ImportFilePath = _Import_ImportFilePath;
            Import_Trust_comboBox = _Import_Trust_comboBox;
            Settings_ImportYear_comboBox = _Settings_ImportYear_comboBox;
        }

        public void SettingsTab()
        {
            Settings_Year_comboBox.ItemsSource = bl.GetAccountingYears();
            Settings_Trust_comboBox.ItemsSource = bl.GetTrustNames();
            Settings_AccountType_comboBox.ItemsSource = bl.GetAccountTypes();
            Import_Trust_comboBox.ItemsSource = bl.GetTrustNames();
            Settings_ImportYear_comboBox.ItemsSource = bl.GetAccountingYears();
        }
        public void Settings_Year_comboBox_SelectionChanged()
        {
            bl.SetYear(Settings_Year_comboBox.SelectedValue.ToString());
        }

        public void Settings_AddAccount_button_Click()
        {
            bl.InsertAccount(Settings_Trust_comboBox.SelectedIndex + 1, Settings_AccountType_comboBox.SelectedValue,
                Settings_AccountCode_textBox.Text, Settings_AccountName_textBox.Text);
        }

        public void Import_BrowseDirectory_Click()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                //Import_ImportFilePath.Text = File.ReadAllText(openFileDialog.FileName);
                Import_ImportFilePath.Text = openFileDialog.FileName;
            }
            else
            {
                throw new SettingsUIException("Failed to select file");
            }

        }

        public void Import_ImportAccounts_Click()
        {
            if (Import_Trust_comboBox.SelectedIndex < 0)
                throw new SettingsUIException("Please select Trust");
            if(Import_ImportFilePath.Text != "")
            {
                ibl.ImportAccounts(Import_Trust_comboBox.SelectedIndex + 1, Import_ImportFilePath.Text,
                    Settings_Year_comboBox.SelectedValue.ToString());
                Message.ShowSuccess("Accounts imported successfully");
            }
            else
            {
                Message.ShowWarning("Please select file to import");
            }
        }
    }
}
