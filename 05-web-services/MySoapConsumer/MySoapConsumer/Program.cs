using MySoapConsumer.UnitConversionService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySoapConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new UnitConversionServiceClient())
            {
                client.Open();

                Console.WriteLine($"Service version: {client.GetServiceVersion()}");

                Console.WriteLine("Enter length in feet: ");
                var feet = double.Parse(Console.ReadLine());
                var meters = client.FeetToMeters(feet);
                Console.WriteLine($"Result in meters is: {meters}");

                Console.WriteLine("Enter temperature in degrees Celsius: ");
                var celsius = double.Parse(Console.ReadLine());
                var tempC = new Temperature
                {
                    Unit = Temperature.TempUnit.Celsius,
                    Value = celsius
                };
                Temperature tempF = client.ConvertTemperature(tempC);
                Console.WriteLine($"Result in degrees Fahrenheit is: {tempF.Value}");

                client.Close();
            }
            Console.ReadKey(); // wait at end of program
        }
    }
}
