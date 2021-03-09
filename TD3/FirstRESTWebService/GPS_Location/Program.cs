using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace GPS_Location
{
    class Program
    {
        // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
        static readonly HttpClient client = new HttpClient();
        readonly static string apiKey = "53cd9a615200bb7bea6e01ece896597513ccae67";

        static async Task Main(string[] args)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                // Get Arguments : "ContractName Latitude Longitude"
                String contractName = args[0];
                Double lat = Convert.ToDouble(args[1]);
                Double lng = Convert.ToDouble(args[2]);
                Position argPosition = new Position(lat, lng);
                Console.WriteLine("Position coordinates : " + argPosition);

                // Get all Stations for the desired contract
                string responseBody = await client.GetStringAsync("https://api.jcdecaux.com/vls/v1/stations?contract=" + contractName + "&apiKey=" + apiKey);
                Station[] stations = JsonSerializer.Deserialize<Station[]>(responseBody);

                // Find closest Station and print it.         
                Station stationClosestToMe = StationFinder.FindClosestStationToPositon(stations, argPosition);
                Console.WriteLine("\nClosest station : " + stationClosestToMe.ToStringRecap() + " | -> " + stationClosestToMe.position.DisplayDistanceTo(argPosition));
                Console.Read();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}
