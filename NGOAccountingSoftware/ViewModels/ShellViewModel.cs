using System.Collections.ObjectModel;
using TrustApplication.Views;

namespace TrustApplication.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        public ObservableCollection<NavEntry> Sections { get; } = new ObservableCollection<NavEntry>();

        private object _currentView;
        private string _currentSectionTitle;
        private Section _currentSection;

        public ShellViewModel()
        {
            BuildSections();
            CurrentSection = Section.Rent;
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

        private void BuildSections()
        {
            // Masters
            Sections.Add(new NavEntry { Title = "Trusts", Category = "Masters", Section = Section.Trusts });
            Sections.Add(new NavEntry { Title = "Plots", Category = "Masters", Section = Section.Plots });
            Sections.Add(new NavEntry { Title = "Spaces", Category = "Masters", Section = Section.Spaces });
            Sections.Add(new NavEntry { Title = "Tenants", Category = "Masters", Section = Section.Tenants });
            Sections.Add(new NavEntry { Title = "Shops", Category = "Masters", Section = Section.Shops });
            Sections.Add(new NavEntry { Title = "Units", Category = "Masters", Section = Section.Units });
            Sections.Add(new NavEntry { Title = "Zones", Category = "Masters", Section = Section.Zones });
            Sections.Add(new NavEntry { Title = "Markets", Category = "Masters", Section = Section.Markets });

            // Billing
            Sections.Add(new NavEntry { Title = "Receipts", Category = "Billing", Section = Section.Receipts });
            Sections.Add(new NavEntry { Title = "Adjustments", Category = "Billing", Section = Section.Adjustments });
            Sections.Add(new NavEntry { Title = "Invoices", Category = "Billing", Section = Section.Invoices });
            Sections.Add(new NavEntry { Title = "Payments", Category = "Billing", Section = Section.Payments });
            Sections.Add(new NavEntry { Title = "Refunds", Category = "Billing", Section = Section.Refunds });

            // Reports
            Sections.Add(new NavEntry { Title = "Summary", Category = "Reports", Section = Section.Summary });
            Sections.Add(new NavEntry { Title = "Ledger", Category = "Reports", Section = Section.Ledger });
            Sections.Add(new NavEntry { Title = "Arrears", Category = "Reports", Section = Section.Arrears });
            Sections.Add(new NavEntry { Title = "Occupancy", Category = "Reports", Section = Section.Occupancy });
            Sections.Add(new NavEntry { Title = "Revenue by Plot", Category = "Reports", Section = Section.RevenueByPlot });
            Sections.Add(new NavEntry { Title = "Water Usage", Category = "Reports", Section = Section.WaterUsage });

            // Main
            Sections.Add(new NavEntry { Title = "Rent", Category = "Main", Section = Section.Rent });

            // Settings
            Sections.Add(new NavEntry { Title = "Users", Category = "Settings", Section = Section.Users });
            Sections.Add(new NavEntry { Title = "Roles", Category = "Settings", Section = Section.Roles });
            Sections.Add(new NavEntry { Title = "Preferences", Category = "Settings", Section = Section.Preferences });
            Sections.Add(new NavEntry { Title = "Backup", Category = "Settings", Section = Section.Backup });
            Sections.Add(new NavEntry { Title = "About", Category = "Settings", Section = Section.About });
        }

        private void Navigate(Section section)
        {
            CurrentSectionTitle = section.ToString();
            switch (section)
            {
                case Section.Rent:
                    CurrentView = new RentView();
                    CurrentSectionTitle = "Rent";
                    break;
                default:
                    CurrentView = new PlaceholderView(CurrentSectionTitle);
                    break;
            }
        }
    }
}
