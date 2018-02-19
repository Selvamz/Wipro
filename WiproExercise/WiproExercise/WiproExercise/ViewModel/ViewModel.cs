using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace WiproExercise
{
    public class ViewModel : INotifyPropertyChanged
    {
        private Data source;
        private bool isLoading = true;
        private string refreshingText = "Fetching Data";
        private bool? isAscending = null;

        public ViewModel()
        {
            DataSource = new Data();
            RefreshCommand = new Command(Refresh);
            SortCommand = new Command(Sort);
            GetData();
        }

        private async void GetData()
        {
            //If it has an internet connection, then fetch data from online. Else displays an alert message.
            //Below condition is used to identify whether the device has internet connection or not. This class is
            //available in Xam.Plugin.Connectivity nuget package.
            if(CrossConnectivity.Current.IsConnected)
            {
                //Used HttpClient (from Microsoft.Net.Http nuget package) to download json data.
                var client = new System.Net.Http.HttpClient();
                var response = await client.GetAsync("https://dl.dropboxusercontent.com/s/2iodh4vg0eortkl/facts.json");
                string jsonObject = await response.Content.ReadAsStringAsync();
                if (jsonObject != "")
                {
                    //Converting json object using NewtonSoft.Json package.
                    DataSource = JsonConvert.DeserializeObject<Data>(jsonObject);
                    isAscending = null;
                }
            }
            else
                await App.Current.MainPage.DisplayAlert("Message", "Internet connection required.", "Ok");
            IsLoading = false;
        }

        public Command RefreshCommand { get; set; }

        public Command SortCommand { get; set; }

        public Data DataSource
        {
            get { return source; }
            set
            {
                source = value;
                OnPropertyChanged("DataSource");
            }
        }

        /// <summary>
        /// Determines whether data is in loading or not. It is bound with ActivityIndicator.
        /// </summary>
        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string RefreshingText
        {
            get { return refreshingText; }
            set
            {
                refreshingText = value;
                OnPropertyChanged("RefreshingText");
            }
        }

        private void Refresh()
        {
            //Refreshes the data by downloading again from the provided url when Refresh button is clicked.
            RefreshingText = "Refreshing Data";
            IsLoading = true;
            GetData();
        }

        private void Sort()
        {
            //Sort the items either ascending or descending order.
            if (isAscending == null || !(bool)isAscending)
            {
                var rows = DataSource.Rows.OrderBy(r => r.Title);
                DataSource.Rows = new ObservableCollection<Row>(rows);
                isAscending = true;
            }
            else
            {
                var rows = DataSource.Rows.OrderByDescending(r => r.Title);
                DataSource.Rows = new ObservableCollection<Row>(rows);
                isAscending = false;
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
