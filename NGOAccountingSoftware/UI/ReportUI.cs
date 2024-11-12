using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TrustApplication.Exceptions;

namespace TrustApplication
{
    class ReportUI
    {
        //DAL dal = new DAL();
        ReportBL bl = new ReportBL(); AccountsBL abl = new AccountsBL();
        StatementsBL sbl = new StatementsBL();
        ComboBox Reports_Trust_comboBox; ComboBox Reports_Year_comboBox; DataGrid Report_Grid;
        ComboBox Reports_Statement_comboBox;

        public void SetControls(ComboBox _Reports_Trust_comboBox, ComboBox _Reports_Year_comboBox,
            DataGrid _Report_Grid, ComboBox _Reports_Statement_comboBox)
        {
            Reports_Trust_comboBox = _Reports_Trust_comboBox; Reports_Year_comboBox = _Reports_Year_comboBox;
            Report_Grid = _Report_Grid; Reports_Statement_comboBox = _Reports_Statement_comboBox;
        }

        public void ReportTab()
        {
            Reports_Trust_comboBox.ItemsSource = bl.GetTrustNames();
            Reports_Year_comboBox.ItemsSource = bl.GetAccountingYears();
            Reports_Statement_comboBox.ItemsSource = bl.GetStatementList();
        }

        bool ReportValidations()
        {
            int TrustCode = Reports_Trust_comboBox.SelectedIndex;
            if (TrustCode < 0)
            {
                throw new ReportUIException("Trust not seleted");
            }
            int year = Reports_Year_comboBox.SelectedIndex;
            if (year < 0)
            {
                throw new ReportUIException("Year not seleted");
            }
            return true;
        }

        public void Report_Accounts_button_Click()
        {
            if (ReportValidations())
            {
                int Trustcode = Reports_Trust_comboBox.SelectedIndex + 1;
                bl.WriteAccounts(Trustcode, Reports_Year_comboBox.SelectedValue.ToString());
            }
        }

        public void Report_Statements_button_Click()
        {
            int TrustCode = Reports_Trust_comboBox.SelectedIndex;
            TrustCode++;
            int year = Reports_Year_comboBox.SelectedIndex;
            string Year = Reports_Year_comboBox.SelectedValue.ToString();
            bl.YearlyStatement(TrustCode, Year);
        }

        public void Reports_Statement_comboBox_SelectionChanged()
        {
            string value = Reports_Statement_comboBox.SelectedValue.ToString();
            int TrustCode = Reports_Trust_comboBox.SelectedIndex;
            string Year = Reports_Year_comboBox.SelectedValue.ToString();

            TrustCode++;
            if(value == "Trial Balance")
            {
                Report_Grid.ItemsSource = bl.TrialBalance(TrustCode, Year);
            }
            else
            {
                throw new ReportUIException("Invalid option");
            }
        }
    }
}
