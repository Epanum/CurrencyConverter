using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CurrencyConverter.Resources;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

namespace CurrencyConverter
{
    public partial class MainPage : PhoneApplicationPage
    {
        private Popup p;

        private ItemViewModel selectedFROM;
        private ItemViewModel selectedTO;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);

            txtInputText1.InputScope = new InputScope()
                                      {
                                          Names =
                                          {
                                              new InputScopeName()
                                              {
                                                  NameValue = InputScopeNameValue.TelephoneNumber
                                              }
                                          }
                                      };

            try
            {
                ((ApplicationBarMenuItem)ApplicationBar.MenuItems[0]).Text = AppResources.ShowSource;
                ((ApplicationBarMenuItem)ApplicationBar.MenuItems[1]).Text = AppResources.ShowAllRates;
            }
            catch (Exception)
            {
            }
        }

        public void UpdateGUI()
        {
            txtDropdownFra.Text = selectedFROM.ISO + " - " + selectedFROM.Kurs;
            imgDropDownFra.Source = new BitmapImage(new Uri(selectedFROM.ImagePath, UriKind.Relative));

            txtDropdownTil.Text = selectedTO.ISO + " - " + selectedTO.Kurs;
            imgDropDownTil.Source = new BitmapImage(new Uri(selectedTO.ImagePath, UriKind.Relative));

            txtInputText1_TextChanged(this, null);
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            App.ViewModel.KurserLoaded += new EventHandler(ViewModel_KurserLoaded);

            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }            
        }

        void ViewModel_KurserLoaded(object sender, EventArgs e)
        {
            selectedFROM = ((MainViewModel)DataContext).Items[0];
            selectedTO = ((MainViewModel)DataContext).Items[5];

            UpdateGUI();
        }

        private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            WebBrowserTask task = new WebBrowserTask();
            task.Uri = new Uri("http://www.ecb.int/stats/eurofxref/eurofxref-daily.xml");
            task.Show();
        }

        private void ApplicationBarMenuItem_Click_1(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/DetailsPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void dropdownListFra_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            p = new Popup();
            var picker = new ValutaPicker();
            picker.lstSelect.SelectionChanged += new SelectionChangedEventHandler(lstSelect_SelectionChanged);
            picker.LayoutRoot.Tap += new EventHandler<System.Windows.Input.GestureEventArgs>(LayoutRoot_Tap);
            p.Child = picker;

            ApplicationBar.IsVisible = false;
            p.IsOpen = true;
        }

        void lstSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedFROM = (ItemViewModel) ((ValutaPicker) p.Child).lstSelect.SelectedItem;
            p.IsOpen = false;
            ApplicationBar.IsVisible = true;

            UpdateGUI();
        }

        private void dropdownListTil_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            p = new Popup();
            var picker = new ValutaPicker();
            picker.lstSelect.SelectionChanged += new SelectionChangedEventHandler(lstSelectTO_SelectionChanged);
            picker.LayoutRoot.Tap += new EventHandler<System.Windows.Input.GestureEventArgs>(LayoutRoot_Tap);
            p.Child = picker;
            ApplicationBar.IsVisible = false;
            p.IsOpen = true;
        }

        void LayoutRoot_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ApplicationBar.IsVisible = true;
            p.IsOpen = false;
        }

        void lstSelectTO_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedTO = (ItemViewModel)((ValutaPicker)p.Child).lstSelect.SelectedItem;
            p.IsOpen = false;
            ApplicationBar.IsVisible = true;

            UpdateGUI();
        }

        private void btnChangeUpDown_Click(object sender, RoutedEventArgs e)
        {
            ItemViewModel tempfra = selectedFROM;
            selectedFROM = selectedTO;
            selectedTO = tempfra;

            UpdateGUI();

            txtInputText1_TextChanged(this, null);
        }

        private void txtInputText1_MouseEnter(object sender, MouseEventArgs e)
        {
            if (txtInputText1.Text == AppResources.EnterAmount)
            {
                txtInputText1.Text = "";
            }
            else if(string.IsNullOrEmpty(txtInputText1.Text))
            {
                txtInputText1.Text = AppResources.EnterAmount;
            }
        }

        private void txtInputText2_MouseEnter(object sender, MouseEventArgs e)
        {
            if (string.IsNullOrEmpty(txtInputText2.Text))
            {
                txtInputText2.Text = AppResources.Result;
            }
        }

        private void txtInputText1_TextChanged(object sender, TextChangedEventArgs e)
        {
            decimal beløb;
            if (decimal.TryParse(txtInputText1.Text, out beløb))
            {
                if (selectedFROM != null && selectedTO != null && !string.IsNullOrEmpty(txtInputText1.Text) && txtInputText1.Text != AppResources.EnterAmount)
                {
                    txtInputText2.Text = (beløb*(selectedTO.Kurs/selectedFROM.Kurs)).ToString("0.########");
                }
            }
            else
            {
                txtInputText2.Text = "";
                txtInputText2_MouseEnter(this,null);
            }
        }
    }
}