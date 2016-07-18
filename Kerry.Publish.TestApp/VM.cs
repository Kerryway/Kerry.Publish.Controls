using Kerry.Publish.TestAppoBoxDemo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kerry.Publish.TestApp
{
    public class VM : NotifyPropertyBase
    {
        public VM()
        {
            ItemsSource = new ObservableCollection<Item>();
            for (int i = 0; i < 10; i++)
            {
                ItemsSource.Add(new Item() { Address = "Address" + i, Age = i, Name = "Name" + i });
            }
        }


        private ObservableCollection<Item> _ItemsSource;

        public ObservableCollection<Item> ItemsSource
        {
            get { return _ItemsSource; }
            set
            {
                _ItemsSource = value;
                this.SetProperty(x => x.ItemsSource);
            }
        }
    }

    public class Item : NotifyPropertyBase
    {
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                this.SetProperty(x => x.Name);
            }
        }

        private int _Age;

        public int Age
        {
            get { return _Age; }
            set
            {
                _Age = value;
                this.SetProperty(x => x.Age);
            }
        }

        private string _Address;

        public string Address
        {
            get { return _Address; }
            set
            {
                _Address = value;
                this.SetProperty(x => x.Address);
            }
        }

    }
}