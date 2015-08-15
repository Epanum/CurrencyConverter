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

namespace ValutaOmregner
{
    public partial class MainPage : PhoneApplicationPage
    {
        private Dictionary<String, Double> kurser;
        private XMLparser parser;
        
        // Constructor
        public MainPage()
        {
            parser = new XMLparser();
            InitializeComponent();
        }

        private void btnVisKurser_Click(object sender, RoutedEventArgs e)
        {
            kurser = parser.Kurser;

            foreach (string kurs in kurser.Keys)
            {
                lstVaelgFra.Items.Add(kurs);
                lstVaelgTil.Items.Add(kurs);
            }
        }
    }
}