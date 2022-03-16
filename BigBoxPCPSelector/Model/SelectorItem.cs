using System.ComponentModel;

namespace BigBoxPCPSelector.Model
{
    public class SelectorItem : INotifyPropertyChanged
    {
        private string itemType;
        public string ItemType
        {
            get { return itemType; }
            set
            {
                itemType = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ItemType"));
            }
        }

        private string itemDescription;
        public string ItemDescription
        {
            get { return itemDescription; }
            set
            {
                itemDescription = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ItemDescription"));
            }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsSelected"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
