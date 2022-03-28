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
        public List<SelectorItem> OriginalPlatforms { get; }
        public List<SelectorItem> OriginalPlaylists { get; }
        public bool switchPlatform = false;
        public bool wheelActivateLock = true;
        public string startName = "";

        private PlatformSelectorViewModel()
        {
            ActiveList = new ObservableCollection<SelectorItem>();
            OriginalList = new List<SelectorItem>();

            //Playlist initialize
            IList<IPlaylist> childspl = PluginHelper.DataManager.GetAllPlaylists();

            OriginalPlaylists = new List<SelectorItem>();

            foreach (IPlaylist child in childspl)
            {
                OriginalPlaylists.Add(new SelectorItem()
                {
                    IsSelected = false,
                    ItemDescription = child.Name,
                    ItemType = SelectorItemType.Playlist
                });
            }

            var itemQuery = from item in OriginalPlaylists
                            orderby item.ItemDescription
                            select item;

            OriginalPlaylists = itemQuery.ToList();



            //platforms initialize
            IList<IPlatform> childs = PluginHelper.DataManager.GetAllPlatforms();

            OriginalPlatforms = new List<SelectorItem>();
            foreach (IPlatform child in childs)
            {
                OriginalPlatforms.Add(new SelectorItem()
                {
                    IsSelected = false,
                    ItemDescription = child.Name,
                    ItemType = SelectorItemType.Platform
                });
            }

            var itemQueryPr = from item in OriginalPlatforms
                            orderby item.ItemDescription
                            select item;

            OriginalPlatforms = itemQueryPr.ToList();

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
                return true;
            }
            return false;
        }

        internal bool OnUp(bool held)
        {
            if (switchPlatform)
            {
                wheelActivateLock = true;
                switchPlatform = false;
                setItemSelection(0, false);
                ResetSelectorPosition();
                if(ActiveList[0].ItemType == SelectorItemType.Platform)
                {                    
                    ShowPlatform(0, OriginalPlaylists);
                }
                else
                {
                    ShowPlatform(0, OriginalPlatforms);
                }
                return true;
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
            if (!wheelActivateLock)
            {
                wheelActivateLock = true;
                if (switchPlatform)
                {
                    switchPlatform = false;
                    setItemSelection(0, false);
                }
                ShowPlatform(0,OriginalPlaylists);
                return true;
            }
            return false;
        }
        
        internal bool OnPageUp()
        {
            if (!wheelActivateLock)
            {
                wheelActivateLock = true;
                if (switchPlatform)
                {
                    switchPlatform = false;
                    setItemSelection(0, false);
                }
                ShowPlatform(0, OriginalPlatforms);
                return true;
            }
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
                    List<SelectorItem> outputPCP = new List<SelectorItem>();
                    if (playlist != null)
                    {
                        if (playlist.Name.Equals(startName)) // otherwise performance penalty due to constant list recreation while holding down any direction key
                        {
                            return;
                        }
                        startName = playlist.Name;

                        outputPCP = OriginalPlaylists;

                    } else {

                        if (platform.Name.Equals(startName))
                        {
                            return;
                        }  
                        startName = platform.Name;

                        outputPCP = OriginalPlatforms;
                        
                    }
                    SelectorItem foundEntry = outputPCP.FirstOrDefault(item => item.ItemDescription == startName);

                    if (foundEntry != null && outputPCP.Count > 1)
                    {
                        int entryIndex = outputPCP.IndexOf(foundEntry);
                        outputPCP.AddRange(outputPCP.GetRange(0, entryIndex));
                        outputPCP.RemoveRange(0, entryIndex);
                    }

                    SetActiveList(outputPCP);
                }
            }
                
            catch(Exception ex)
            {
                LogHelper.LogException(ex, "OnSelectionChanged");
            }
            if (wheelActivateLock)
            {
                wheelActivateLock = false;
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
                wheelActivateLock = false;
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
                    ShowPlatform(0,ActiveList.ToList());
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

        internal void ShowPlatform(int index, List<SelectorItem> list)
        {
            SelectorItem firstItem = list[index];
            if (list.Count > 0 && !OriginalList[0].ItemDescription.Equals(firstItem.ItemDescription))
            {
                PluginHelper.BigBoxMainViewModel.ShowGames(FilterType.PlatformOrCategoryOrPlaylist, firstItem.ItemDescription);
            } else
            {
                wheelActivateLock = false;
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


        #region Dependency Properties
        private int numberToDisplay;
        public int NumberToDisplay
        {
            get => numberToDisplay;
            set
            {
                numberToDisplay = value;
                PropertyChanged(this, new PropertyChangedEventArgs("NumberToDisplay"));
            }
        }

        private FontFamily itemFontFamily;
        public FontFamily ItemFontFamily
        {
            get => itemFontFamily;
            set
            {
                itemFontFamily = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ItemFontFamily"));
            }
        }

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

        private Brush itemBorderBrush;
        public Brush ItemBorderBrush
        {
            get => itemBorderBrush;
            set
            {
                itemBorderBrush = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ItemBorderBrush"));
            }
        }

        private Thickness itemBorderThickness;
        public Thickness ItemBorderThickness
        {
            get => itemBorderThickness;
            set
            {
                itemBorderThickness = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ItemBorderThickness"));
            }
        }

        private Thickness itemBorderMargin;
        public Thickness ItemBorderMargin
        {
            get => itemBorderMargin;
            set
            {
                itemBorderMargin = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ItemBorderMargin"));
            }
        }

        private Thickness itemMargin;
        public Thickness ItemMargin
        {
            get => itemMargin;
            set
            {
                itemMargin = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ItemMargin"));
            }
        }


        private Brush itemBorderBackgroundBrush;
        public Brush ItemBorderBackgroundBrush
        {
            get => itemBorderBackgroundBrush;
            set
            {
                itemBorderBackgroundBrush = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ItemBorderBackgroundBrush"));
            }
        }

        private CornerRadius itemBorderCornerRadius;
        public CornerRadius ItemBorderCornerRadius
        {
            get => itemBorderCornerRadius;
            set
            {
                itemBorderCornerRadius = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ItemBorderCornerRadius"));
            }
        }


        private FontFamily selectedFontFamily;
        public FontFamily SelectedFontFamily
        {
            get => selectedFontFamily;
            set
            {
                selectedFontFamily = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedFontFamily"));
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

        private Brush selectedBorderBrush;
        public Brush SelectedBorderBrush
        {
            get => selectedBorderBrush;
            set
            {
                selectedBorderBrush = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedBorderBrush"));
            }
        }

        private Brush selectedBorderBackgroundBrush;
        public Brush SelectedBorderBackgroundBrush
        {
            get => selectedBorderBackgroundBrush;
            set
            {
                selectedBorderBackgroundBrush = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedBorderBackgroundBrush"));
            }
        }

        private Thickness selectedBorderThickness;
        public Thickness SelectedBorderThickness
        {
            get => selectedBorderThickness;
            set
            {
                selectedBorderThickness = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedBorderThickness"));
            }
        }

        private CornerRadius selectedCornerRadius;
        public CornerRadius SelectedCornerRadius
        {
            get => selectedCornerRadius;
            set
            {
                selectedCornerRadius = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedCornerRadius"));
            }
        }

        private Thickness selectedBorderMargin;
        public Thickness SelectedBorderMargin
        {
            get => selectedBorderMargin;
            set
            {
                selectedBorderMargin = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedBorderMargin"));
            }
        }

        private Thickness selectedMargin;
        public Thickness SelectedMargin
        {
            get => selectedMargin;
            set
            {
                selectedMargin = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedMargin"));
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
