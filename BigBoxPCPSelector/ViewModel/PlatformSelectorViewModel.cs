using BigBoxPCPSelector.Helper;
using BigBoxPCPSelector.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace BigBoxPCPSelector.ViewModel
{
    public sealed class PlatformSelectorViewModel
    {
        public ObservableCollection<SelectorItem> ActiveList { get; }
        public bool WheelIsActive { get; }
        public List<SelectorItem> OriginalList { get; set; }
        public bool switchPlatform = false;
        public bool wheelActivateLock = true;
        public string startName = "";

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
            if (switchPlatform)
            {
                switchPlatform = false;
                setItemSelection(0, false);
                ResetSelectorPosition();
            }
            return false;
        }

        internal bool OnUp(bool held)
        {
            if (switchPlatform)
            {
                switchPlatform = false;
                setItemSelection(0, false);
                ResetSelectorPosition();
            }
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
        
        internal bool OnPageUp()
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
                if (switchPlatform == false) //necessary as pressing direction keys quickly after opening the plugin wheel triggers onselectionchanged for a game title.
                {
                    if (wheelActivateLock)
                    {
                        wheelActivateLock = false;
                    }
                    if (playlist != null)
                    {
                        if (playlist.Name.Equals(startName)) // otherwise performance penalty due to constant list recreation while holding down any direction key
                        {
                            return;
                        }
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

                        if (platform.Name.Equals(startName))
                        {
                            return;
                        }  
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
            }
                
            catch(Exception ex)
            {
                LogHelper.LogException(ex, "OnSelectionChanged");
            }
        }

        internal bool OnEscape()
        {
            if (switchPlatform)
            {
                switchPlatform = false;
                setItemSelection(0, false);
                ResetSelectorPosition();
                return false;
            }
            else
            {
                if (!wheelActivateLock)
                {
                    switchPlatform = true;
                    setItemSelection(0, true);
                    return true;
                }
                return false;
            }
        }

        internal bool OnEnter()
        {
            if (switchPlatform)
            {
                switchPlatform = false;
                if (!startName.Equals(ActiveList[0].ItemDescription))
                {
                    wheelActivateLock = true;
                    ShowPlatform(startName);
                    OriginalList = new List<SelectorItem>();
                }
                setItemSelection(0, false);
                return true;
            }
            else
            {
                wheelActivateLock = true; //necessary or user unable to see wheel active when entering game detail and quickly pressing escape, makes quick exit possible
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

        internal void ResetSelectorPosition()
        {
            try
            {
                LogHelper.Log($"ResetSelectorPosition: {OriginalList?.Count ?? 0}");

                if (OriginalList.Count > 0)
                {
                    ActiveList.Clear();
                    foreach (SelectorItem item in OriginalList)
                    {
                        ActiveList.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex, "ResetSelectorPosition");
            }
        }

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
    }
}
