using BigBoxPCPSelector.ViewModel;
using System.Windows.Controls;
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
        }

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
    }
}
