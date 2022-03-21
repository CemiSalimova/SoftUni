using System;

namespace CarManufacturer
{
   public class StartUp
    {
      public  static void Main(string[] args)
        {
            Car car = new Car();
            car.Make = "vw";
            car.Model ="mk3";
            car.Year = 1992;
            Console.WriteLine($"{car.Make}-{car.Model}-{car.Year}");
        }
    }
}
