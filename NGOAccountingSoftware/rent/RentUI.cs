using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TrustApplication.Exceptions;

namespace TrustApplication
{
    class RentUI
    {
        bool UpdateOnce = true;
        //RentDAL dal = new RentDAL();
        RentBL bl = new RentBL(); Utils util = new Utils();
        ComboBox Rent_Trust_comboBox; ComboBox Rent_FromM_comboBox; ComboBox Rent_FromY_comboBox;
        ComboBox Rent_ToM_comboBox; ComboBox Rent_ToY_comboBox; ComboBox Rent_NumMonths_comboBox; DatePicker Rent_Date;
        DataGrid Rent_Grid; ComboBox Rent_Plot_comboBox; ComboBox Rent_Space_comboBox; ComboBox Rent_SpaceNo_comboBox;
        TextBox Rent_Name_textBox; TextBox Rent_MonthlyRent_textBox; TextBox Rent_MonthlyWater_textBox;
        TextBox Rent_RentArears_textBox; TextBox Rent_WaterArears_textBox; TextBox Rent_TotalRent_textBox;
        TextBox Rent_TotalWater_textBox; TextBox Rent_TotalAmount_textBox; TextBox Rent_SerialNo_textBox;
        TextBox Rent_CNIC_textBox; TextBox Rent_ID_textBox; ComboBox Rent_PaymentMode; TextBox Rent_RentParticulars_textBox;
        TextBox Rent_WaterParticulars_textBox; TextBox Rent_RentArearsParticulars_textBox; TextBox Rent_WaterArearsParticulars_textBox;
        TextBox Rent_BankReference_textBox; TextBox Rent_TotalPaid_textBox;

        public void SetRentControls(ComboBox _Rent_Trust_comboBox, ComboBox _Rent_Plot_comboBox,
            ComboBox _Rent_Space_comboBox, ComboBox _Rent_SpaceNo_comboBox, TextBox _Rent_SerialNo_textBox, 
            DatePicker _Rent_Date, TextBox _Rent_Name_textBox, ComboBox _Rent_FromM_comboBox, ComboBox _Rent_FromY_comboBox, 
            ComboBox _Rent_ToM_comboBox, ComboBox _Rent_ToY_comboBox, ComboBox _Rent_NumMonths_comboBox,
            TextBox _Rent_MonthlyRent_textBox, TextBox _Rent_MonthlyWater_textBox, TextBox _Rent_TotalRent_textBox, 
            TextBox _Rent_TotalWater_textBox, TextBox _Rent_RentArears_textBox, TextBox _Rent_WaterArears_textBox, 
            TextBox _Rent_RentParticulars_textBox, TextBox _Rent_WaterParticulars_textBox, TextBox _Rent_RentArearsParticulars_textBox,
            TextBox _Rent_WaterArearsParticulars_textBox
            /*, DataGrid _Rent_Grid,
            TextBox _Rent_TotalAmount_textBox, TextBox _Rent_CNIC_textBox,
            TextBox _Rent_ID_textBox, ComboBox _Rent_PaymentMode, TextBox _Rent_BankReference_textBox, ComboBox _Tenant_Trust_comboBox, 
            ComboBox _Tenant_Plot_comboBox, DataGrid _Tenant_Grid, TextBox _Rent_TotalPaid_textBox*/)
        {
            Rent_Trust_comboBox = _Rent_Trust_comboBox; Rent_Plot_comboBox = _Rent_Plot_comboBox;
            Rent_Space_comboBox = _Rent_Space_comboBox; Rent_SpaceNo_comboBox = _Rent_SpaceNo_comboBox;
            Rent_SerialNo_textBox = _Rent_SerialNo_textBox; Rent_Date = _Rent_Date;
            Rent_FromM_comboBox = _Rent_FromM_comboBox; Rent_FromY_comboBox = _Rent_FromY_comboBox; 
            Rent_ToM_comboBox = _Rent_ToM_comboBox; Rent_ToY_comboBox = _Rent_ToY_comboBox; 
            Rent_NumMonths_comboBox = _Rent_NumMonths_comboBox; Rent_Name_textBox = _Rent_Name_textBox;
            Rent_MonthlyRent_textBox = _Rent_MonthlyRent_textBox; Rent_MonthlyWater_textBox = _Rent_MonthlyWater_textBox; 
            Rent_RentArears_textBox = _Rent_RentArears_textBox; Rent_WaterArears_textBox = _Rent_WaterArears_textBox; 
            Rent_TotalRent_textBox = _Rent_TotalRent_textBox; Rent_TotalWater_textBox = _Rent_TotalWater_textBox;
            Rent_RentParticulars_textBox = _Rent_RentParticulars_textBox; Rent_WaterParticulars_textBox = _Rent_WaterParticulars_textBox;
            Rent_RentArearsParticulars_textBox = _Rent_RentArearsParticulars_textBox;
            Rent_WaterArearsParticulars_textBox = _Rent_WaterArearsParticulars_textBox;
            /*Rent_Grid = _Rent_Grid;
            Rent_TotalAmount_textBox = _Rent_TotalAmount_textBox;
            Rent_CNIC_textBox = _Rent_CNIC_textBox;
            Rent_ID_textBox = _Rent_ID_textBox; Rent_PaymentMode = _Rent_PaymentMode;
            Rent_BankReference_textBox = _Rent_BankReference_textBox; Rent_TotalPaid_textBox = _Rent_TotalPaid_textBox;*/
        }

        public void RentTab()
        {
            Rent_Trust_comboBox.ItemsSource = bl.GetTrustNames();
            /*string[] Months = bl.GetMonthNames();
            string[] NumMonths = bl.GetMonthNos();
            List<string> NumY = bl.GetRentYears();
            Rent_FromM_comboBox.ItemsSource = Months;
            Rent_FromY_comboBox.ItemsSource = NumY;
            Rent_ToM_comboBox.ItemsSource = Months;
            Rent_ToY_comboBox.ItemsSource = NumY;
            Rent_NumMonths_comboBox.ItemsSource = NumMonths;*/
        }
        public void InitializeRent()
        {
            Rent_Date.Text = DateTime.Now.Date.ToString();
            Rent_NumMonths_comboBox.SelectedIndex = -1;
            Rent_ToM_comboBox.SelectedIndex = -1;
            Rent_ToY_comboBox.SelectedIndex = -1;
            Rent_Grid.SelectedIndex = -1;
        }
        public void InitializeRent2()
        {
            if (Rent_Trust_comboBox.SelectedIndex < 0 || Rent_Plot_comboBox.SelectedIndex < 0
                || Rent_Space_comboBox.SelectedIndex < 0 || Rent_SpaceNo_comboBox.SelectedIndex < 0)
            {
                throw new RentUIException("Please select Trust, Plot, Shop/Flat, Shop/Flat No");
            }
            int TrustIndex = Rent_Trust_comboBox.SelectedIndex + 1;
            int PlotIndex = Rent_Plot_comboBox.SelectedIndex + 1;
            string SpaceType = Rent_Space_comboBox.SelectedValue.ToString();
            string SpaceNo = Rent_SpaceNo_comboBox.SelectedValue.ToString();
            List<string> TenantInfo = bl.GetTenantInfo(TrustIndex.ToString(), PlotIndex.ToString(), SpaceType.ToString(), SpaceNo.ToString());
            Rent_Name_textBox.Text = TenantInfo[1].ToString();
            Rent_MonthlyRent_textBox.Text = TenantInfo[6].ToString();
            Rent_MonthlyWater_textBox.Text = TenantInfo[7].ToString();
            int month = 0;
            if (!string.IsNullOrEmpty(TenantInfo[8].ToString()))
            {
                int.TryParse(TenantInfo[8], out month);
                if (month > 11)
                    Rent_FromM_comboBox.SelectedIndex = 0;
                else
                    Rent_FromM_comboBox.SelectedIndex = month;
            }
            if (!string.IsNullOrEmpty(TenantInfo[9].ToString()))
            {
                if (month > 11)
                {
                    int year = 0;
                    int.TryParse(TenantInfo[9].ToString(), out year);
                    year++;
                    Rent_FromY_comboBox.SelectedValue = year.ToString();
                }
                else
                    Rent_FromY_comboBox.SelectedValue = TenantInfo[9].ToString();
            }
        }
        public void Rent_Trust_comboBox_SelectionChanged()
        {
            if (Rent_Trust_comboBox.SelectedIndex < 0)
            {
                throw new RentUIException("Please select trust");
            }
            int index = Rent_Trust_comboBox.SelectedIndex + 1;
            Rent_Plot_comboBox.ItemsSource = bl.GetPlots(index.ToString());
            Rent_Space_comboBox.ItemsSource = null;
            //Rent_PaymentMode.ItemsSource = null;
        }
        public void Rent_Plot_comboBox_SelectionChanged()
        {
            if (Rent_Trust_comboBox.SelectedIndex < 0)
            {
                throw new RentUIException("Please select Trust");
            }
            if (Rent_Plot_comboBox.SelectedIndex < 0)
            {
                throw new RentUIException("Please select Plot");
            }
            int TrustIndex = Rent_Trust_comboBox.SelectedIndex + 1;
            int PlotIndex = Rent_Plot_comboBox.SelectedIndex + 1;
            string PlotNo = Rent_Plot_comboBox.SelectedItem.ToString();
            Rent_Space_comboBox.ItemsSource = bl.GetSpaceType(TrustIndex.ToString(), PlotIndex.ToString());
            Rent_SpaceNo_comboBox.ItemsSource = null;
            //Rent_PaymentMode.ItemsSource = bl.GetRentContraAccounts(TrustIndex, PlotNo);
            //Rent_PaymentMode.SelectedIndex = Rent_PaymentMode.Items.IndexOf(new KeyValuePair<string, string>("1", "CASH"));
            //Rent_PaymentMode.SelectedIndex = 1;
        }

        public void Rent_Space_comboBox_SelectionChanged()
        {
            if (Rent_Trust_comboBox.SelectedIndex < 0 || Rent_Plot_comboBox.SelectedIndex < 0 || Rent_Space_comboBox.SelectedIndex < 0)
            {
                throw new RentUIException("Please select Trust, Plot, Shop/Flat");
            }
            int TrustIndex = Rent_Trust_comboBox.SelectedIndex + 1;
            int PlotIndex = Rent_Plot_comboBox.SelectedIndex + 1;
            string SpaceType = Rent_Space_comboBox.SelectedValue.ToString();
            Rent_SpaceNo_comboBox.ItemsSource = bl.GetSpaceNo(TrustIndex.ToString(), PlotIndex.ToString(), SpaceType);
        }

        public void Rent_SpaceNo_comboBox_SelectionChanged()
        {
            if (Rent_Trust_comboBox.SelectedIndex < 0 || Rent_Plot_comboBox.SelectedIndex < 0
                || Rent_Space_comboBox.SelectedIndex < 0 || Rent_SpaceNo_comboBox.SelectedIndex < 0)
            {
                throw new RentUIException("Please select Trust, Plot, Shop/Flat, Shop/Flat No");
            }
            int TrustIndex = Rent_Trust_comboBox.SelectedIndex + 1;
            int PlotIndex = Rent_Plot_comboBox.SelectedIndex + 1;
            string SpaceType = Rent_Space_comboBox.SelectedValue.ToString();
            string SpaceNo = Rent_SpaceNo_comboBox.SelectedValue.ToString();
            Rent_Grid.ItemsSource = bl.GetRent(TrustIndex.ToString(), PlotIndex.ToString(), SpaceType.ToString(), SpaceNo.ToString());
            InitializeRent();
            List<string> TenantInfo = bl.GetTenantInfo(TrustIndex.ToString(), PlotIndex.ToString(), SpaceType.ToString(), SpaceNo.ToString());
            Rent_Name_textBox.Text = TenantInfo[1].ToString();
            Rent_MonthlyRent_textBox.Text = TenantInfo[6].ToString();
            Rent_MonthlyWater_textBox.Text = TenantInfo[7].ToString();
            int month = 0;
            if (!string.IsNullOrEmpty(TenantInfo[8].ToString()))
            {
                int.TryParse(TenantInfo[8], out month);
                if (month > 11)
                    Rent_FromM_comboBox.SelectedIndex = 0;
                else
                    Rent_FromM_comboBox.SelectedIndex = month;
            }
            if (!string.IsNullOrEmpty(TenantInfo[9].ToString()))
            {
                if (month > 11)
                {
                    int year = 0;
                    int.TryParse(TenantInfo[9].ToString(), out year);
                    year++;
                    Rent_FromY_comboBox.SelectedValue = year.ToString();
                }
                else
                    Rent_FromY_comboBox.SelectedValue = TenantInfo[9].ToString();
            }
            Rent_SerialNo_textBox.Text = bl.GetNextReceiptNo(TrustIndex.ToString());
        }

        public void CalculateRent()
        {
            double TotalRent = bl.CalculateRent(Rent_MonthlyRent_textBox.Text, Rent_NumMonths_comboBox.SelectedIndex + 1);
            double TotalWater = bl.CalculateWater(Rent_MonthlyWater_textBox.Text, Rent_NumMonths_comboBox.SelectedIndex + 1);
            double TotalArears = bl.CalculateArears(Rent_RentArears_textBox.Text, Rent_WaterArears_textBox.Text);
            double TotalAmount = TotalRent + TotalWater + TotalArears;
            Rent_TotalRent_textBox.Text = TotalRent.ToString();
            Rent_TotalWater_textBox.Text = TotalWater.ToString();
            Rent_TotalAmount_textBox.Text = TotalAmount.ToString();
        }
        public void Rent_NumMonths_comboBox_SelectionChanged()
        {
            if (Rent_FromM_comboBox.SelectedIndex > -1 && Rent_FromY_comboBox.SelectedIndex > -1 && UpdateOnce)
            {
                UpdateOnce = false;
                int NumMonths = Rent_NumMonths_comboBox.SelectedIndex;
                int FromM = Rent_FromM_comboBox.SelectedIndex + 1;
                int FromY = Rent_FromY_comboBox.SelectedIndex + 1;
                int ToM = FromM + NumMonths;
                int ToY = FromY;
                if (ToM > 12)
                {
                    ToM -= 12;
                    ToY++;
                }
                UpdateOnce = false;
                Rent_ToM_comboBox.SelectedIndex = ToM - 1;
                UpdateOnce = false;
                Rent_ToY_comboBox.SelectedIndex = ToY - 1;

                UpdateOnce = true;
            }
            else
            {
                UpdateOnce = true;
            }
            CalculateRent();
        }
        public void CalculateNumMonths()
        {
            if (Rent_FromM_comboBox.SelectedIndex > -1 && Rent_FromY_comboBox.SelectedIndex > -1
                && Rent_ToM_comboBox.SelectedIndex > -1 && Rent_ToY_comboBox.SelectedIndex > -1
                && UpdateOnce)
            {
                UpdateOnce = false;
                Rent_NumMonths_comboBox.SelectedIndex = bl.CalculateNumMonths(Rent_FromM_comboBox.SelectedIndex + 1,
                    Rent_FromY_comboBox.SelectedValue, Rent_ToM_comboBox.SelectedIndex + 1, Rent_ToY_comboBox.SelectedValue);
            }
            else
            {
                UpdateOnce = true;
            }
        }

        public void Rent_button_Click()
        {
            if (ValidateRent()) return;

            bl.InsertRent(Rent_Date.SelectedDate.ToString(), Rent_SerialNo_textBox.Text,
                Rent_Trust_comboBox.SelectedIndex + 1, Rent_Plot_comboBox.SelectedIndex +1, Rent_Plot_comboBox.SelectedItem.ToString(),
                Rent_Space_comboBox.SelectedValue.ToString(), Rent_SpaceNo_comboBox.SelectedValue.ToString(),
                Rent_MonthlyRent_textBox.Text, Rent_MonthlyWater_textBox.Text, Rent_Name_textBox.Text,
                Rent_RentArears_textBox.Text, Rent_WaterArears_textBox.Text, Rent_TotalRent_textBox.Text,
                Rent_TotalWater_textBox.Text, Rent_TotalAmount_textBox.Text,
                Rent_FromM_comboBox.SelectedIndex + 1, Rent_FromY_comboBox.SelectedValue.ToString(),
                Rent_ToM_comboBox.SelectedIndex + 1, Rent_ToY_comboBox.SelectedValue.ToString(), 
                Rent_NumMonths_comboBox.SelectedIndex + 1,
                Rent_CNIC_textBox.Text, Rent_PaymentMode.SelectedItem, Rent_RentParticulars_textBox.Text, 
                Rent_WaterParticulars_textBox.Text, Rent_RentArearsParticulars_textBox.Text,
                Rent_WaterArearsParticulars_textBox.Text, Rent_BankReference_textBox.Text, Rent_TotalPaid_textBox.Text);

            Rent_Grid.ItemsSource = bl.GetRent((Rent_Trust_comboBox.SelectedIndex + 1).ToString(), (Rent_Plot_comboBox.SelectedIndex + 1).ToString(),
                Rent_Space_comboBox.SelectedValue.ToString(), Rent_SpaceNo_comboBox.SelectedValue.ToString());
            Rent_Grid.SelectedIndex = Rent_Grid.Items.Count - 2;
        }

        public void Rent_Grid_SelectionChanged()
        {
            int SelectedIndex = Rent_Grid.SelectedIndex;
            if (SelectedIndex > -1 && Rent_Grid.Items.Count > 0 && Rent_Grid.Items[SelectedIndex] != null
                && (Rent_Grid.Items[SelectedIndex] as Rent) != null)
            {
                Rent rent = Rent_Grid.Items[SelectedIndex] as Rent;
                Rent_Date.Text = util.FormatDateUS(rent.Date);
                string[] fromdate = util.Splitdate(rent.FDate);
                string[] todate = util.Splitdate(rent.TDate);
                Rent_FromM_comboBox.SelectedIndex = Convert.ToInt32(fromdate[1]) - 1;
                Rent_FromY_comboBox.SelectedValue = fromdate[2];
                Rent_ToM_comboBox.SelectedIndex = Convert.ToInt32(todate[1]) - 1;
                Rent_ToY_comboBox.SelectedValue = todate[2];
                Rent_TotalRent_textBox.Text = rent.TotalRent;
                Rent_TotalWater_textBox.Text = rent.TotalWCharges;
                Rent_RentArears_textBox.Text = rent.RArears;
                Rent_WaterArears_textBox.Text = rent.WArears;
                Rent_TotalAmount_textBox.Text = rent.Total;
                Rent_RentParticulars_textBox.Text = rent.Rpart;
                Rent_WaterParticulars_textBox.Text = rent.Wpart;
                Rent_RentArearsParticulars_textBox.Text = rent.RApart;
                Rent_WaterArearsParticulars_textBox.Text = rent.WApart;
            }
            //else
            //{
                //throw new RentUIException("Could not get rent");
            //}
        }

        public bool ValidateRent()
        {
            if (Rent_SerialNo_textBox.Text == "" || Rent_Trust_comboBox.SelectedIndex == -1 || Rent_Plot_comboBox.SelectedIndex == -1
                || Rent_Space_comboBox.SelectedIndex == -1 || Rent_SpaceNo_comboBox.SelectedIndex == -1 || Rent_NumMonths_comboBox.SelectedIndex == -1)
            {
                throw new RentUIException("Serial No, Trust, Plot, Space, Space No, Number of Months cannot be blank");
            }
            if (!bl.ValidateAmount(Rent_TotalAmount_textBox.Text))
            {
                throw new RentUIException("Please enter valid amount");
            }
            if (!bl.ValidateDateRange(Rent_FromM_comboBox.SelectedIndex + 1, Rent_FromY_comboBox.SelectedValue.ToString(),
                Rent_ToM_comboBox.SelectedIndex + 1, Rent_ToY_comboBox.SelectedValue.ToString()))
            {
                throw new RentUIException("Please enter valid date range");
            }
            return true;
        }

        public void Rent_Updatebutton_Click()
        {
            if (ValidateRent()) return;
            bl.UpdateRent(Rent_Date.SelectedDate.ToString(), Rent_SerialNo_textBox.Text,
                Rent_Trust_comboBox.SelectedIndex + 1, Rent_Plot_comboBox.SelectedIndex + 1, Rent_Plot_comboBox.SelectedItem.ToString(),
                Rent_Space_comboBox.SelectedValue.ToString(), Rent_SpaceNo_comboBox.SelectedValue.ToString(),
                Rent_MonthlyRent_textBox.Text, Rent_MonthlyWater_textBox.Text, Rent_Name_textBox.Text,
                Rent_RentArears_textBox.Text, Rent_WaterArears_textBox.Text, Rent_TotalRent_textBox.Text,
                Rent_TotalWater_textBox.Text, Rent_TotalAmount_textBox.Text,
                Rent_FromM_comboBox.SelectedIndex + 1, Rent_FromY_comboBox.SelectedValue.ToString(),
                Rent_ToM_comboBox.SelectedIndex + 1, Rent_ToY_comboBox.SelectedValue.ToString(), Rent_NumMonths_comboBox.SelectedIndex + 1,
                Rent_CNIC_textBox.Text, Rent_ID_textBox.Text, Rent_PaymentMode.SelectedItem, Rent_RentParticulars_textBox.Text,
                Rent_WaterParticulars_textBox.Text, Rent_RentArearsParticulars_textBox.Text,
                Rent_WaterArearsParticulars_textBox.Text, Rent_BankReference_textBox.Text);
            Rent_Grid.ItemsSource = bl.GetRent((Rent_Trust_comboBox.SelectedIndex + 1).ToString(), (Rent_Plot_comboBox.SelectedIndex + 1).ToString(),
                Rent_Space_comboBox.SelectedValue.ToString(), Rent_SpaceNo_comboBox.SelectedValue.ToString());
            InitializeRent();
        }

        public void Rent_Printbutton_Click()
        {
            bl.PrintReceipt(Rent_Date.SelectedDate.ToString(), Rent_SerialNo_textBox.Text,
                Rent_Trust_comboBox.SelectedIndex + 1, Rent_Plot_comboBox.SelectedItem.ToString(),
                Rent_Space_comboBox.SelectedValue.ToString(), Rent_SpaceNo_comboBox.SelectedValue.ToString(),
                Rent_MonthlyRent_textBox.Text, Rent_MonthlyWater_textBox.Text, Rent_Name_textBox.Text,
                Rent_RentArears_textBox.Text, Rent_WaterArears_textBox.Text, Rent_TotalRent_textBox.Text,
                Rent_TotalWater_textBox.Text, Rent_TotalAmount_textBox.Text,
                Rent_FromM_comboBox.SelectedIndex + 1, Rent_FromY_comboBox.SelectedValue.ToString(),
                Rent_ToM_comboBox.SelectedIndex + 1, Rent_ToY_comboBox.SelectedValue.ToString(), Rent_NumMonths_comboBox.SelectedIndex + 1,
                Rent_CNIC_textBox.Text);
        }

        public void Rent_TotalRent_textBox_TextChanged()
        {
            if (Rent_FromM_comboBox.SelectedIndex > -1 && Rent_FromY_comboBox.SelectedValue != null &&
                Rent_ToM_comboBox.SelectedIndex > -1 && Rent_ToY_comboBox.SelectedValue != null)
            {
                Rent_RentParticulars_textBox.Text = bl.GetRentParticulars(Rent_Space_comboBox.SelectedValue.ToString(),
                Rent_SpaceNo_comboBox.SelectedValue.ToString(), Rent_MonthlyRent_textBox.Text, Rent_FromM_comboBox.SelectedIndex + 1,
                Rent_FromY_comboBox.SelectedValue.ToString(), Rent_ToM_comboBox.SelectedIndex + 1,
                Rent_ToY_comboBox.SelectedValue.ToString());
            }
            //else
            //{
                //throw new RentUIException("Please select From Month, From Year, To Month, To Year");
            //}
        }

        public void Rent_TotalWater_textBox_TextChanged()
        {
            if (Rent_FromM_comboBox.SelectedIndex > -1 && Rent_FromY_comboBox.SelectedValue != null &&
                Rent_ToM_comboBox.SelectedIndex > -1 && Rent_ToY_comboBox.SelectedValue != null)
            {
                Rent_WaterParticulars_textBox.Text = bl.GetWaterParticulars(Rent_Space_comboBox.SelectedValue.ToString(),
                Rent_SpaceNo_comboBox.SelectedValue.ToString(), Rent_MonthlyWater_textBox.Text,
                Rent_FromM_comboBox.SelectedIndex + 1,
                Rent_FromY_comboBox.SelectedValue.ToString(), Rent_ToM_comboBox.SelectedIndex + 1,
                Rent_ToY_comboBox.SelectedValue.ToString());
            }
            //else
            //{
                //throw new RentUIException("Please select From Month, From Year, To Month, To Year");
            //}
        }

        public void Rent_RentArears_textBox_TextChanged()
        {
            if (Rent_FromM_comboBox.SelectedIndex > -1 && Rent_FromY_comboBox.SelectedValue != null &&
                Rent_ToM_comboBox.SelectedIndex > -1 && Rent_ToY_comboBox.SelectedValue != null)
            {
                Rent_RentArearsParticulars_textBox.Text = bl.GetRentArearsParticulars(Rent_Space_comboBox.SelectedValue.ToString(),
                Rent_SpaceNo_comboBox.SelectedValue.ToString(), Rent_FromM_comboBox.SelectedIndex + 1,
                Rent_FromY_comboBox.SelectedValue.ToString(), Rent_ToM_comboBox.SelectedIndex + 1,
                Rent_ToY_comboBox.SelectedValue.ToString());
            }
            //else
            //{
                //throw new RentUIException("Please select From Month, From Year, To Month, To Year");
            //}
        }

        public void Rent_WaterArears_textBox_TextChanged()
        {
            if (Rent_FromM_comboBox.SelectedIndex > -1 && Rent_FromY_comboBox.SelectedValue != null &&
                Rent_ToM_comboBox.SelectedIndex > -1 && Rent_ToY_comboBox.SelectedValue != null)
            {
                Rent_WaterArearsParticulars_textBox.Text = bl.GetWaterArearsParticulars(Rent_Space_comboBox.SelectedValue.ToString(),
                Rent_SpaceNo_comboBox.SelectedValue.ToString(), Rent_FromM_comboBox.SelectedIndex + 1,
                Rent_FromY_comboBox.SelectedValue.ToString(), Rent_ToM_comboBox.SelectedIndex + 1,
                Rent_ToY_comboBox.SelectedValue.ToString());
            }
            //else
            //{
                //throw new RentUIException("Please select From Month, From Year, To Month, To Year");
            //}
        }
        public void Rent_RentParticulars_textBox_TextChanged()
        {
            Rent_BankReference_textBox.Text = bl.GetBankReference(Rent_RentParticulars_textBox.Text);
        }
    }
}
