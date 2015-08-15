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
using System.Windows.Navigation;
using CurrencyConverter.Resources;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace CurrencyConverter
{
    public partial class DetailsPage : PhoneApplicationPage
    {
        // Constructor
        public DetailsPage()
        {
            InitializeComponent();
        }

        // When page is navigated to set data context to selected item in list
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataContext = App.ViewModel;

            try
            {
                ((ApplicationBarIconButton)ApplicationBar.Buttons[0]).Text = AppResources.Update;
            }
            catch (Exception)
            {
            }
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            App.ViewModel.LoadData();
        }
    }
}