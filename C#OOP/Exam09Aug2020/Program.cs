using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam09Aug2020
{
    class Program
    {
        public static object Dictoinary { get; private set; }

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            Dictionary<string, List<double>> database = new Dictionary<string, List<double>>();
            for (int i = 0; i <n; i++)
            {
                var input = Console.ReadLine().Split("<->");
                string plant = input[0];
                var rarity = double.Parse(input[1]);
                if (database.ContainsKey(plant))
                {
                    database[plant].Add(rarity);
                }
                else
                {
                    database.Add(plant, new List<double>());
                    database[plant].Add(rarity);
                }
            }
            var DB = database.ToString();
            string newInput = Console.ReadLine();
           // Dictionary<string, double> database = new Dictionary<string, double>();
            while (newInput!= "Exhibition")
            {
                var command = newInput.Split(':','-');
                var action = command[0];
                var plant = command[1];
                var rating = double.Parse(command[2]);
                
                switch (action)
                {
                    case "Rate":
                        //•	Rate: {plant} - {rating} – add the given rating to the plant (store all ratings)
                        if (database.ContainsKey(plant))
                        {
                            database[plant][1]+= rating;
                        }
                        else
                        {
                            Console.WriteLine("error");
                            break;
                        }

                        break;
                    case "Update":
                        //•	Update {plant} - {new_rarity} – update the rarity of the plant with the new one
                        if (database.ContainsKey(plant))
                        {
                            database[plant][1] = rating;
                        }
                        else
                        {
                            Console.WriteLine("error");
                            break;
                        }
                        break;
                    case "Reset":
                        //•	Reset: {plant} – remove all the ratings of the given plant
                        if (database.ContainsKey(plant))
                        {
                            database[plant][1] = 0.00;
                        }
                        else
                        {
                            Console.WriteLine("error");
                            break;
                        }
                        break;
                    default:
                        Console.WriteLine("error");
                        break;
                }
                Console.WriteLine("Plants for the exhibition:");
                foreach (var itam in database)
                {
                    Console.WriteLine($"- {itam.Key}; Rarity: {itam.Value}; Rating: {itam.Value[1]}");
                }
            }

        }
    }
}
