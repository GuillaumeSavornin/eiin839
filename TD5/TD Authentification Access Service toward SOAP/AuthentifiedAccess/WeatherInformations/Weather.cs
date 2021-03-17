using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace WeatherInformations
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Weather : IWeather
    {
        // you can use the following keys to acces the web service
        String key1 = "327f93f0117f1f0009c3a14ee69ff283";

        public string checkWeatherCity(string city)
        {
            string url1 = "http://api.openweathermap.org/data/2.5/weather?q=" + city + ",fr&appid=" + key1;

            // create a web  Client
            // A C#/.Net WebClient provides common methods for sending data to and receiving 
            // data from a resource identified by a URL
            WebClient client = new WebClient();
            // download the data
            string data = client.DownloadString(url1);

            Console.WriteLine("connecting openweathermap.org");
            // wait a bit for the server to have the time to send its answer
            Thread.Sleep(300);

            // show information of the data (json) that have been sent
            JObject jso = JObject.Parse(data);

            String result = "in " + city + " the visibility is " + jso.SelectToken("visibility") +
                " meters and the weather is " + jso.SelectToken("weather[0].main");
            Console.WriteLine(result);
            return result;

        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
