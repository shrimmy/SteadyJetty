using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Net;


namespace Jetty01
{
    public class MainViewModel : INotifyPropertyChanged
    {
        WebClient client;
        bool forecastLoading = false;
        public MainViewModel()
        {
            this.Items = new ObservableCollection<ItemViewModel>();
            client = new WebClient();
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompleted);
        }

        void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                SampleProperty = e.Result;
            }
            ForecastLoading = false;
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ItemViewModel> Items { get; private set; }

        public bool ForecastLoading
        {
            get { return forecastLoading; }
            set 
            {
                if (value != forecastLoading)
                {
                    forecastLoading = value;
                    NotifyPropertyChanged("ForecastLoading");
                }
            }
        }
        
        private string _sampleProperty = "Loading...";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            ForecastLoading = true;
            client.DownloadStringAsync(new Uri("http://weather.noaa.gov/pub/data/forecasts/marine/coastal/pz/pzz134.txt", UriKind.Absolute));
            this.IsDataLoaded = true;
        }

        public void LoadTides()
        {
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}