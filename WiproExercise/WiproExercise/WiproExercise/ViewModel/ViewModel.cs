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
        private const string url = "https://dl.dropboxusercontent.com/s/2iodh4vg0eortkl/facts.json";

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
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    //Used HttpClient (from Microsoft.Net.Http nuget package) to download json data.
                    var client = new System.Net.Http.HttpClient();
                    var response = await client.GetAsync(url);
                    string jsonObject = await response.Content.ReadAsStringAsync();
                    if (jsonObject != "")
                    {
                        //Converting json object using NewtonSoft.Json package.
                        var source = JsonConvert.DeserializeObject<Data>(jsonObject);

                        //Below code will ignore the null items from the collection
                        var rows = source.Rows.Where(a => a != null && (a.Title != null || a.Description != null || a.ImageHref != null));
                        source.Rows = new ObservableCollection<Row>(rows);
                        if (IsInitialLoading)
                            DataSource = source;
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                DataSource = source;
                                await Application.Current.MainPage.DisplayAlert("Message", "Data refreshed.", "Ok");
                            });
                        }
                        isAscending = null;
                        IsInitialLoading = false;
                    }
                }
                catch
                {
                    DisplayConnectionWarning();
                }
            }
            else
                DisplayConnectionWarning();
        }

        /// <summary>
        /// Used to display the alert when no internet connection.
        /// </summary>
        private void DisplayConnectionWarning()
        {
            IsInitialLoading = false;
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Application.Current.MainPage.DisplayAlert("Message", "Internet connection required.", "Ok");
            });
        }

        /// <summary>
        /// Command bound with the Refresh button in View.
        /// </summary>
        public Command RefreshCommand { get; set; }

        /// <summary>
        /// Command bound with the Sort button in View.
        /// </summary>
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
        public bool IsInitialLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;
                OnPropertyChanged("IsInitialLoading");
            }
        }

        /// <summary>
        /// Used to represent the state in UI while data fetching.
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
            //Refreshes the data in background thread by downloading again from the provided url when Refresh button is clicked.
            Task.Run(()=> GetData());
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
