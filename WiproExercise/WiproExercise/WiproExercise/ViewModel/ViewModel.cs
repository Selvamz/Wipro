using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiproExercise
{
    public class ViewModel : INotifyPropertyChanged
    {
        private Data source;
        private bool isLoading = true;

        public ViewModel()
        {
            DataSource = new Data();
            GetData();
        }

        private async void GetData()
        {
            //If it has an internet connection, then fetch data from online. Else displays an alert message.
            if(HasInternet)
            {
                var client = new System.Net.Http.HttpClient();
                var response = await client.GetAsync("https://dl.dropboxusercontent.com/s/2iodh4vg0eortkl/facts.json");
                string jsonObject = await response.Content.ReadAsStringAsync();
                if (jsonObject != "")
                {
                    //Converting json object using NewtonSoft.Json package.
                    DataSource = JsonConvert.DeserializeObject<Data>(jsonObject);
                }
            }
            else
                await App.Current.MainPage.DisplayAlert("Message", "Internet connection required.", "Ok");
            IsLoading = false;
        }

        public Data DataSource
        {
            get { return source; }
            set
            {
                source = value;
                OnPropertyChanged("DataSource");
            }
        }

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
        /// Gets whether internet is connected or not.
        /// </summary>
        private bool HasInternet
        {
            get
            {

                return CrossConnectivity.Current.IsConnected;
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
