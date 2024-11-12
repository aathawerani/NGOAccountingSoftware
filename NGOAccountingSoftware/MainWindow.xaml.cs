using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TrustApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //DAL dal = new DAL();
        //BL bl = new BL();
        Utils util = new Utils();
        Logger logger;
        CultureInfo ci = CultureInfo.CreateSpecificCulture("en-GB");
        RentUI rentUI = new RentUI();
        ReceivablesUI receivablesUI = new ReceivablesUI();
        TenantUI tenantUI = new TenantUI();
        AccountUI accountUI = new AccountUI();
        CertificateUI certificateUI = new CertificateUI();
        ReportUI reportUI = new ReportUI();
        ClosingUI closingUI = new ClosingUI();
        MiscUI miscUI = new MiscUI();
        SettingsUI settingsUI = new SettingsUI();
        CashUI cashUI = new CashUI();
        MajlisUI majlisUI = new MajlisUI();
        VoucherUI voucherUI = new VoucherUI();
        DonationUI donationUI = new DonationUI();

        public MainWindow()
        {
            try
            {
                ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
                Thread.CurrentThread.CurrentCulture = ci;
                InitializeComponent();
                Logger.SetLogger(new FileManager("log", "TrustLog" + DateTime.Now.ToString("yyyyMMdd") + ".log"));
                logger = Logger.GetInstance();
                RentTab();
                TenantTab();
                MajlisTab();
                VoucherTab();
                DonationTab();
                AccountsTab();
                CashTab();
                MiscTab();
                InvestTab();
                ReportTab();
                ClosingTab();
                SettingsTab();
            }
            catch(Exception ex)
            {
                HandleException(ex);
            }
        }

        private void HandleException(Exception ex)
        {
            Message.ShowError("Error occurred please check log file: " + ex.Message);
            logger.WriteLog(DateTime.Now.ToString() +  ": Exception Message : " + ex.Message);
            if (ex.InnerException != null)
            {
                logger.WriteLog("Inner Exception : " + ex.InnerException.ToString());
            }
            logger.WriteLog("Stack Trace : " + ex.StackTrace);
        }

        #region Receivables
        private void ReceivablesTab()
        {
            receivablesUI.SetRentControls(Receivable_Trust_comboBox, Receivable_Grid, Receivable_ID_textBox);
            receivablesUI.ReceivablesTab();
        }
        private void Receivable_Trust_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                receivablesUI.Receivable_Trust_comboBox_SelectionChanged();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        private void Receivables_Receivedbutton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                receivablesUI.Receivables_Receivedbutton_Click();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        #endregion

        #region Tenant
        private void TenantTab()
        {
            tenantUI.SetRentControls(Tenant_Trust_comboBox, Tenant_Plot_comboBox,
                Tenant_Grid);
            tenantUI.TenantTab();
        }
        private void Tenant_Trust_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                tenantUI.Tenant_Trust_comboBox_SelectionChanged();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        private void Tenant_Plot_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                tenantUI.Tenant_Plot_comboBox_SelectionChanged();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        #endregion

        #region Rent

        private void RentTab()
        {
            rentUI.SetRentControls(Rent_Trust_comboBox, Rent_FromM_comboBox, Rent_FromY_comboBox, Rent_ToM_comboBox,
                Rent_ToY_comboBox, Rent_NumMonths_comboBox, Rent_Date, Rent_Grid, Rent_Plot_comboBox, Rent_Space_comboBox, 
                Rent_SpaceNo_comboBox, Rent_Name_textBox, Rent_MonthlyRent_textBox, Rent_MonthlyWater_textBox, 
                Rent_RentArears_textBox, Rent_WaterArears_textBox, Rent_TotalRent_textBox, Rent_TotalWater_textBox, 
                Rent_TotalAmount_textBox, Rent_SerialNo_textBox, Rent_CNIC_textBox, Rent_ID_textBox, Rent_PaymentMode,
                Rent_RentParticulars_textBox, Rent_WaterParticulars_textBox, Rent_RentArearsParticulars_textBox, 
                Rent_WaterArearsParticulars_textBox, Rent_BankReference_textBox, Tenant_Trust_comboBox, Tenant_Plot_comboBox,
                Tenant_Grid, Rent_TotalPaid_textBox);
            rentUI.RentTab();
        }
        private void Rent_Clear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                rentUI.InitializeRent();
                rentUI.InitializeRent2();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void Rent_Trust_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                rentUI.Rent_Trust_comboBox_SelectionChanged();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        private void Rent_Plot_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                rentUI.Rent_Plot_comboBox_SelectionChanged();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void Rent_Space_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                rentUI.Rent_Space_comboBox_SelectionChanged();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void Rent_SpaceNo_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                rentUI.Rent_SpaceNo_comboBox_SelectionChanged();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        private void CalculateRent(object sender, TextChangedEventArgs e)
        {
            try
            {
                rentUI.CalculateRent();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        private void Rent_NumMonths_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                rentUI.Rent_NumMonths_comboBox_SelectionChanged();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        void CacluateNumMonths(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                rentUI.CalculateNumMonths();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        private void Rent_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                rentUI.Rent_button_Click();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        private void Rent_Grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                rentUI.Rent_Grid_SelectionChanged();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        private void Rent_Updatebutton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                rentUI.Rent_Updatebutton_Click();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void Rent_Printbutton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                rentUI.Rent_Printbutton_Click();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void Rent_TotalRent_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                rentUI.Rent_TotalRent_textBox_TextChanged();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void Rent_TotalWater_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                rentUI.Rent_TotalWater_textBox_TextChanged();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void Rent_RentArears_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                rentUI.CalculateRent();
                rentUI.Rent_RentArears_textBox_TextChanged();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void Rent_WaterArears_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                rentUI.CalculateRent();
                rentUI.Rent_WaterArears_textBox_TextChanged();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        private void Rent_RentParticulars_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                rentUI.Rent_RentParticulars_textBox_TextChanged();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        #endregion Rent

        #region Majlis

        private void CalculateBill(object sender, TextChangedEventArgs e)
        {
            try
            { 
                majlisUI.CalculateBill();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        private void MajlisTab()
        {
            try
            {
                majlisUI.SetControls(Majlis_Date, Majlis_IslamicDay_comboBox, Majlis_IslamicMonth_comboBox, 
                    Majlis_IslamicYear_textBox, Majlis_MilkTotal_textBox, Majlis_MilkUnit_textBox, 
                    Majlis_MilkUnitPrice_textBox, Majlis_SugarUnit_textBox, Majlis_SugarTotal_textBox, 
                    Majlis_SugarUnitPrice_textBox, Majlis_TeaTotal_textBox, Majlis_TotalBill_textBox, 
                    Majlis_TeaUnit_textBox, Majlis_TeaUnitPrice_textBox, Majlis_Saffron_textBox, 
                    Majlis_Cardamoms_textBox, Majlis_Pistachios_textBox, Majlis_Ice_textBox, Majlis_EssenceColor_textBox,
                    Majlis_Miscellaneous_textBox, Majlis_LightFan_textBox, Majlis_Gas_textBox, 
                    Majlis_LoudSpeaker_textBox, Majlis_Molana_textBox, Majlis_TimeFrom_comboBox, Majlis_TimeTo_comboBox, 
                    Majlis_TimeAM_comboBox, Majlis_SerialNo_textBox, Majlis_Name_textBox, Majlis_MiscDesc_textBox, 
                    Majlis_Particulars_textBox);
                majlisUI.MajlisTab();
            }
            catch(Exception ex)
            {
                HandleException(ex);
            }
        }

        private void Majlis_GenerateBill_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                majlisUI.Majlis_GenerateBill_button_Click();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void Majlis_PrintBill_button_Click(object sender, RoutedEventArgs e)
        {
            majlisUI.Majlis_PrintBill_button_Click();
        }

        private void Majlis_Date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            { 
                majlisUI.UpdateDate();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void Majlis_Name_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                majlisUI.Majlis_Name_textBox_TextChanged();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        #endregion Majlis

        #region Voucher & Donation

        private void VoucherTab()
        {
            try
            {
                voucherUI.SetControls(Expense_Date, Expense_Voucher_comboBox, Expense_Being_textBox,
                    Expense_Amount_textBox, Expense_VoucherNumber_textBox);
                voucherUI.VoucherTab();
            }
            catch(Exception ex)
            {
                HandleException(ex);
            }
        }

        private void Expense_GenerateVoucher_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                voucherUI.Expense_GenerateVoucher_button_Click();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void Expense_Voucher_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                voucherUI.Expense_Voucher_comboBox_SelectionChanged();
            }
            catch(Exception ex)
            {
                HandleException(ex);
            }
        }

        private void DonationTab()
        {
            try
            {
                donationUI.SetControls(Donation_Date, Donation_Type_comboBox, Donation_Amount_textBox,
                    Donation_SerialNo_textBox, Donation_Name_textBox, Donation_OnAccountOf_textBox,
                    Donation_PaymentMode_comboBox);
                donationUI.DonationTab();
            }
            catch(Exception ex)
            {
                HandleException(ex);
            }
        }
        private void Donation_GenerateReceipt_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                donationUI.Donation_GenerateReceipt_button_Click();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void Donation_Name_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                donationUI.Donation_Name_textBox_TextChanged();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void Donation_Type_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                donationUI.Donation_Type_comboBox_SelectionChanged();
            }
            catch(Exception ex)
            {
                HandleException(ex);
            }
        }

        #endregion Voucher & Expense

        #region Accounts

        private void AccountsTab()
        {
            try
            {
                accountUI.SetAccountControls(Accounts_Trust_comboBox, Accounts_Account_comboBox, 
                    Accounts_RefAccount_comboBox, Accounts_Grid, Accounts_Date, Accounts_SerialNo_textBox, 
                    Accounts_Name_textBox, Accounts_Particulars_textBox, Accounts_Debit_textBox, Accounts_Credit_textBox, 
                    Accounts_ID_textBox);
                accountUI.AccountsTab();
            }
            catch(Exception ex)
            {
                HandleException(ex);
            }
        }
        private void Accounts_Trust_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                accountUI.Accounts_Trust_comboBox_SelectionChanged();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        private void Accounts_Account_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                accountUI.Accounts_Account_comboBox_SelectionChanged();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        private void Account_Update_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                accountUI.Account_Update_button_Click();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        private void Account_Add_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                accountUI.Account_Add_button_Click();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        private void Account_Delete_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                accountUI.Account_Delete_button_Click();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        private void Accounts_Grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                accountUI.Accounts_Grid_SelectionChanged();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        #endregion Accounts

        #region Misc

        void MiscTab()
        {
            miscUI.setMiscControls(Misc_Date, Misc_Trust_comboBox, Debit_Account_comboBox, Credit_Account_comboBox, 
                Transfer_Amount_textBox_Copy, Expense_Particulars_textBox);
            miscUI.MiscTab();
        }
        private void Misc_Trust_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                miscUI.Misc_Trust_comboBox_SelectionChanged();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        private void Destination_Account_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Transfer_Save_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                miscUI.Transfer_Save_button_Click();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        #endregion Misc

        #region Cash

        private void CashTab()
        {
            cashUI.SetControls(Cash_Trust_comboBox, Cash_Grid);
            cashUI.CashTab();
        }

        private void Cash_Trust_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                cashUI.Cash_Trust_comboBox_SelectionChanged();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void Cash_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                cashUI.Cash_GotFocus();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        #endregion Cash

        #region Investment

        private void InvestTab()
        {
            certificateUI.SetControls(Investment_Date, Investment_Trust_comboBox,
                Investment_CertPurchaseMaturity_comboBox, Investment_CertType_comboBox, Investment_Grid, 
                Investment_Amount_textBox, Investment_AmountCertProfit_textBox, Investment_WHTCertProfit_textBox, 
                Investment_CertNo_textBox, Investment_Folio_textBox, Investment_NetCertProfit_textBox, 
                Investment_ID_textBox, Matured_checkBox, Investment_Purchase_Date);
            certificateUI.InvestTab();
        }

        private void Investment_Trust_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                certificateUI.Investment_Trust_comboBox_SelectionChanged();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void Investment_CertType_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                certificateUI.LoadInvestments();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        private void Investment_CertSoldSave_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                certificateUI.Investment_CertSoldSave_button_Click();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void Investment_CertProfitSave_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                certificateUI.Investment_CertProfitSave_button_Click();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        private void Investment_CertPurchasedSave_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                certificateUI.Investment_CertPurchasedSave_button_Click();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        private void Investment_AmountCertProfit_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            certificateUI.Investment_AmountCertProfit_textBox_TextChanged();
        }
        private void Investment_Grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                certificateUI.Investment_Grid_SelectionChanged();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        private void Investment_CertUpdate_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                certificateUI.Investment_CertUpdate_button_Click();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        private void Matured_checkBox_Checked(object sender, RoutedEventArgs e)
        {
            certificateUI.LoadAllInvestments();
        }
        private void Matured_checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            certificateUI.LoadAllInvestments();
        }

        #endregion Investment

        #region Report

        private void ReportTab()
        {
            reportUI.SetControls(Reports_Trust_comboBox, Reports_Year_comboBox, Report_Grid, Reports_Statement_comboBox);
            reportUI.ReportTab();
        }

        private void Report_Accounts_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                reportUI.Report_Accounts_button_Click();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        /*private void Report_Closing_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                reportUI.Report_Closing_button_Click();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }*/

        private void Report_Statements_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                reportUI.Report_Statements_button_Click();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

        }

        private void Reports_Statement_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                reportUI.Reports_Statement_comboBox_SelectionChanged();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

        }

        #endregion Report

        #region Settings

        private void SettingsTab()
        {
            settingsUI.SetControls(Settings_Year_comboBox, Settings_Trust_comboBox, Settings_AccountCode_textBox,
                Settings_AccountType_comboBox, Settings_AccountName_textBox, Import_ImportFilePath, Import_Trust_comboBox,
                Settings_ImportYear_comboBox);
            settingsUI.SettingsTab();
        }

        private void Settings_Year_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            settingsUI.Settings_Year_comboBox_SelectionChanged();
        }

        private void Settings_AddAccount_button_Click(object sender, RoutedEventArgs e)
        {
            settingsUI.Settings_AddAccount_button_Click();
        }

        private void Import_BrowseDirectory_Click(object sender, RoutedEventArgs e)
        {
            settingsUI.Import_BrowseDirectory_Click();
        }

        private void Import_ImportAccounts_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                settingsUI.Import_ImportAccounts_Click();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        #endregion Settings

        #region Closing
        private void ClosingTab()
        {
            closingUI.SetControls(Closing_Trust_comboBox, Closing_Year_comboBox, Closing_Grid);
            closingUI.ClosingTab();
        }

        private void Closing_Closing_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                closingUI.Closing_Closing_button_Click();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void Closing_Generate_button_Copy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                closingUI.Closing_Generate_button_Copy_Click();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

        }

        #endregion Closing

    }
}
