using System;
using System.Collections.Generic;
using System.Text;

namespace FirstRESTWebService
{
    class Contract
    {

        public string name { get; set; }
        public string country_code { get; set; }
        public string commercial_name { get; set; }
        public string[] cities { get; set; }

        public override string ToString()
        {
            string cities = "";

            if(this.cities != null) { 
                foreach(string s in this.cities)
                {
                    if(cities.Length < 50) // don't print all of them 
                        cities += " " + s;
                }
            }
            else
            {
                cities = "NONE";
            }

            return "[" + country_code + "] " + name.ToUpper() + " - Cities : " + cities;
        }

    }
}
