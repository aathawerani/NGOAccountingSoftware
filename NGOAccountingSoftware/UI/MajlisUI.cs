using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TrustApplication
{
    class MajlisUI
    {
        DatePicker Majlis_Date; ComboBox Majlis_IslamicDay_comboBox; ComboBox Majlis_IslamicMonth_comboBox;
        TextBox Majlis_IslamicYear_textBox; TextBox Majlis_MilkTotal_textBox; TextBox Majlis_MilkUnit_textBox;
        TextBox Majlis_MilkUnitPrice_textBox; TextBox Majlis_SugarUnit_textBox; TextBox Majlis_SugarTotal_textBox;
        TextBox Majlis_SugarUnitPrice_textBox; TextBox Majlis_TeaTotal_textBox; TextBox Majlis_TotalBill_textBox;
        TextBox Majlis_TeaUnit_textBox; TextBox Majlis_TeaUnitPrice_textBox; TextBox Majlis_Saffron_textBox;
        TextBox Majlis_Cardamoms_textBox; TextBox Majlis_Pistachios_textBox; TextBox Majlis_Ice_textBox;
        TextBox Majlis_EssenceColor_textBox; TextBox Majlis_Miscellaneous_textBox; TextBox Majlis_LightFan_textBox;
        TextBox Majlis_Gas_textBox; TextBox Majlis_LoudSpeaker_textBox; TextBox Majlis_Molana_textBox;
        ComboBox Majlis_TimeFrom_comboBox; ComboBox Majlis_TimeTo_comboBox; ComboBox Majlis_TimeAM_comboBox;
        TextBox Majlis_SerialNo_textBox; TextBox Majlis_Name_textBox; TextBox Majlis_MiscDesc_textBox;
        TextBox Majlis_Particulars_textBox;
        MajlisBL bl = new MajlisBL();

        public void SetControls(DatePicker _Majlis_Date, ComboBox _Majlis_IslamicDay_comboBox, 
            ComboBox _Majlis_IslamicMonth_comboBox, TextBox _Majlis_IslamicYear_textBox, 
            TextBox _Majlis_MilkTotal_textBox, TextBox _Majlis_MilkUnit_textBox, TextBox _Majlis_MilkUnitPrice_textBox, 
            TextBox _Majlis_SugarUnit_textBox, TextBox _Majlis_SugarTotal_textBox, TextBox _Majlis_SugarUnitPrice_textBox, 
            TextBox _Majlis_TeaTotal_textBox, TextBox _Majlis_TotalBill_textBox, TextBox _Majlis_TeaUnit_textBox, 
            TextBox _Majlis_TeaUnitPrice_textBox, TextBox _Majlis_Saffron_textBox, TextBox _Majlis_Cardamoms_textBox, 
            TextBox _Majlis_Pistachios_textBox, TextBox _Majlis_Ice_textBox, TextBox _Majlis_EssenceColor_textBox, 
            TextBox _Majlis_Miscellaneous_textBox, TextBox _Majlis_LightFan_textBox, TextBox _Majlis_Gas_textBox, 
            TextBox _Majlis_LoudSpeaker_textBox, TextBox _Majlis_Molana_textBox, ComboBox _Majlis_TimeFrom_comboBox, 
            ComboBox _Majlis_TimeTo_comboBox, ComboBox _Majlis_TimeAM_comboBox, TextBox _Majlis_SerialNo_textBox, 
            TextBox _Majlis_Name_textBox, TextBox _Majlis_MiscDesc_textBox, TextBox _Majlis_Particulars_textBox)
        {
            Majlis_Date = _Majlis_Date; Majlis_IslamicDay_comboBox = _Majlis_IslamicDay_comboBox;
            Majlis_IslamicMonth_comboBox = _Majlis_IslamicMonth_comboBox;
            Majlis_IslamicYear_textBox = _Majlis_IslamicYear_textBox;
            Majlis_MilkTotal_textBox = _Majlis_MilkTotal_textBox; Majlis_MilkUnit_textBox = _Majlis_MilkUnit_textBox;
            Majlis_MilkUnitPrice_textBox = _Majlis_MilkUnitPrice_textBox;
            Majlis_SugarUnit_textBox = _Majlis_SugarUnit_textBox; Majlis_SugarTotal_textBox = _Majlis_SugarTotal_textBox;
            Majlis_SugarUnitPrice_textBox = _Majlis_SugarUnitPrice_textBox;
            Majlis_TeaTotal_textBox = _Majlis_TeaTotal_textBox; Majlis_TotalBill_textBox = _Majlis_TotalBill_textBox;
            Majlis_TeaUnit_textBox = _Majlis_TeaUnit_textBox; Majlis_TeaUnitPrice_textBox = _Majlis_TeaUnitPrice_textBox;
            Majlis_Saffron_textBox = _Majlis_Saffron_textBox; Majlis_Cardamoms_textBox = _Majlis_Cardamoms_textBox;
            Majlis_Pistachios_textBox = _Majlis_Pistachios_textBox; Majlis_Ice_textBox = _Majlis_Ice_textBox;
            Majlis_EssenceColor_textBox = _Majlis_EssenceColor_textBox;
            Majlis_Miscellaneous_textBox = _Majlis_Miscellaneous_textBox;
            Majlis_LightFan_textBox = _Majlis_LightFan_textBox; Majlis_Gas_textBox = _Majlis_Gas_textBox;
            Majlis_LoudSpeaker_textBox = _Majlis_LoudSpeaker_textBox; Majlis_Molana_textBox = _Majlis_Molana_textBox;
            Majlis_TimeFrom_comboBox = _Majlis_TimeFrom_comboBox; Majlis_TimeTo_comboBox = _Majlis_TimeTo_comboBox;
            Majlis_TimeAM_comboBox = _Majlis_TimeAM_comboBox; Majlis_SerialNo_textBox = _Majlis_SerialNo_textBox;
            Majlis_Name_textBox = _Majlis_Name_textBox; Majlis_MiscDesc_textBox = _Majlis_MiscDesc_textBox;
            Majlis_Particulars_textBox = _Majlis_Particulars_textBox;
        }

        public void UpdateDate()
        {
            string[] date = bl.ConvertDate(Majlis_Date.Text);
            int day;
            int.TryParse(date[0], out day);
            int month;
            int.TryParse(date[1], out month);
            int year;
            int.TryParse(date[2], out year);
            Majlis_IslamicDay_comboBox.ItemsSource = bl.GetDays();
            Majlis_IslamicMonth_comboBox.ItemsSource = bl.GetIslamicMonths();
            Majlis_IslamicDay_comboBox.SelectedIndex = day - 1;
            Majlis_IslamicMonth_comboBox.SelectedIndex = month - 1;
            Majlis_IslamicYear_textBox.Text = year.ToString();
        }

        public void CalculateBill()
        {
            Majlis_MilkTotal_textBox.Text = bl.GetTotalPrice(Majlis_MilkUnit_textBox.Text, Majlis_MilkUnitPrice_textBox.Text);
            Majlis_SugarTotal_textBox.Text = bl.GetTotalPrice(Majlis_SugarUnit_textBox.Text,
                Majlis_SugarUnitPrice_textBox.Text);
            Majlis_TeaTotal_textBox.Text = bl.GetTotalPrice(Majlis_TeaUnit_textBox.Text, Majlis_TeaUnitPrice_textBox.Text);
            Majlis_TotalBill_textBox.Text =
            bl.CalculateMajlisBill(Majlis_MilkUnit_textBox.Text, Majlis_MilkUnitPrice_textBox.Text, Majlis_SugarUnit_textBox.Text,
                Majlis_SugarUnitPrice_textBox.Text, Majlis_TeaUnit_textBox.Text, Majlis_TeaUnitPrice_textBox.Text,
                Majlis_Saffron_textBox.Text, Majlis_Cardamoms_textBox.Text, Majlis_Pistachios_textBox.Text, Majlis_Ice_textBox.Text,
                Majlis_EssenceColor_textBox.Text, Majlis_Miscellaneous_textBox.Text, Majlis_LightFan_textBox.Text,
                Majlis_Gas_textBox.Text, Majlis_LoudSpeaker_textBox.Text, Majlis_Molana_textBox.Text);
        }
        public void MajlisTab()
        {
            Majlis_Date.Text = DateTime.Now.Date.ToString();
            UpdateDate();
            Majlis_TimeFrom_comboBox.ItemsSource = bl.GetTime();
            Majlis_TimeTo_comboBox.ItemsSource = bl.GetTime();
            Majlis_TimeAM_comboBox.ItemsSource = bl.GetAM();
            Majlis_TimeFrom_comboBox.SelectedIndex = 8;
            Majlis_TimeTo_comboBox.SelectedIndex = 9;
            Majlis_TimeAM_comboBox.SelectedIndex = 1;
        }

        public void Majlis_GenerateBill_button_Click()
        {
            bl.InsertMajlisBill(Majlis_Date.Text, Majlis_IslamicDay_comboBox.SelectedValue.ToString(),
                Majlis_IslamicMonth_comboBox.SelectedValue.ToString(), Majlis_IslamicYear_textBox.Text, Majlis_SerialNo_textBox.Text,
                Majlis_TimeFrom_comboBox.SelectedValue.ToString(), Majlis_TimeTo_comboBox.SelectedValue.ToString(),
                Majlis_Name_textBox.Text, Majlis_MilkUnit_textBox.Text, Majlis_MilkUnitPrice_textBox.Text,
                Majlis_MilkTotal_textBox.Text, Majlis_SugarUnit_textBox.Text, Majlis_SugarUnitPrice_textBox.Text, Majlis_SugarTotal_textBox.Text,
                Majlis_TeaUnit_textBox.Text, Majlis_TeaUnitPrice_textBox.Text, Majlis_TeaTotal_textBox.Text, Majlis_Saffron_textBox.Text,
                Majlis_Cardamoms_textBox.Text, Majlis_Pistachios_textBox.Text, Majlis_Ice_textBox.Text, Majlis_EssenceColor_textBox.Text,
                Majlis_Miscellaneous_textBox.Text, Majlis_LightFan_textBox.Text, Majlis_Gas_textBox.Text, Majlis_LoudSpeaker_textBox.Text,
                Majlis_Molana_textBox.Text, Majlis_TotalBill_textBox.Text, Majlis_MiscDesc_textBox.Text, Majlis_Particulars_textBox.Text);
        }

        public void Majlis_PrintBill_button_Click()
        {
            bl.PrintMajlisBill(Majlis_Date.Text, Majlis_IslamicDay_comboBox.SelectedValue.ToString(),
                Majlis_IslamicMonth_comboBox.SelectedValue.ToString(), Majlis_IslamicYear_textBox.Text, Majlis_SerialNo_textBox.Text,
                Majlis_TimeFrom_comboBox.SelectedValue.ToString(), Majlis_TimeTo_comboBox.SelectedValue.ToString(),
                Majlis_Name_textBox.Text, Majlis_MilkUnit_textBox.Text, Majlis_MilkUnitPrice_textBox.Text,
                Majlis_MilkTotal_textBox.Text, Majlis_SugarUnit_textBox.Text, Majlis_SugarUnitPrice_textBox.Text, Majlis_SugarTotal_textBox.Text,
                Majlis_TeaUnit_textBox.Text, Majlis_TeaUnitPrice_textBox.Text, Majlis_TeaTotal_textBox.Text, Majlis_Saffron_textBox.Text,
                Majlis_Cardamoms_textBox.Text, Majlis_Pistachios_textBox.Text, Majlis_Ice_textBox.Text, Majlis_EssenceColor_textBox.Text,
                Majlis_Miscellaneous_textBox.Text, Majlis_LightFan_textBox.Text, Majlis_Gas_textBox.Text, Majlis_LoudSpeaker_textBox.Text,
                Majlis_Molana_textBox.Text, Majlis_TotalBill_textBox.Text, Majlis_MiscDesc_textBox.Text, Majlis_Particulars_textBox.Text);
        }

        public void Majlis_Date_SelectedDateChanged()
        {
            UpdateDate();
        }

        public void Majlis_Name_textBox_TextChanged()
        {
            Majlis_Particulars_textBox.Text = bl.GetMajlisParticulars(Majlis_Name_textBox.Text);
        }
    }
}
