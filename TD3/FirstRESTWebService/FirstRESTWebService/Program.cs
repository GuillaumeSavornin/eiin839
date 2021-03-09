using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace FirstRESTWebService
{

    class Program
    {
        // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
        static readonly HttpClient client = new HttpClient();

        static async Task Main()
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                JCDecaux api = new JCDecaux(client);

                // Print contracts & ask user to select one of them
                Contract[] constracts = await api.printContractsAsync();
                Console.WriteLine("Choose a contract index : ");
                int contractIndex = int.Parse(Console.ReadLine());

                // Print stations of that contract & ask user to select on of them
                Station[] stations = await api.printStationsOfContractAsync(constracts[contractIndex].name);
                Console.WriteLine("Choose a station index : ");
                int stationIndex = int.Parse(Console.ReadLine());

                // Print the station selected by the user
                await api.printStationOfContractAsync(constracts[contractIndex].name, stations[stationIndex].number);

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}
