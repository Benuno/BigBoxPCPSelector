using BigBoxPCPSelector.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace BigBoxPCPSelector.View
{
    public partial class PlatformSelector : UserControl, IBigBoxThemeElementPlugin
    {
        private readonly PlatformSelectorViewModel platformSelectorViewModel = PlatformSelectorViewModel.Instance();

        public PlatformSelector()
        {
            InitializeComponent();

            DataContext = platformSelectorViewModel;

            Loaded += PlatformSelector_Loaded;
        }

        private void PlatformSelector_Loaded(object sender, RoutedEventArgs e)
        {
            platformSelectorViewModel.SelectedFontSize = SelectedFontSize;
            platformSelectorViewModel.SelectedFontWeight = SelectedFontWeight;
            platformSelectorViewModel.SelectedForegroundBrush = SelectedForegroundBrush;
            platformSelectorViewModel.SelectedBackgroundBrush = SelectedBackgroundBrush;
            platformSelectorViewModel.ItemFontSize = ItemFontSize;
            platformSelectorViewModel.ItemFontWeight = ItemFontWeight;
            platformSelectorViewModel.ItemForegroundBrush = ItemForegroundBrush;
            platformSelectorViewModel.ItemBackgroundBrush = ItemBackgroundBrush;
        }



        #region Dependency Properties
        public static readonly DependencyProperty SelectedFontSizeProperty =
            DependencyProperty.Register("SelectedFontSize", typeof(double), typeof(PlatformSelector), new PropertyMetadata(10.0));

        public static readonly DependencyProperty SelectedFontWeightProperty = 
            DependencyProperty.Register("SelectedFontWeight", typeof(FontWeight), typeof(PlatformSelector), new PropertyMetadata(FontWeights.Bold));

        public static readonly DependencyProperty SelectedForegroundBrushProperty =
            DependencyProperty.Register("SelectedForegroundBrush", typeof(Brush), typeof(PlatformSelector), new PropertyMetadata(Brushes.Transparent));

        public static readonly DependencyProperty SelectedBackgroundBrushProperty =
            DependencyProperty.Register("SelectedBackgroundBrush", typeof(Brush), typeof(PlatformSelector), new PropertyMetadata(Brushes.Transparent));

        public static readonly DependencyProperty ItemFontSizeProperty =
            DependencyProperty.Register("ItemFontSize", typeof(double), typeof(PlatformSelector), new PropertyMetadata(10.0));

        public static readonly DependencyProperty ItemFontWeightProperty =
            DependencyProperty.Register("ItemFontWeight", typeof(FontWeight), typeof(PlatformSelector), new PropertyMetadata(FontWeights.Normal));

        public static readonly DependencyProperty ItemForegroundBrushProperty =
            DependencyProperty.Register("ItemForegroundBrush", typeof(Brush), typeof(PlatformSelector), new PropertyMetadata(Brushes.Transparent));

        public static readonly DependencyProperty ItemBackgroundBrushProperty =
            DependencyProperty.Register("ItemBackgroundBrush", typeof(Brush), typeof(PlatformSelector), new PropertyMetadata(Brushes.Transparent));

        public Brush SelectedBackgroundBrush
        {
            get => (Brush)GetValue(SelectedBackgroundBrushProperty);
            set => SetValue(SelectedBackgroundBrushProperty, value);
        }

        public Brush SelectedForegroundBrush
        {
            get => (Brush)GetValue(SelectedForegroundBrushProperty);
            set => SetValue(SelectedForegroundBrushProperty, value);
        }

        public FontWeight SelectedFontWeight
        {
            get => (FontWeight)GetValue(SelectedFontWeightProperty);
            set => SetValue(SelectedFontWeightProperty, value);
        }

        public double SelectedFontSize
        {
            get => (double)GetValue(SelectedFontSizeProperty);
            set => SetValue(SelectedFontSizeProperty, value);
        }

        public Brush ItemBackgroundBrush
        {
            get => (Brush)GetValue(ItemBackgroundBrushProperty);
            set => SetValue(ItemBackgroundBrushProperty, value);
        }

        public Brush ItemForegroundBrush
        {
            get => (Brush)GetValue(ItemForegroundBrushProperty);
            set => SetValue(ItemForegroundBrushProperty, value);
        }

        public FontWeight ItemFontWeight
        {
            get => (FontWeight)GetValue(ItemFontWeightProperty);
            set => SetValue(ItemFontWeightProperty, value);
        }

        public double ItemFontSize
        {
            get => (double)GetValue(ItemFontSizeProperty);
            set => SetValue(ItemFontSizeProperty, value);
        }
        #endregion

        #region Plugin Behaviors
        public bool OnEnter()
        {
            return platformSelectorViewModel.OnEnter();
        }

        public bool OnEscape()
        {
            return platformSelectorViewModel.OnEscape();
        }

        public bool OnDown(bool held)
        {
            return platformSelectorViewModel.OnDown(held);
        }

        public bool OnUp(bool held)
        {
            return platformSelectorViewModel.OnUp(held);
        }

        public bool OnLeft(bool held)
        {
            return platformSelectorViewModel.OnLeft(held);
        }

        public bool OnPageDown()
        {
            return platformSelectorViewModel.OnPageDown();
        }

        public bool OnPageUp()
        {
            return platformSelectorViewModel.OnPageUp();
        }

        public bool OnRight(bool held)
        {
            return platformSelectorViewModel.OnRight(held);
        }

        public void OnSelectionChanged(FilterType filterType, string filterValue, IPlatform platform, IPlatformCategory category, IPlaylist playlist, IGame game)
        {
            platformSelectorViewModel.OnSelectionChanged(filterType, filterValue, platform, category, playlist, game);
        }
        #endregion
    }
}
