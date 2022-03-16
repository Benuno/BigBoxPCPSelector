using BigBoxPCPSelector.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace BigBoxPCPSelector.View
{
    /// <summary>
    /// Interaction logic for PlatformSelector.xaml
    /// </summary>
    public partial class PlatformSelector : UserControl, IBigBoxThemeElementPlugin
    {
        public bool switchPlatform = false;
        public bool categoryLock = false;
        public string startName = null;
        public string lastCategory = null;
        public bool gameDetails = false;
        public string key = null;

        public PlatformSelector()
        {
            InitializeComponent();
            DataContext = PlatformSelectorViewModel.Instance();
            
        }

        public bool OnEnter()
        {
            this.key = "enter";
            if (this.switchPlatform)
            {
                this.switchPlatform = false;
                PlatformSelectorViewModel.Instance().ShowPlatform(this.startName);
                return true;
            }
            else
            {                
                return false;

            }
        }

        public bool OnEscape()
        {
            this.key = "escape";
            if (this.switchPlatform && this.gameDetails==false) //&& !this.UseType.Equals("False"))
            {
                this.switchPlatform = false;
                PlatformSelectorViewModel.Instance().escapeKey();
                return false;
            }
            else
            {
                if (this.switchPlatform == false && this.gameDetails==false)
                {
                    this.switchPlatform = true;
                    return true;
                }
                return false;
            }
        }

        public bool OnDown(bool held)
        {
            this.switchPlatform = false;
            this.key = "down";
            return false;
        }

        public bool OnUp(bool held)
        {
            this.key = "up";
            this.switchPlatform = false;
            return false;
        }
        public bool OnLeft(bool held)
        {
            this.key = "left";
            if (this.switchPlatform)
            {
                PlatformSelectorViewModel.Instance().CycleLeft();
                
                
                return true;
            }
            return false;
            
        }

        public bool OnPageDown()
        {
            this.key = "pageDown";
            return false;
        }

        public bool OnPageUp()
        {
            this.key = "pageUp";
            return false;
        }

        public bool OnRight(bool held)
        {
            if (this.switchPlatform)
            {
                PlatformSelectorViewModel.Instance().CycleRight();
               
                return true;
            }
            return false;
        }

        public void OnSelectionChanged(FilterType filterType, string filterValue, IPlatform platform, IPlatformCategory category, IPlaylist playlist, IGame game)
        {
                if (playlist != null)
                {
                    startName = playlist.Name;
                
                    IList<IPlaylist> childs = PluginHelper.DataManager.GetAllPlaylists();

                    List<String> names = new List<String>();
                    foreach (IPlaylist child in childs)
                    {
                        names.Add(child.Name);
                    }
                    names.Sort();

                    var foundEntry = names.Where(X => X == startName).FirstOrDefault();

                    if (foundEntry != null && names.Count > 1)
                    {
                        int entryIndex = names.IndexOf(foundEntry);
                        List<String> addToEnd = names.GetRange(0, entryIndex);
                        names.AddRange(addToEnd);
                        names.RemoveRange(0, entryIndex);
                        
                    }

                    PlatformSelectorViewModel.Instance().setActiveList(names);
                }
                else
                {
                    startName = platform.Name;
                    IList<IPlatform> childs = PluginHelper.DataManager.GetAllPlatforms();

                    List<String> names = new List<String>();
                    foreach (IPlatform child in childs)
                    {
                        names.Add(child.Name);
                    }
                    names.Sort();

                    var foundEntry = names.Where(X => X == startName).FirstOrDefault();

                    if (foundEntry != null && names.Count > 1)
                    {
                        int entryIndex = names.IndexOf(foundEntry);
                    List<String> addToEnd = names.GetRange(0, entryIndex);
                    names.AddRange(addToEnd);
                    names.RemoveRange(0, entryIndex);
                }

                    PlatformSelectorViewModel.Instance().setActiveList(names);
                }
        }
      
    }
}
