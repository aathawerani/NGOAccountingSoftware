using System;
using System.Windows;
using System.Windows.Controls;

namespace TrustApplication.Views
{
    public partial class RentView : UserControl
    {
        public RentView()
        {
            InitializeComponent();
        }

        // === Selection handlers ===
        private void Rent_Trust_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) { /* paste your logic */ }
        private void Rent_Plot_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) { /* paste your logic */ }
        private void Rent_Space_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) { /* paste your logic */ }
        private void Rent_SpaceNo_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) { /* paste your logic */ }
        private void Rent_NumMonths_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) { /* paste your logic */ }

        // === Date/Calc ===
        private void CacluateNumMonths(object sender, SelectionChangedEventArgs e) { /* paste your logic */ }
        private void CalculateRent(object sender, RoutedEventArgs e) { /* paste your logic */ }

        // === Text change ===
        private void Rent_RentParticulars_textBox_TextChanged(object sender, TextChangedEventArgs e) { /* paste your logic */ }
        private void Rent_TotalRent_textBox_TextChanged(object sender, TextChangedEventArgs e) { /* paste your logic */ }
        private void Rent_TotalWater_textBox_TextChanged(object sender, TextChangedEventArgs e) { /* paste your logic */ }
        private void Rent_RentArears_textBox_TextChanged(object sender, TextChangedEventArgs e) { /* paste your logic */ }
        private void Rent_WaterArears_textBox_TextChanged(object sender, TextChangedEventArgs e) { /* paste your logic */ }

        // === Buttons ===
        private void Rent_Updatebutton_Click(object sender, RoutedEventArgs e) { /* paste your logic */ }
        private void Rent_Clear_Click(object sender, RoutedEventArgs e) { /* paste your logic */ }
        private void Rent_Printbutton_Click(object sender, RoutedEventArgs e) { /* paste your logic */ }
        private void Rent_button_Click(object sender, RoutedEventArgs e) { /* paste your logic */ }

        // === Grid ===
        private void Rent_Grid_SelectionChanged(object sender, SelectionChangedEventArgs e) { /* paste your logic */ }
    }
}
