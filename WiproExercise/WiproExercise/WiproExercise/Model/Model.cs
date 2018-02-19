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
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageHref { get; set; }
    }

    public class Data : INotifyPropertyChanged
    {
        private string title;
        private ObservableCollection<Row> rows;

        public Data()
        {
            rows = new ObservableCollection<Row>();
        }

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        public ObservableCollection<Row> Rows
        {
            get { return rows; }
            set
            {
                rows = value;
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
