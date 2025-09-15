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
                    CurrentView = new RentView { DataContext = new RentViewModel() };
                    CurrentSectionTitle = "Rent";
                    break;
            }
        }
    }
}
