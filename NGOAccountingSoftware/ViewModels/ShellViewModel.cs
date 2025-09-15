using TrustApplication.Views;
using TrustApplication.ViewModels;

namespace TrustApplication.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        private Section _currentSection = Section.Rent;
        private object _currentView;
        private string _currentSectionTitle;

        public ShellViewModel()
        {
            Navigate(_currentSection);
        }

        public Section CurrentSection
        {
            get { return _currentSection; }
            set
            {
                if (_currentSection != value)
                {
                    _currentSection = value;
                    OnPropertyChanged();
                    Navigate(value);
                }
            }
        }

        public object CurrentView
        {
            get { return _currentView; }
            private set { _currentView = value; OnPropertyChanged(); }
        }

        public string CurrentSectionTitle
        {
            get { return _currentSectionTitle; }
            private set { _currentSectionTitle = value; OnPropertyChanged(); }
        }

        private void Navigate(Section section)
        {
            switch (section)
            {
                // Operations
                case Section.Rent:
                    CurrentView = new RentView { DataContext = new RentViewModel() };
                    CurrentSectionTitle = "Rent";
                    break;
                case Section.Tenants:
                    CurrentView = new TenantView();
                    CurrentSectionTitle = "Tenants";
                    break;

                // Reports
                case Section.Majlis:
                    CurrentView = new MajlisView();
                    CurrentSectionTitle = "Receipt";
                    break;
                case Section.Voucher:
                    CurrentView = new VoucherView();
                    CurrentSectionTitle = "Voucher";
                    break;


                // Administration
                case Section.Accounts:
                    CurrentView = new AccountsView();
                    CurrentSectionTitle = "Manual Entries";
                    break;
                case Section.Invest:
                    CurrentView = new InvestView();
                    CurrentSectionTitle = "Investments";
                    break;
                case Section.Cash:
                    CurrentView = new CashView();
                    CurrentSectionTitle = "Cash";
                    break;
                case Section.Receivables:
                    CurrentView = new ReceiveView();
                    CurrentSectionTitle = "Receivables";
                    break;
                case Section.Import:
                    CurrentView = new ImportView();
                    CurrentSectionTitle = "Import";
                    break;
                case Section.Export:
                    CurrentView = new ReportsView();
                    CurrentSectionTitle = "Exports";
                    break;

                default:
                    CurrentView = new RentView { DataContext = new RentViewModel() };
                    CurrentSectionTitle = "Rent";
                    break;
            }
        }
    }
}
