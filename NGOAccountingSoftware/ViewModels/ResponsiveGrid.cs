using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace TrustApplication.ViewModels
{
    public static class ResponsiveGrid
    {
        // Attach ONCE on the page root (UserControl/Window)
        public static readonly DependencyProperty EnableProperty =
            DependencyProperty.RegisterAttached("Enable", typeof(bool), typeof(ResponsiveGrid),
                new PropertyMetadata(false, OnEnableChanged));
        public static void SetEnable(DependencyObject d, bool v) => d.SetValue(EnableProperty, v);
        public static bool GetEnable(DependencyObject d) => (bool)d.GetValue(EnableProperty);

        public static readonly DependencyProperty BreakpointWidthProperty =
            DependencyProperty.RegisterAttached("BreakpointWidth", typeof(double), typeof(ResponsiveGrid),
                new PropertyMetadata(1100.0, OnEnableChanged));
        public static void SetBreakpointWidth(DependencyObject d, double v) => d.SetValue(BreakpointWidthProperty, v);
        public static double GetBreakpointWidth(DependencyObject d) => (double)d.GetValue(BreakpointWidthProperty);

        // Cache for children positions & original column widths
        private static readonly DependencyProperty OriginalRowProperty =
            DependencyProperty.RegisterAttached("OriginalRow", typeof(int?), typeof(ResponsiveGrid), new PropertyMetadata(null));
        private static void SetOriginalRow(UIElement e, int? v) => e.SetValue(OriginalRowProperty, v);
        private static int? GetOriginalRow(UIElement e) => (int?)e.GetValue(OriginalRowProperty);

        private static readonly DependencyProperty OriginalColumnProperty =
            DependencyProperty.RegisterAttached("OriginalColumn", typeof(int?), typeof(ResponsiveGrid), new PropertyMetadata(null));
        private static void SetOriginalColumn(UIElement e, int? v) => e.SetValue(OriginalColumnProperty, v);
        private static int? GetOriginalColumn(UIElement e) => (int?)e.GetValue(OriginalColumnProperty);

        private static readonly DependencyProperty InitializedProperty =
            DependencyProperty.RegisterAttached("Initialized", typeof(bool), typeof(ResponsiveGrid), new PropertyMetadata(false));
        private static void SetInitialized(Grid g, bool v) => g.SetValue(InitializedProperty, v);
        private static bool GetInitialized(Grid g) => (bool)g.GetValue(InitializedProperty);

        private static readonly DependencyProperty OriginalColWidthsProperty =
            DependencyProperty.RegisterAttached("OriginalColWidths", typeof(GridLength[]), typeof(ResponsiveGrid), new PropertyMetadata(null));
        private static void SetOriginalColWidths(Grid g, GridLength[] v) => g.SetValue(OriginalColWidthsProperty, v);
        private static GridLength[] GetOriginalColWidths(Grid g) => (GridLength[])g.GetValue(OriginalColWidthsProperty);

        private static void OnEnableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement fe)
            {
                fe.Loaded -= Root_Loaded;
                fe.SizeChanged -= Root_SizeChanged;

                if (GetEnable(fe))
                {
                    fe.Loaded += Root_Loaded;
                    fe.SizeChanged += Root_SizeChanged;
                }
            }
        }

        private static void Root_Loaded(object sender, RoutedEventArgs e)
        {
            var fe = sender as FrameworkElement;
            // Defer one tick so ActualWidth/ViewportWidth are valid
            fe?.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() => ApplyForRoot(fe)));
        }

        private static void Root_SizeChanged(object sender, SizeChangedEventArgs e) => ApplyForRoot(sender as FrameworkElement);

        private static void ApplyForRoot(FrameworkElement root)
        {
            if (root == null) return;

            double bp = GetBreakpointWidth(root);
            if (bp <= 0) return;

            double width = GetViewportOrActualWidth(root);
            if (width <= 0) return;

            bool single = width < bp;
            TraverseAndApply(root, single);
        }

        private static double GetViewportOrActualWidth(FrameworkElement fe)
        {
            // Prefer the viewport width of the nearest ScrollViewer (right content area)
            var sv = FindAncestor<ScrollViewer>(fe);
            if (sv != null && sv.ViewportWidth > 0) return sv.ViewportWidth;
            return fe.ActualWidth > 0 ? fe.ActualWidth : fe.RenderSize.Width;
        }

        private static void TraverseAndApply(DependencyObject parent, bool single)
        {
            if (parent is Grid g && g.ColumnDefinitions.Count >= 2)
                ApplyGrid(g, single);

            int n = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < n; i++)
                TraverseAndApply(VisualTreeHelper.GetChild(parent, i), single);
        }

        private static void ApplyGrid(Grid grid, bool single)
        {
            // Remember initial layout once
            if (!GetInitialized(grid))
            {
                foreach (UIElement child in grid.Children)
                {
                    if (GetOriginalRow(child) == null) SetOriginalRow(child, Grid.GetRow(child));
                    if (GetOriginalColumn(child) == null) SetOriginalColumn(child, Grid.GetColumn(child));
                }
                SetOriginalColWidths(grid, grid.ColumnDefinitions.Select(cd => cd.Width).ToArray());
                SetInitialized(grid, true);
            }

            if (single) CollapseToSingleColumn(grid);
            else RestoreOriginal(grid);
        }

        private static void CollapseToSingleColumn(Grid grid)
        {
            // Make sure we have enough rows for 1-per-child reflow
            int needed = grid.Children.Count;
            while (grid.RowDefinitions.Count < needed)
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            // Show only column 0
            if (grid.ColumnDefinitions.Count > 0)
            {
                grid.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
                for (int i = 1; i < grid.ColumnDefinitions.Count; i++)
                    grid.ColumnDefinitions[i].Width = new GridLength(0);
            }

            // Reflow in reading order to rows 0..N-1, column 0
            var ordered = grid.Children
                .OfType<UIElement>()
                .Select(ch => new
                {
                    Elem = ch,
                    Row = GetOriginalRow(ch) ?? Grid.GetRow(ch),
                    Col = GetOriginalColumn(ch) ?? Grid.GetColumn(ch)
                })
                .OrderBy(x => x.Row)
                .ThenBy(x => x.Col)
                .ToList();

            int newRow = 0;
            foreach (var item in ordered)
            {
                Grid.SetColumn(item.Elem, 0);
                Grid.SetRow(item.Elem, newRow++);
            }
        }

        private static void RestoreOriginal(Grid grid)
        {
            // Restore column widths exactly
            var widths = GetOriginalColWidths(grid);
            if (widths != null && widths.Length == grid.ColumnDefinitions.Count)
            {
                for (int i = 0; i < widths.Length; i++)
                    grid.ColumnDefinitions[i].Width = widths[i];
            }
            else if (grid.ColumnDefinitions.Count >= 2)
            {
                grid.ColumnDefinitions[0].Width = GridLength.Auto;
                grid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
            }

            // Put children back where they were
            foreach (UIElement child in grid.Children)
            {
                var r = GetOriginalRow(child); if (r != null) Grid.SetRow(child, r.Value);
                var c = GetOriginalColumn(child); if (c != null) Grid.SetColumn(child, c.Value);
            }
        }

        private static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            while (current != null)
            {
                if (current is T t) return t;
                current = VisualTreeHelper.GetParent(current);
            }
            return null;
        }
    }
}
