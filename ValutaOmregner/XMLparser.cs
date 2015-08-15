using System;
using System.Collections.Generic;
using System.Net;

namespace ValutaOmregner
{
    public class XMLparser
    {
        private string URL = "http://www.ecb.int/stats/eurofxref/eurofxref-daily.xml";
        private string xml;
        private Dictionary<String, Double> kurser; 

        public XMLparser()
        {
            Kurser = new Dictionary<string, double>();
            xml = "";
            WebClient client = new WebClient();
            client.DownloadStringCompleted += DownloadStringCallBack;
            client.DownloadStringAsync(new Uri(URL));
        }

        public Dictionary<string, double> Kurser
        {
            get { return kurser; }
            set { kurser = value; }
        }

        public void DownloadStringCallBack(object sender, DownloadStringCompletedEventArgs e)
        {
            xml = e.Result;
            
            if (!string.IsNullOrEmpty(xml))
            {
                String[] delt = xml.Split('\n');

                foreach (string s in delt)
                {
                    if (s.Contains("<Cube currency="))
                    {
                        String[] sdelt = s.Split('\'');
                        Kurser.Add(sdelt[1], Double.Parse(sdelt[3]));
                        
                    }	
                }
            }
        }

    }
}
