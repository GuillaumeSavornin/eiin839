using FirstRESTWebService;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace StationsInformations
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

                string responseBody = await client.GetStringAsync("https://api.jcdecaux.com/vls/v1/stations?contract=" + contractName + "&apiKey=" + apiKey);
                Station[] stations = JsonSerializer.Deserialize<Station[]>(responseBody);


                Console.WriteLine("================= STATIONS OF " + contractName.ToUpper() + " =================");

                for (int i = 0; i < stations.Length; i++)
                {
                    Console.WriteLine(i + " - " + stations[i].ToStringRecap());
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
