using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TrustApplication.Exceptions;

namespace TrustApplication
{
    class TenantUI
    {
        RentBL bl = new RentBL(); Utils util = new Utils();
        ComboBox Tenant_Trust_comboBox; ComboBox Tenant_Plot_comboBox; DataGrid Tenant_Grid;

        public void SetRentControls(ComboBox _Tenant_Trust_comboBox, ComboBox _Tenant_Plot_comboBox, DataGrid _Tenant_Grid)
        {
            Tenant_Trust_comboBox = _Tenant_Trust_comboBox; Tenant_Plot_comboBox = _Tenant_Plot_comboBox;
            Tenant_Grid = _Tenant_Grid;
        }
        public void TenantTab()
        {
            Tenant_Trust_comboBox.ItemsSource = bl.GetTrustNames();
        }
        public void Tenant_Trust_comboBox_SelectionChanged()
        {
            if (Tenant_Trust_comboBox.SelectedIndex < 0)
            {
                throw new TenantUIException("Trust not selected");
            }
            int index = Tenant_Trust_comboBox.SelectedIndex + 1;
            Tenant_Plot_comboBox.ItemsSource = bl.GetPlots(index.ToString());
        }
        public void Tenant_Plot_comboBox_SelectionChanged()
        {
            if (Tenant_Trust_comboBox.SelectedIndex < 0 || Tenant_Plot_comboBox.SelectedIndex < 0)
            {
                throw new TenantUIException("Trust not selected");
            }
            int TrustIndex = Tenant_Trust_comboBox.SelectedIndex + 1;
            int PlotIndex = Tenant_Plot_comboBox.SelectedIndex + 1;
            Tenant_Grid.ItemsSource = bl.GetTenants(TrustIndex.ToString(), PlotIndex.ToString());
        }

    }
}
