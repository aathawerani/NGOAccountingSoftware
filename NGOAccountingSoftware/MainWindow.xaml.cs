using System;
using System.Windows;
using TrustApplication.ViewModels;

namespace TrustApplication
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ShellViewModel();

            System.Windows.Input.Stylus.SetIsPressAndHoldEnabled(this, false);
            System.Windows.Input.Stylus.SetIsTouchFeedbackEnabled(this, false);
            System.Windows.Input.Stylus.SetIsFlicksEnabled(this, false);
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var src = System.Windows.Interop.HwndSource.FromVisual(this) as System.Windows.Interop.HwndSource;
            if (src != null) src.AddHook(WndProc);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_TABLET_QUERYSYSTEMGESTURESTATUS = 0x02C0;
            const int TABLET_DISABLE_PRESSANDHOLD = 0x00000001;
            const int TABLET_DISABLE_PENTAPFEEDBACK = 0x00000008;
            const int TABLET_DISABLE_PENBARRELFEEDBACK = 0x00000010;
            const int TABLET_DISABLE_FLICKS = 0x00010000;
            const int TABLET_DISABLE_SMOOTHSCROLLING = 0x00080000;
            const int TABLET_DISABLE_TOUCHUIFORCEOFF = 0x00200000;
            const int TABLET_DISABLE_TOUCHSWITCH = 0x00800000;
            const int TABLET_DISABLE_FLICKFALLBACKKEYS = 0x01000000;

            if (msg == WM_TABLET_QUERYSYSTEMGESTURESTATUS)
            {
                handled = true;
                int flags = TABLET_DISABLE_PRESSANDHOLD
                            | TABLET_DISABLE_PENTAPFEEDBACK
                            | TABLET_DISABLE_PENBARRELFEEDBACK
                            | TABLET_DISABLE_FLICKS
                            | TABLET_DISABLE_SMOOTHSCROLLING
                            | TABLET_DISABLE_TOUCHUIFORCEOFF
                            | TABLET_DISABLE_TOUCHSWITCH
                            | TABLET_DISABLE_FLICKFALLBACKKEYS;
                return new IntPtr(flags);
            }
            return IntPtr.Zero;
        }

        private void OnNavItemClick(object sender, RoutedEventArgs e)
        {
            if (sender is System.Windows.Controls.Button btn && DataContext is ShellViewModel vm && btn.CommandParameter is Section s)
            {
                vm.CurrentSection = s;
            }
        }
    }
}
