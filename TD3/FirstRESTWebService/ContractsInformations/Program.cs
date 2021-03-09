using FirstRESTWebService;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ContractsInformations
{
    class Program
    {
        // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
        static readonly HttpClient client = new HttpClient();
        readonly static string apiKey = "53cd9a615200bb7bea6e01ece896597513ccae67";

        static async Task Main()
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                string responseBody = await client.GetStringAsync("https://api.jcdecaux.com/vls/v1/contracts?apiKey=" + apiKey);
                
                Contract[] contracts = JsonSerializer.Deserialize<Contract[]>(responseBody);

                Console.WriteLine("================= CONTRACTS =================\n");

                for (int i = 0; i < contracts.Length; i++)
                {
                    Console.WriteLine(i + " - " + contracts[i]);
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
