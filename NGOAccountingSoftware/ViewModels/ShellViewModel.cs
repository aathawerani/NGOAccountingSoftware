using TrustApplication.Views;

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
            get => _currentSection;
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
            get => _currentView;
            private set { _currentView = value; OnPropertyChanged(); }
        }

        public string CurrentSectionTitle
        {
            get => _currentSectionTitle;
            private set { _currentSectionTitle = value; OnPropertyChanged(); }
        }

        private void Navigate(Section section)
        {
            switch (section)
            {
                // Operations
                case Section.Rent:
                    CurrentView = new RentView();
                    CurrentSectionTitle = "Rent";
                    break;
                case Section.Tenants:
                    CurrentView = new PlaceholderView("Tenants");
                    CurrentSectionTitle = "Tenants";
                    break;
                case Section.Invoices:
                    CurrentView = new PlaceholderView("Invoices");
                    CurrentSectionTitle = "Invoices";
                    break;
                case Section.Receipts:
                    CurrentView = new PlaceholderView("Receipts");
                    CurrentSectionTitle = "Receipts";
                    break;

                // Reports
                case Section.ReportsDaily:
                    CurrentView = new PlaceholderView("Reports - Daily");
                    CurrentSectionTitle = "Reports • Daily";
                    break;
                case Section.ReportsMonthly:
                    CurrentView = new PlaceholderView("Reports - Monthly");
                    CurrentSectionTitle = "Reports • Monthly";
                    break;
                case Section.ReportsYearly:
                    CurrentView = new PlaceholderView("Reports - Yearly");
                    CurrentSectionTitle = "Reports • Yearly";
                    break;

                // Administration
                case Section.Settings:
                    CurrentView = new PlaceholderView("Settings");
                    CurrentSectionTitle = "Settings";
                    break;
                case Section.Users:
                    CurrentView = new PlaceholderView("Users");
                    CurrentSectionTitle = "Users";
                    break;
                case Section.Backup:
                    CurrentView = new PlaceholderView("Backup / Restore");
                    CurrentSectionTitle = "Backup / Restore";
                    break;

                default:
                    CurrentView = new RentView();
                    CurrentSectionTitle = "Rent";
                    break;
            }
        }
    }
}
