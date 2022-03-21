using System;
using System.Text.RegularExpressions;
using System.Linq;
namespace Problem_2._Ad_Astra
{
    class Program
    {
        static void Main(string[] args)
        {
            string textCode = Console.ReadLine();
            var pattern = @"(?<symbol>[#\|])(?<item>[A-Za-z\s]+)\1(?<expDate>[0-9]{2}\/[0-9]{2}\/[0-9]{2})\1(?<calori>\d+)\1";
            MatchCollection matches = Regex.Matches(textCode, pattern);
            double sumCalories = 0;
            foreach (Match match in matches)
            {
                sumCalories +=double.Parse( match.Groups["calori"].Value);
                
            }
            Console.WriteLine($"You have food to last you for: {Math.Floor(sumCalories/2000)} days!");
            foreach (Match match in matches)
            {
                Console.WriteLine($"Item: {match.Groups["item"]}, Best before: {match.Groups["expDate"]}, Nutrition: {match.Groups["calori"]}");
            }


        }
    }
}
