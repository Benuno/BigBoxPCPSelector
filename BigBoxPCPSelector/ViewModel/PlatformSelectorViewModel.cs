using BigBoxPCPSelector.Helper;
using BigBoxPCPSelector.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace BigBoxPCPSelector.ViewModel
{
    public sealed class PlatformSelectorViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<SelectorItem> ActiveList { get; }
        public bool WheelIsActive { get; }
        public List<SelectorItem> OriginalList { get; set; }
        public bool switchPlatform = false;
        public string startName = null;

        private PlatformSelectorViewModel()
        {
            ActiveList = new ObservableCollection<SelectorItem>();
            OriginalList = new List<SelectorItem>();
            WheelIsActive = true;
        }

        internal void CycleLeft()
        {
            if (ActiveList.Count > 1)
            {
                setItemSelection(0, false);

                SelectorItem lastItem = ActiveList[ActiveList.Count - 1];
                ActiveList.Insert(0, lastItem);
                ActiveList.RemoveAt(ActiveList.Count - 1);

                setItemSelection(0, true);
            }
        }


        internal bool OnDown(bool held)
        {
            switchPlatform = false;
            return false;
        }

        internal bool OnUp(bool held)
        {
            switchPlatform = false;
            return false;
        }

        internal bool OnLeft(bool held)
        {
            if (switchPlatform)
            {
                CycleLeft();
                return true;
            }
            return false;
        }

        internal bool OnPageDown()
        {
            return false;
        }

        internal bool OnRight(bool held)
        {
            if (switchPlatform)
            {
                CycleRight();
                return true;
            }
            return false;
        }

        internal void OnSelectionChanged(FilterType filterType, string filterValue, IPlatform platform, IPlatformCategory category, IPlaylist playlist, IGame game)
        {
            try
            {
                if (playlist != null)
                {
                    startName = playlist.Name;

                    IList<IPlaylist> childs = PluginHelper.DataManager.GetAllPlaylists();

                    List<SelectorItem> items = new List<SelectorItem>();

                    foreach (IPlaylist child in childs)
                    {
                        items.Add(new SelectorItem()
                        {
                            IsSelected = false,
                            ItemDescription = child.Name,
                            ItemType = SelectorItemType.Playlist
                        });
                    }

                    var itemQuery = from item in items
                                    orderby item.ItemDescription
                                    select item;

                    items = itemQuery.ToList();

                    SelectorItem foundEntry = items.FirstOrDefault(item => item.ItemDescription == startName);

                    if (foundEntry != null && items.Count > 1)
                    {
                        int entryIndex = items.IndexOf(foundEntry);

                        List<SelectorItem> addToEnd = items.GetRange(0, entryIndex);

                        items.AddRange(addToEnd);
                        items.RemoveRange(0, entryIndex);
                    }

                    SetActiveList(items);
                }
                else
                {
                    startName = platform.Name;
                    IList<IPlatform> childs = PluginHelper.DataManager.GetAllPlatforms();

                    List<SelectorItem> items = new List<SelectorItem>();
                    foreach (IPlatform child in childs)
                    {
                        items.Add(new SelectorItem()
                        {
                            IsSelected = false,
                            ItemDescription = child.Name,
                            ItemType = SelectorItemType.Platform
                        });
                    }

                    var itemQuery = from item in items
                                    orderby item.ItemDescription
                                    select item;

                    items = itemQuery.ToList();

                    SelectorItem foundEntry = items.FirstOrDefault(item => item.ItemDescription == startName);

                    if (foundEntry != null && items.Count > 1)
                    {
                        int entryIndex = items.IndexOf(foundEntry);

                        List<SelectorItem> addToEnd = items.GetRange(0, entryIndex);

                        items.AddRange(addToEnd);
                        items.RemoveRange(0, entryIndex);
                    }

                    SetActiveList(items);
                }
            }
            catch(Exception ex)
            {
                LogHelper.LogException(ex, "OnSelectionChanged");
            }
        }

        internal bool OnPageUp()
        {
            return false;
        }

        internal bool OnEscape()
        {
            if (switchPlatform)
            {
                switchPlatform = false;
                EscapeKey();
                return false;
            }
            else
            {
                switchPlatform = true;
                setItemSelection(0, true);
                return true;
            }
        }

        internal bool OnEnter()
        {
            if (switchPlatform)
            {
                switchPlatform = false;
                ShowPlatform(startName);
                return true;
            }
            else
            {
                return false;
            }
        }

        internal void CycleRight()
        {
            if (ActiveList.Count > 1)
            {
                setItemSelection(0, false);

                SelectorItem firstItem = ActiveList[0];
                ActiveList.Add(firstItem);
                ActiveList.RemoveAt(0);

                setItemSelection(0, true);
            }
        }

        private void setItemSelection(int index, bool selected)
        {
            SelectorItem item = ActiveList[index];
            item.IsSelected = selected;
        }

        internal void ShowPlatform(string startName)
        {
            SelectorItem firstItem = ActiveList[0];
            if (ActiveList.Count > 0)
            {
                PluginHelper.BigBoxMainViewModel.ShowGames(FilterType.PlatformOrCategoryOrPlaylist, firstItem.ItemDescription);
            }
        }

        internal void SetActiveList(List<SelectorItem> items)
        {
            ActiveList.Clear();
            foreach (SelectorItem item in items)
            {
                ActiveList.Add(item);
            }
            OriginalList = items;
        }

        internal void EscapeKey()
        {
            try
            {
                LogHelper.Log($"EscapeKey: {OriginalList?.Count ?? 0}");

                if (OriginalList.Count > 0)
                {
                    if (!PluginHelper.StateManager.GetSelectedPlatform().Name.Equals(OriginalList[0]))
                    {
                        PluginHelper.BigBoxMainViewModel.ShowGames(FilterType.PlatformOrCategoryOrPlaylist, OriginalList[0].ItemDescription);
                    }
                    ActiveList.Clear();
                    foreach (SelectorItem item in OriginalList)
                    {
                        ActiveList.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "EscapeKey");
            }
        }


        #region Dependency Properties
        private double itemFontSize;
        public double ItemFontSize
        {
            get => itemFontSize;
            set
            {
                itemFontSize = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ItemFontSize"));
            }
        }

        private FontWeight itemFontWeight;
        public FontWeight ItemFontWeight
        {
            get => itemFontWeight;
            set
            {
                itemFontWeight = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ItemFontWeight"));
            }
        }

        private Brush itemBackgroundBrush;
        public Brush ItemBackgroundBrush
        {
            get => itemBackgroundBrush;
            set
            {
                itemBackgroundBrush = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ItemBackgroundBrush"));
            }
        }

        private Brush itemForegroundBrush;
        public Brush ItemForegroundBrush
        {
            get => itemForegroundBrush;
            set
            {
                itemForegroundBrush = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ItemForegroundBrush"));
            }
        }

        private double selectedFontSize;
        public double SelectedFontSize
        {
            get => selectedFontSize;
            set
            {
                selectedFontSize = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedFontSize"));
            }
        }

        private FontWeight selectedFontWeight;
        public FontWeight SelectedFontWeight
        {
            get => selectedFontWeight;
            set
            {
                selectedFontWeight = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedFontWeight"));
            }
        }

        private Brush selectedBackgroundBrush;
        public Brush SelectedBackgroundBrush
        {
            get => selectedBackgroundBrush;
            set
            {
                selectedBackgroundBrush = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedBackgroundBrush"));
            }
        }

        private Brush selectedForegroundBrush;
        public Brush SelectedForegroundBrush
        {
            get => selectedForegroundBrush;
            set
            {
                selectedForegroundBrush = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedForegroundBrush"));
            }
        }
        #endregion

        //single instance across all views.
        #region singleton implementation 
        private static PlatformSelectorViewModel _instance;
        public static PlatformSelectorViewModel Instance()
        {
            if (_instance == null)
            {
                _instance = new PlatformSelectorViewModel();
            }
            return _instance;
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
