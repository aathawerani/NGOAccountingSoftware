using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace TrustApplication.ViewModels
{
    public class ShellViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Section> Sections { get; } = new ObservableCollection<Section>();

        private Section _currentSection;
        public Section CurrentSection
        {
            get { return _currentSection; }
            set { _currentSection = value; OnPropertyChanged(); }
        }

        public ShellViewModel()
        {
            var rentViewModel = new RentViewModel();
            var rentView = new TrustApplication.Views.RentView();
            rentView.DataContext = rentViewModel;
            Sections.Add(new Section("Rent", "Operations", rentView, rentViewModel));
            var tenantViewModel = new TenantViewModel();
            var tenantView = new TrustApplication.Views.TenantView();
            tenantView.DataContext = tenantViewModel;
            Sections.Add(new Section("Tenant", "Operations", tenantView, tenantViewModel));
            var majlisViewModel = new MajlisViewModel();
            var majlisView = new TrustApplication.Views.MajlisView();
            majlisView.DataContext = majlisViewModel;
            Sections.Add(new Section("Majlis", "Events", majlisView, majlisViewModel));
            var voucherViewModel = new VoucherViewModel();
            var voucherView = new TrustApplication.Views.VoucherView();
            voucherView.DataContext = voucherViewModel;
            Sections.Add(new Section("Voucher", "Finance", voucherView, voucherViewModel));
            var investViewModel = new InvestViewModel();
            var investView = new TrustApplication.Views.InvestView();
            investView.DataContext = investViewModel;
            Sections.Add(new Section("Invest", "Finance", investView, investViewModel));
            var miscViewModel = new MiscViewModel();
            var miscView = new TrustApplication.Views.MiscView();
            miscView.DataContext = miscViewModel;
            Sections.Add(new Section("Misc", "Admin", miscView, miscViewModel));
            var cashViewModel = new CashViewModel();
            var cashView = new TrustApplication.Views.CashView();
            cashView.DataContext = cashViewModel;
            Sections.Add(new Section("Cash", "Finance", cashView, cashViewModel));
            var receiveViewModel = new ReceiveViewModel();
            var receiveView = new TrustApplication.Views.ReceiveView();
            receiveView.DataContext = receiveViewModel;
            Sections.Add(new Section("Receive", "Operations", receiveView, receiveViewModel));
            var accountsViewModel = new AccountsViewModel();
            var accountsView = new TrustApplication.Views.AccountsView();
            accountsView.DataContext = accountsViewModel;
            Sections.Add(new Section("Accounts", "Finance", accountsView, accountsViewModel));
            var reportsViewModel = new ReportsViewModel();
            var reportsView = new TrustApplication.Views.ReportsView();
            reportsView.DataContext = reportsViewModel;
            Sections.Add(new Section("Reports", "Reporting", reportsView, reportsViewModel));
            var closingViewModel = new ClosingViewModel();
            var closingView = new TrustApplication.Views.ClosingView();
            closingView.DataContext = closingViewModel;
            Sections.Add(new Section("Closing", "Admin", closingView, closingViewModel));
            var settingsViewModel = new SettingsViewModel();
            var settingsView = new TrustApplication.Views.SettingsView();
            settingsView.DataContext = settingsViewModel;
            Sections.Add(new Section("Settings", "Admin", settingsView, settingsViewModel));
            var importViewModel = new ImportViewModel();
            var importView = new TrustApplication.Views.ImportView();
            importView.DataContext = importViewModel;
            Sections.Add(new Section("Import", "Admin", importView, importViewModel));
            CurrentSection = Sections.Count > 0 ? Sections[0] : null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Section
    {
        public string Title { get; private set; }
        public string Category { get; private set; }
        public UserControl View { get; private set; }
        public object ViewModel { get; private set; }

        public Section(string title, string category, UserControl view, object viewModel)
        {
            Title = title;
            Category = category;
            View = view;
            ViewModel = viewModel;
        }
    }
}
