using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;
using System.Xml;


namespace CurrencyConverter
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private CultureInfo currentculture;


        public MainViewModel()
        {
            this.Items = new ObservableCollection<ItemViewModel>();
        }

        

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ItemViewModel> Items { get; private set; }

        private string _sampleProperty = "Sample Runtime Property Value";
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
            Items.Clear();

            var isv = IsolatedStorageSettings.ApplicationSettings.Contains("Items") ? IsolatedStorageSettings.ApplicationSettings["Items"] : null;
            if (isv != null) { 
                Items = (ObservableCollection<ItemViewModel>) isv;
                KurserLoaded(this, new EventArgs());
                this.IsDataLoaded = true;
                return;
            }

            currentculture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("da-DK");
            WebClient client = new WebClient();

            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompleted);
            client.DownloadStringAsync(new Uri("http://www.ecb.int/stats/eurofxref/eurofxref-daily.xml"));
            
            this.IsDataLoaded = true;
        }

        void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error == null)
            {

                

                string reader = e.Result;

                String[] delt = reader.Split('\n');

                Items.Add(new ItemViewModel()
                              {
                                  Country = "",
                                  ImagePath = "/img/eur.png",
                                  ISO = "EUR",
                                  Kurs = decimal.Parse("1,000")
                              });

                foreach (string s in delt)
                {
                    if (s.Contains("<Cube currency="))
                    {
                        String[] sdelt = s.Split('\'');
                        //Kurser.Add(sdelt[1], Double.Parse(sdelt[3]));

                        var ku = sdelt[3].Replace('.', ',');

                        decimal kurs = decimal.Parse(ku,NumberStyles.Currency);

                        Items.Add(new ItemViewModel()
                                      {
                                          Country = "",
                                          ImagePath = "/img/" + sdelt[1].ToLower() + ".png",
                                          ISO = sdelt[1].ToUpper(),
                                          Kurs = kurs
                                      });
                    }
                }

                var isv = IsolatedStorageSettings.ApplicationSettings.Contains("Items") ? IsolatedStorageSettings.ApplicationSettings["Items"] : null;

                if (isv != null)
                    IsolatedStorageSettings.ApplicationSettings["Items"] = Items;
                else
                    IsolatedStorageSettings.ApplicationSettings.Add("Items", Items);           

                KurserLoaded(this, e);
            }
            Thread.CurrentThread.CurrentCulture = currentculture;
        }

        public event EventHandler KurserLoaded;
        
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