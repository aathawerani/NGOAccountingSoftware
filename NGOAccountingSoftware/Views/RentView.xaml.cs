using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace TrustApplication.Views
{
    public partial class RentView : UserControl
    {
        Utils util = new Utils();
        Logger logger;
        CultureInfo ci = CultureInfo.CreateSpecificCulture("en-GB");
        RentUI rentUI = new RentUI();
        public RentView()
        {
            try
            {
                ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
                Thread.CurrentThread.CurrentCulture = ci;
                InitializeComponent();
                Logger.SetLogger(new FileManager("log", "TrustLog" + DateTime.Now.ToString("yyyyMMdd") + ".log"));
                logger = Logger.GetInstance();
                RentTab();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void HandleException(Exception ex)
        {
            Message.ShowError("Error occurred please check log file: " + ex.Message);
            logger.WriteLog(DateTime.Now.ToString() + ": Exception Message : " + ex.Message);
            if (ex.InnerException != null)
            {
                logger.WriteLog("Inner Exception : " + ex.InnerException.ToString());
            }
            logger.WriteLog("Stack Trace : " + ex.StackTrace);
        }

        private void RentTab()
        {
            rentUI.SetRentControls(Rent_Trust_comboBox, Rent_Plot_comboBox, Rent_Space_comboBox, Rent_SpaceNo_comboBox, 
                Rent_SerialNo_textBox, Rent_Date, Rent_Name_textBox, Rent_FromM_comboBox, Rent_FromY_comboBox, 
                Rent_ToM_comboBox, Rent_ToY_comboBox, Rent_NumMonths_comboBox, Rent_MonthlyRent_textBox, Rent_MonthlyWater_textBox, 
                Rent_TotalRent_textBox, Rent_TotalWater_textBox, Rent_RentArears_textBox, Rent_WaterArears_textBox,
                Rent_RentParticulars_textBox, Rent_WaterParticulars_textBox, Rent_RentArearsParticulars_textBox,
                Rent_WaterArearsParticulars_textBox
                /*, Rent_Grid,
                Rent_TotalAmount_textBox, Rent_CNIC_textBox, Rent_ID_textBox, Rent_PaymentMode, Rent_BankReference_textBox, Tenant_Trust_comboBox, Tenant_Plot_comboBox,
                Tenant_Grid, Rent_TotalPaid_textBox*/);
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

    }
}
