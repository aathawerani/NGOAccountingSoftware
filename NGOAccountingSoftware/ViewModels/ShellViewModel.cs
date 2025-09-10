using System; using System.Collections.ObjectModel; using System.Linq; using System.Reflection;
namespace TrustApplication.ViewModels {
    public class ShellViewModel : ViewModelBase {
        public ObservableCollection<NavEntry> Sections { get; } = new ObservableCollection<NavEntry>();
        private object _currentView; private string _currentSectionTitle;
        public ShellViewModel(){ BuildSectionsFromTabs(); NavigateByTitle("Rent"); }
        public object CurrentView { get=>_currentView; private set { _currentView=value; OnPropertyChanged(); } }
        public string CurrentSectionTitle { get=>_currentSectionTitle; private set { _currentSectionTitle=value; OnPropertyChanged(); } }
        private void BuildSectionsFromTabs(){
            var titles = new[]{"Rent","Tenant","Majlis","Voucher","Invest","Misc","Cash","Receive","Accounts","Reports","Closing","Settings","Import"};
            foreach(var t in titles) Sections.Add(new NavEntry{ Title=t, Category="Pages"});
        }
        public void NavigateByTitle(string title){
            if(string.IsNullOrWhiteSpace(title)) return; CurrentSectionTitle = title;
            var name1 = $"TrustApplication.Views.{Normalize(title)}View";
            var viewType = AppDomain.CurrentDomain.GetAssemblies().Select(a=>a.GetType(name1,false,true)).FirstOrDefault(t=>t!=null);
            if(viewType!=null){ CurrentView = Activator.CreateInstance(viewType); return; }
            var name2 = $"TrustApplication.Views.{RemoveSpaces(title)}View";
            viewType = AppDomain.CurrentDomain.GetAssemblies().Select(a=>a.GetType(name2,false,true)).FirstOrDefault(t=>t!=null);
            if(viewType!=null){ CurrentView = Activator.CreateInstance(viewType); return; }
            var ph = AppDomain.CurrentDomain.GetAssemblies().Select(a=>a.GetType("TrustApplication.Views.PlaceholderView", false, true)).FirstOrDefault();
            CurrentView = ph!=null ? Activator.CreateInstance(ph, new object[]{ title }) : null;
        }
        private static string RemoveSpaces(string s)=> new string(s.Where(c=>!char.IsWhiteSpace(c)).ToArray());
        private static string Normalize(string s){ var parts=s.Split(' '); for(int i=0;i<parts.Length;i++){ if(parts[i].Length>0) parts[i]=char.ToUpperInvariant(parts[i][0])+parts[i][1..]; } return string.Join("", parts); }
    }
}