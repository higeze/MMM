using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace HiLib.FilterDataGrid
{

    public class Order : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                NotifyPropertyChanged("Name");
            }
        }

        private string orderCount;

        public string ItemCount
        {
            get
            {
                return orderCount;
            }
            set
            {
                orderCount = value;
                NotifyPropertyChanged("ItemCount");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raise the PropertyChanged event
        /// </summary>
        /// <param name="propertyName">The name of the property that has changed</param>
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public Order()
        {

        }
    }
}
