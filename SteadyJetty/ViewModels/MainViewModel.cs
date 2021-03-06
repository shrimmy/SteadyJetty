﻿using System;
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


namespace SteadyJetty
{
    public class MainViewModel : INotifyPropertyChanged
    {
        WebClient client;
        bool forecastLoading = false;
        private string jettyWindImageUrl;
        private string hatIslandWindUrl;
        private string tideUrl;

        public MainViewModel()
        {
            this.Items = new ObservableCollection<ItemViewModel>();
            client = new WebClient();
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompleted);

            jettyWindImageUrl = "http://windonthewater.com/wg.php?s=WA002&1335825831370";
            hatIslandWindUrl = "http://windonthewater.com/wg.php?s=WOTW07&1335825831299";
            tideUrl = string.Format("http://www.tide-forecast.com/tides/Everett-Washington.png?rnd={0}", Guid.NewGuid());
        }

        void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                Forecast = e.Result;
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
        public string Forecast
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
                    NotifyPropertyChanged("Forecast");
                }
            }
        }

        public string JettyWindImageUrl
        {
            get { return jettyWindImageUrl; }
            set
            {
                if (value != jettyWindImageUrl)
                {
                    jettyWindImageUrl = value;
                    NotifyPropertyChanged("JettyWindImageUrl");
                }
            }
        }

        public string HatIslandWindUrl
        {
            get { return hatIslandWindUrl; }
            set
            {
                if (value != hatIslandWindUrl)
                {
                    hatIslandWindUrl = value;
                    NotifyPropertyChanged("HatIslandWindUrl");
                }
            }
        }

        public string TideUrl
        {
            get { return tideUrl; }
            set
            {
                if (value != tideUrl)
                {
                    tideUrl = value;
                    NotifyPropertyChanged("TideUrl");
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