using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace BigBoxPCPSelector.ViewModel
{

    public sealed class PlatformSelectorViewModel
    {
        public ObservableCollection<String> ActiveList { get; }
        public bool WheelIsActive { get; }
        public List<String> originalList { get; set; }
        private PlatformSelectorViewModel()
        {
            ActiveList = new ObservableCollection<String>(new List<String>{""});
            originalList = new List<string>();
            WheelIsActive = true;
        }

        internal void CycleLeft()
        {
            if(ActiveList.Count > 1)
            {
                String lastPlatform = ActiveList[ActiveList.Count - 1];
                ActiveList.Insert(0, lastPlatform);
                ActiveList.RemoveAt(ActiveList.Count - 1);
            }
               

        }

        internal void CycleRight()
        {
            if (ActiveList.Count > 1)
            {
                String firstPlatform = ActiveList[0];
                ActiveList.Add(firstPlatform);
                ActiveList.RemoveAt(0);
            }

        }

        internal void ShowPlatform(String startName)
        {
            String firstPlatform = ActiveList[0];
            if (ActiveList.Count > 0)// && !startName.Equals(firstPlatform))
            { 
                PluginHelper.BigBoxMainViewModel.ShowGames(FilterType.PlatformOrCategoryOrPlaylist, firstPlatform);
            }
                
        }

        //internal void setWheelActive(bool isActive)
        //{
        //    WheelIsActive = isActive;
        //}

        internal void setActiveList(List<String> names)
        {
            ActiveList.Clear();
            foreach (string name in names)
            {
                ActiveList.Add(name);
            }   
            originalList = names;
        }

        internal void escapeKey()
        {   
            if(originalList.Count > 0)
            {
                if (!PluginHelper.StateManager.GetSelectedPlatform().Name.Equals(originalList[0]))
                {
                    PluginHelper.BigBoxMainViewModel.ShowGames(FilterType.PlatformOrCategoryOrPlaylist, originalList[0]);
                }
                ActiveList.Clear();
                foreach (string name in originalList)
                {
                    ActiveList.Add(name);
                }                
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
