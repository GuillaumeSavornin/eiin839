using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceReference1.IAuthenticator auth = new ServiceReference1.AuthenticatorClient();
            ServiceReference2.IWeather weather = new ServiceReference2.WeatherClient();


            Console.WriteLine("============== Authentification ==============\n");
            Console.WriteLine("Name: ");
            String name = Console.ReadLine();

            Console.WriteLine("Password: ");
            String password = Console.ReadLine();

            bool hasAccess = auth.ValidateCredentials(name, password);

            if (hasAccess)
            {
                Console.WriteLine("============== Weather ==============\n");
                Console.WriteLine("City Name: ");
                String city = Console.ReadLine();

                String response = weather.checkWeatherCity(city);
                Console.WriteLine(response);
            }
            else
            {

                Console.WriteLine("Auth Invalide...");
            }

            Console.Read();
        }
    }
}
