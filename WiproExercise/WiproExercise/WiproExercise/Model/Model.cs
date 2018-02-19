using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiproExercise
{
    public class Row
    {
        string description;

        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageHref { get; set; }
    }

    public class Data : INotifyPropertyChanged
    {
        private string heading;
        private ObservableCollection<Row> items;

        public Data()
        {
            items = new ObservableCollection<Row>();
        }

        public string Title
        {
            get { return heading; }
            set
            {
                heading = value;
                OnPropertyChanged("Title");
            }
        }

        public ObservableCollection<Row> Rows
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged("Rows");
            }
        }

        #region INotifyPropertyChanged

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }

}
