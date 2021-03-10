using SOAPWebService.Calculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOAPWebService
{
    class Program
    {
        static void Main(string[] args)
        {
            CalculatorSoapClient calculator = new CalculatorSoapClient();

            Console.WriteLine("Entrez une première valeur :");
            int a = int.Parse(Console.ReadLine());

            Console.WriteLine("Entrez une deuxième valeur :");
            int b = int.Parse(Console.ReadLine());

            int sum = calculator.Add(a, b);

            Console.WriteLine("Somme de " + a + " + " + b + " = " + sum);
            Console.Read();
        }
    }
}
