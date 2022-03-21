using System;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            string result ="hello";
            Cat cat = new Cat("muci", 5);
           
            Console.WriteLine((result.GetType().Name == "Cat") ? cat.GetType().Name : cat.Name);
        }
    }
}
