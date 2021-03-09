using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FirstRESTWebService
{
    class JCDecaux
    {

        HttpClient client = new HttpClient();
        readonly string apiKey = "53cd9a615200bb7bea6e01ece896597513ccae67";

        public JCDecaux(HttpClient client)
        {
            this.client = client;
        }

        public async Task<Contract[]> printContractsAsync()
        {
            string responseBody = await client.GetStringAsync("https://api.jcdecaux.com/vls/v1/contracts?apiKey=" + apiKey);
            Contract[] contracts = JsonSerializer.Deserialize<Contract[]>(responseBody);


            Console.WriteLine ("================= CONTRACTS =================");

            for (int i = 0; i < contracts.Length; i++)
            {
                Console.WriteLine(i + " - " + contracts[i]);
            }

            Console.WriteLine("\n");

            return contracts;
        }

        public async Task<Station[]> printStationsOfContractAsync(string contractName)
        {
            string responseBody = await client.GetStringAsync("https://api.jcdecaux.com/vls/v1/stations?contract=" + contractName + "&apiKey=" + apiKey);
            Station[] stations = JsonSerializer.Deserialize<Station[]>(responseBody);


            Console.WriteLine("================= STATIONS OF " + contractName.ToUpper() + " =================");

            for (int i = 0; i < stations.Length; i++)
            {
                Console.WriteLine(i + " - " + stations[i].ToStringRecap());
            }

            Console.WriteLine("\n");

            return stations;
        }

        public async Task<Station> printStationOfContractAsync(string contractName, int stationNumber)
        {
            string responseBody = await client.GetStringAsync("https://api.jcdecaux.com/vls/v1/stations/" + stationNumber + "?contract=" + contractName + "&apiKey=" + apiKey);
            Station station = JsonSerializer.Deserialize<Station>(responseBody);


            Console.WriteLine("================= STATION OF " + contractName.ToUpper() + " =================");

            Console.WriteLine(station);

            Console.WriteLine("\n");

            return station;
        }
    }
}
