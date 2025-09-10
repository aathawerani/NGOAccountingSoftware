using System.Windows.Controls;

namespace TrustApplication.Views
{
    public partial class PlaceholderView : UserControl
    {
        public PlaceholderView(string name)
        {
            InitializeComponent();
            Label.Text = name;
        }
    }
}
