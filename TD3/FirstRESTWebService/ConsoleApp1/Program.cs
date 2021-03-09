using FirstRESTWebService;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp1
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
                String stationNumber = args[0];
                String contractName = args[1];

                string responseBody = await client.GetStringAsync("https://api.jcdecaux.com/vls/v1/stations/" + stationNumber + "?contract=" + contractName + "&apiKey=" + apiKey);
                Station station = JsonSerializer.Deserialize<Station>(responseBody);

                Console.WriteLine("================= STATION OF " + contractName.ToUpper() + " =================");
                Console.WriteLine(station);
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
