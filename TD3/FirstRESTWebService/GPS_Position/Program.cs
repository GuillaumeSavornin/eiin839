using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;


namespace GPS_Position
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

                String contractName = args[0];
                Double lat = Convert.ToDouble(args[1]);
                Double lng = Convert.ToDouble(args[2]);
                GeoCoordinate currentPosition = new GeoCoordinate(lat, lng);
                Console.WriteLine(currentPosition);
                string responseBody = await client.GetStringAsync("https://api.jcdecaux.com/vls/v1/stations?contract=" + contractName + "&apiKey=" + apiKey);
                Station[] stations = JsonSerializer.Deserialize<Station[]>(responseBody);


                Console.WriteLine("================= STATIONS OF " + contractName.ToUpper() + " =================");
                Station stationClosestToMe = null;
                GeoCoordinate stationClosestToMePosition = null;


                for (int i = 0; i < stations.Length; i++)
                {
                    Console.WriteLine(i + " - " + stations[i].ToStringRecap());
                    Position pos = stations[i].position;
                    GeoCoordinate stationPosition = new GeoCoordinate(pos.lat, pos.lng);

                    if(stationClosestToMePosition == null)
                    {
                        stationClosestToMePosition = stationPosition;
                    }else if (stationPosition.GetDistanceTo(stationClosestToMePosition))
                    {

                    }
                }

                Console.WriteLine("\n");

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}
