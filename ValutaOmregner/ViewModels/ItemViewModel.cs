using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CurrencyConverter
{
    public class ItemViewModel : INotifyPropertyChanged
    {
        private string _ImagePath;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string ImagePath
        {
            get
            {
                return _ImagePath;
            }
            set
            {
                if (value != _ImagePath)
                {
                    _ImagePath = value;
                    NotifyPropertyChanged("ImagePath");
                }
            }
        }

        private string _Country;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Country
        {
            get
            {
                return _Country;
            }
            set
            {
                if (value != _Country)
                {
                    _Country = value;
                    NotifyPropertyChanged("Country");
                }
            }
        }

        private string _ISO;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string ISO
        {
            get
            {
                return _ISO;
            }
            set
            {
                if (value != _ISO)
                {
                    _ISO = value;
                    NotifyPropertyChanged("ISO");
                }
            }
        }

        private decimal _Kurs;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public decimal Kurs
        {
            get
            {
                return _Kurs;
            }
            set
            {
                if (value != _Kurs)
                {
                    _Kurs = value;
                    NotifyPropertyChanged("Kurs");
                }
            }
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