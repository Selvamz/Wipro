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
        public ViewModel()
        {
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
                Source = new Data();
                if (jsonObject != "")
                {
                    //Converting json object into the collection.
                    Source = JsonConvert.DeserializeObject<Data>(jsonObject);
                }
            }
            else
                await App.Current.MainPage.DisplayAlert("Message", "Internet connection required.", "Ok");
        }

        public Data Source
        {
            get;
            set;
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
