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
            platformSelectorViewModel.NumberToDisplay = NumberToDisplay;

            platformSelectorViewModel.SelectedFontSize = SelectedFontSize;
            platformSelectorViewModel.SelectedFontWeight = SelectedFontWeight;
            platformSelectorViewModel.SelectedForegroundBrush = SelectedForegroundBrush;
            platformSelectorViewModel.SelectedBackgroundBrush = SelectedBackgroundBrush;
            platformSelectorViewModel.SelectedBorderBrush = SelectedBorderBrush;
            platformSelectorViewModel.SelectedBorderBackgroundBrush = SelectedBorderBackgroundBrush;
            platformSelectorViewModel.SelectedBorderThickness = SelectedBorderThickness;
            platformSelectorViewModel.SelectedCornerRadius = SelectedBorderCornerRadius;
            platformSelectorViewModel.SelectedBorderMargin = SelectedBorderMargin;
            platformSelectorViewModel.SelectedMargin = SelectedMargin;

            platformSelectorViewModel.ItemFontSize = ItemFontSize;
            platformSelectorViewModel.ItemFontWeight = ItemFontWeight;
            platformSelectorViewModel.ItemForegroundBrush = ItemForegroundBrush;
            platformSelectorViewModel.ItemBackgroundBrush = ItemBackgroundBrush;
            platformSelectorViewModel.ItemBorderBrush = ItemBorderBrush;
            platformSelectorViewModel.ItemBorderBackgroundBrush = ItemBorderBackgroundBrush;
            platformSelectorViewModel.ItemBorderThickness = ItemBorderThickness;
            platformSelectorViewModel.ItemBorderCornerRadius = ItemBorderCornerRadius;
            platformSelectorViewModel.ItemBorderMargin = ItemBorderMargin;
            platformSelectorViewModel.ItemMargin = ItemMargin;
        }



        #region Dependency Properties

        public static readonly DependencyProperty NumberToDisplayProperty = DependencyProperty.Register("NumberToDisplay", typeof(int), typeof(PlatformSelector), new PropertyMetadata(5));
        public int NumberToDisplay
        {
            get => (int)GetValue(NumberToDisplayProperty);
            set => SetValue(NumberToDisplayProperty, value);
        }

        public static readonly DependencyProperty SelectedFontFamilyProperty = DependencyProperty.Register("SelectedFontFamily", typeof(FontFamily), typeof(PlatformSelector), new FrameworkPropertyMetadata(TextBlock.FontFamilyProperty.DefaultMetadata.DefaultValue, FrameworkPropertyMetadataOptions.AffectsRender));
        public FontFamily SelectedFontFamily
        {
            get => (FontFamily)GetValue(SelectedFontFamilyProperty);
            set => SetValue(SelectedFontFamilyProperty, value);
        }

        public static readonly DependencyProperty SelectedFontSizeProperty = DependencyProperty.Register("SelectedFontSize", typeof(double), typeof(PlatformSelector), new PropertyMetadata(10.0));
        public double SelectedFontSize
        {
            get => (double)GetValue(SelectedFontSizeProperty);
            set => SetValue(SelectedFontSizeProperty, value);
        }

        public static readonly DependencyProperty SelectedFontWeightProperty = DependencyProperty.Register("SelectedFontWeight", typeof(FontWeight), typeof(PlatformSelector), new PropertyMetadata(FontWeights.Bold));
        public FontWeight SelectedFontWeight
        {
            get => (FontWeight)GetValue(SelectedFontWeightProperty);
            set => SetValue(SelectedFontWeightProperty, value);
        }

        public static readonly DependencyProperty SelectedForegroundBrushProperty = DependencyProperty.Register("SelectedForegroundBrush", typeof(Brush), typeof(PlatformSelector), new PropertyMetadata(Brushes.Transparent));
        public Brush SelectedForegroundBrush
        {
            get => (Brush)GetValue(SelectedForegroundBrushProperty);
            set => SetValue(SelectedForegroundBrushProperty, value);
        }

        public static readonly DependencyProperty SelectedBackgroundBrushProperty = DependencyProperty.Register("SelectedBackgroundBrush", typeof(Brush), typeof(PlatformSelector), new PropertyMetadata(Brushes.Transparent));
        public Brush SelectedBackgroundBrush
        {
            get => (Brush)GetValue(SelectedBackgroundBrushProperty);
            set => SetValue(SelectedBackgroundBrushProperty, value);
        }

        public static readonly DependencyProperty SelectedBorderThicknessProperty = DependencyProperty.Register("SelectedBorderThickness", typeof(Thickness), typeof(PlatformSelector), new PropertyMetadata(default(Thickness)));
        public Thickness SelectedBorderThickness
        {
            get => (Thickness)GetValue(SelectedBorderThicknessProperty);
            set => SetValue(SelectedBorderThicknessProperty, value);
        }

        public static readonly DependencyProperty SelectedBorderBackgroundBrushProperty = DependencyProperty.Register("SelectedBorderBackgroundBrush", typeof(Brush), typeof(PlatformSelector), new PropertyMetadata(Brushes.Transparent));
        public Brush SelectedBorderBackgroundBrush
        {
            get => (Brush)GetValue(SelectedBorderBackgroundBrushProperty);
            set => SetValue(SelectedBorderBackgroundBrushProperty, value);
        }

        public static readonly DependencyProperty SelectedBorderBrushProperty = DependencyProperty.Register("SelectedBorderBrush", typeof(Brush), typeof(PlatformSelector), new PropertyMetadata(Brushes.Transparent));
        public Brush SelectedBorderBrush
        {
            get => (Brush)GetValue(SelectedBorderBrushProperty);
            set => SetValue(SelectedBorderBrushProperty, value);
        }

        public static readonly DependencyProperty SelectedBorderCornerRadiusProperty = DependencyProperty.Register("SelectedBorderCornerRadius", typeof(CornerRadius), typeof(PlatformSelector), new PropertyMetadata(default(CornerRadius)));
        public CornerRadius SelectedBorderCornerRadius
        {
            get => (CornerRadius)GetValue(SelectedBorderCornerRadiusProperty);
            set => SetValue(SelectedBorderCornerRadiusProperty, value);
        }

        public static readonly DependencyProperty SelectedBorderMarginProperty = DependencyProperty.Register("SelectedBorderMargin", typeof(Thickness), typeof(PlatformSelector), new PropertyMetadata(default(Thickness)));
        public Thickness SelectedBorderMargin
        {
            get => (Thickness)GetValue(SelectedBorderMarginProperty);
            set => SetValue(SelectedBorderMarginProperty, value);
        }

        public static readonly DependencyProperty SelectedMarginProperty = DependencyProperty.Register("SelectedMargin", typeof(Thickness), typeof(PlatformSelector), new PropertyMetadata(default(Thickness)));
        public Thickness SelectedMargin
        {
            get => (Thickness)GetValue(SelectedMarginProperty);
            set => SetValue(SelectedMarginProperty, value);
        }


        public static readonly DependencyProperty ItemFontFamilyProperty = DependencyProperty.Register("ItemFontFamily", typeof(FontFamily), typeof(PlatformSelector), new FrameworkPropertyMetadata(TextBlock.FontFamilyProperty.DefaultMetadata.DefaultValue, FrameworkPropertyMetadataOptions.AffectsRender));
        public FontFamily ItemFontFamily
        {
            get => (FontFamily)GetValue(ItemFontFamilyProperty);
            set => SetValue(ItemFontFamilyProperty, value);
        }

        public static readonly DependencyProperty ItemFontSizeProperty = DependencyProperty.Register("ItemFontSize", typeof(double), typeof(PlatformSelector), new PropertyMetadata(10.0));
        public double ItemFontSize
        {
            get => (double)GetValue(ItemFontSizeProperty);
            set => SetValue(ItemFontSizeProperty, value);
        }

        public static readonly DependencyProperty ItemFontWeightProperty = DependencyProperty.Register("ItemFontWeight", typeof(FontWeight), typeof(PlatformSelector), new PropertyMetadata(FontWeights.Normal));
        public FontWeight ItemFontWeight
        {
            get => (FontWeight)GetValue(ItemFontWeightProperty);
            set => SetValue(ItemFontWeightProperty, value);
        }

        public static readonly DependencyProperty ItemForegroundBrushProperty = DependencyProperty.Register("ItemForegroundBrush", typeof(Brush), typeof(PlatformSelector), new PropertyMetadata(Brushes.Transparent));
        public Brush ItemForegroundBrush
        {
            get => (Brush)GetValue(ItemForegroundBrushProperty);
            set => SetValue(ItemForegroundBrushProperty, value);
        }

        public static readonly DependencyProperty ItemBackgroundBrushProperty = DependencyProperty.Register("ItemBackgroundBrush", typeof(Brush), typeof(PlatformSelector), new PropertyMetadata(Brushes.Transparent));
        public Brush ItemBackgroundBrush
        {
            get => (Brush)GetValue(ItemBackgroundBrushProperty);
            set => SetValue(ItemBackgroundBrushProperty, value);
        }

        public static readonly DependencyProperty ItemBorderBrushProperty = DependencyProperty.Register("ItemBorderBrush", typeof(Brush), typeof(PlatformSelector), new PropertyMetadata(Brushes.Transparent));
        public Brush ItemBorderBrush
        {
            get => (Brush)GetValue(ItemBorderBrushProperty);
            set => SetValue(ItemBorderBrushProperty, value);
        }

        public static readonly DependencyProperty ItemBorderBackgroundBrushProperty = DependencyProperty.Register("ItemBorderBackgroundBrush", typeof(Brush), typeof(PlatformSelector), new PropertyMetadata(Brushes.Transparent));
        public Brush ItemBorderBackgroundBrush
        {
            get => (Brush)GetValue(ItemBorderBackgroundBrushProperty);
            set => SetValue(ItemBorderBackgroundBrushProperty, value);
        }

        public static readonly DependencyProperty ItemBorderThicknessProperty = DependencyProperty.Register("ItemBorderThickness", typeof(Thickness), typeof(PlatformSelector), new PropertyMetadata(default(Thickness)));
        public Thickness ItemBorderThickness
        {
            get => (Thickness)GetValue(ItemBorderThicknessProperty);
            set => SetValue(ItemBorderThicknessProperty, value);
        }

        public static readonly DependencyProperty ItemBorderCornerRadiusProperty = DependencyProperty.Register("ItemBorderCornerRadius", typeof(CornerRadius), typeof(PlatformSelector), new PropertyMetadata(default(CornerRadius)));
        public CornerRadius ItemBorderCornerRadius
        {
            get => (CornerRadius)GetValue(ItemBorderCornerRadiusProperty);
            set => SetValue(ItemBorderCornerRadiusProperty, value);
        }

        public static readonly DependencyProperty ItemBorderMarginProperty = DependencyProperty.Register("ItemBorderMargin", typeof(Thickness), typeof(PlatformSelector), new PropertyMetadata(default(Thickness)));
        public Thickness ItemBorderMargin
        {
            get => (Thickness)GetValue(ItemBorderMarginProperty);
            set => SetValue(ItemBorderMarginProperty, value);
        }

        public static readonly DependencyProperty ItemMarginProperty = DependencyProperty.Register("ItemMargin", typeof(Thickness), typeof(PlatformSelector), new PropertyMetadata(default(Thickness)));
        public Thickness ItemMargin
        {
            get => (Thickness)GetValue(ItemMarginProperty);
            set => SetValue(ItemMarginProperty, value);
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