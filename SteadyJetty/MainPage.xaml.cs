using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace SteadyJetty
{
    public partial class MainPage : PhoneApplicationPage
    {
        private double initialScale;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            App.ViewModel.LoadData();
        }

        private void GestureListener_PinchDelta(object sender, PinchGestureEventArgs e)
        {
            tideImage.ScaleX = tideImage.ScaleY = initialScale * e.DistanceRatio;
        }

        private void GestureListener_PinchStarted(object sender, PinchStartedGestureEventArgs e)
        {
            initialScale = tideImage.ScaleX;
        }

        private void GestureListener_DoubleTap(object sender, Microsoft.Phone.Controls.GestureEventArgs e)
        {
            tideImage.ScaleX = tideImage.ScaleY = 1;
        }

        private void GestureListener_DragDelta(object sender, DragDeltaGestureEventArgs e)
        {
            tideImage.TranslateX += e.HorizontalChange;
            tideImage.TranslateY += e.VerticalChange;

        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            EmailComposeTask emailComposer = new EmailComposeTask();
            emailComposer.Subject = string.Format("Jetty Conditions for {0}", DateTime.Now.ToShortDateString());
            emailComposer.Body = App.ViewModel.Forecast;
            emailComposer.Show();

        }
    }
}