using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Problem_2._Destination_Mapper
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            List<string> cities = new List<string>();
            var pattern = @"(?<symbol>[=]|[\/])(?<town>[A-Z][a-zA-Z]{2,})\1";
            Regex regex = new Regex(pattern);
            MatchCollection matchCity = regex.Matches(input);
            var sum = 0.0;
            foreach (Match destination in matchCity)
            {
                sum += destination.Groups["town"].Length;
                cities.Add(destination.Groups["town"].ToString());
            }
            Console.WriteLine($"Destinations: {String.Join(", ",cities)}");
            Console.WriteLine($"Travel Points: {sum}");
        }
    }
}
