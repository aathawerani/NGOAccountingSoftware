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
                case Section.Rent:
                    CurrentView = new RentView();
                    CurrentSectionTitle = "Rent";
                    break;
                case Section.Reports:
                    CurrentView = new PlaceholderView("Reports");
                    CurrentSectionTitle = "Reports";
                    break;
                case Section.Settings:
                    CurrentView = new PlaceholderView("Settings");
                    CurrentSectionTitle = "Settings";
                    break;
                default:
                    CurrentView = new RentView();
                    CurrentSectionTitle = "Rent";
                    break;
            }
        }
    }
}
