using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TrustApplication.Behaviors
{
    // Attached behavior: when enabled on a Panel containing Expanders,
    // expanding one expander collapses all others (accordion behavior).
    public static class Accordion
    {
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached(
                "IsEnabled",
                typeof(bool),
                typeof(Accordion),
                new PropertyMetadata(false, OnIsEnabledChanged));

        public static bool GetIsEnabled(DependencyObject obj) { return (bool)obj.GetValue(IsEnabledProperty); }
        public static void SetIsEnabled(DependencyObject obj, bool value) { obj.SetValue(IsEnabledProperty, value); }

        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var panel = d as Panel;
            if (panel == null) return;

            if ((bool)e.NewValue)
            {
                panel.Loaded -= Panel_Loaded;
                panel.Loaded += Panel_Loaded;
            }
            else
            {
                panel.Loaded -= Panel_Loaded;
                foreach (var child in panel.Children.OfType<Expander>())
                    child.Expanded -= Expander_Expanded;
            }
        }

        private static void Panel_Loaded(object sender, RoutedEventArgs e)
        {
            var panel = sender as Panel;
            if (panel == null) return;

            foreach (var expander in panel.Children.OfType<Expander>())
            {
                expander.Expanded -= Expander_Expanded;
                expander.Expanded += Expander_Expanded;
            }
        }

        private static void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            var expanded = sender as Expander;
            if (expanded == null) return;

            var panel = expanded.Parent as Panel;
            if (panel == null) return;

            foreach (var other in panel.Children.OfType<Expander>())
            {
                if (!ReferenceEquals(other, expanded) && other.IsExpanded)
                    other.IsExpanded = false;
            }
        }
    }
}
