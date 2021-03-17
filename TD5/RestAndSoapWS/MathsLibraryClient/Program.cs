using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathsLibraryClient
{
    class Program
    {
        static void Main(string[] args)
        {
            MathsOperations.IMathsOperations calculator = new MathsOperations.MathsOperationsClient();


            Console.WriteLine("Type d'opération à effectuer : (1) +   (2) -   (3) x   (4) /");
            int operationType = int.Parse(Console.ReadLine());

            Console.WriteLine("Entrez une première valeur :");
            int a = int.Parse(Console.ReadLine());

            Console.WriteLine("Entrez une deuxième valeur :");
            int b = int.Parse(Console.ReadLine());

            switch (operationType)
            {
                case 1:
                    int sum = calculator.Add(a, b);
                    Console.WriteLine("Somme de " + a + " + " + b + " = " + sum);
                    break;
                case 2:
                    int sub = calculator.Subtract(a, b);
                    Console.WriteLine("Soustraction de " + a + " - " + b + " = " + sub);
                    break;
                case 3:

                    int mult = calculator.Multiply(a, b);
                    Console.WriteLine("Multiplication de " + a + " * " + b + " = " + mult);
                    break;
                case 4:
                    float div = calculator.Divide(a, b);
                    Console.WriteLine("Division de " + a + " / " + b + " = " + div);
                    break;
            }

            

            Console.Read();
        }
    }
}
