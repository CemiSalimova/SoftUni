using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace plant_discovery
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            Dictionary<string,Plant> db = new Dictionary<string,Plant>();
            for (int i = 0; i < n; i++)
            {
                var input = Console.ReadLine().Split("<->");
                string plant = input[0];
                int rarity = int.Parse(input[1]);
                if (!db.ContainsKey(plant))
                {
                    db.Add(plant, new Plant() { PlantName = plant, Rarity = rarity });
                }
                
            }
            var command = Console.ReadLine();
            while (command != "Exhibition")
            {
                string[] symbols = new string[] { " - ", ": " };
                var action = command.Split(symbols, StringSplitOptions.None);
                var plant = action[1];
                
                
                switch (action[0])
                {
                    case "Rate":
                        //•	Rate: {plant} - {rating} – add the given rating to the plant (store all ratings)
                        
                        var rating =double.Parse( action[2]);
                        if (db.ContainsKey(plant))
                        {
                            db[plant].Rating.Add(rating);
                        }
                        else
                        {
                            db.Add(plant, new Plant() { PlantName = plant });
                            
                        }
                        break;
                    case "Update":
                        //•	Update: {plant} - {new_rarity} – update the rarity of the plant with the new one  
                       // plant = action[1];
                        var new_rarity = int.Parse(action[2]);
                        if (db.ContainsKey(plant) )
                        {
                            db[plant].Rarity = new_rarity;
                        }
                        else
                        {
                            Console.WriteLine("error");
                            break;
                        }
                        
                        break;
                    case "Reset":
                        //•	Reset: {plant} – remove all the ratings of the given plant  
                       // plant = action[1];
                        if (db.ContainsKey(plant))
                        {
                            for (int i = 0; i < db[plant].Rating.Count; i++)
                            {
                                db[plant].Rating[i] = 0;
                            }
                        }
                        else
                        {
                            Console.WriteLine("error");
                            break;
                        }
                        break;
                }

                command = Console.ReadLine();
                
                
            }
            
            Console.WriteLine("Plants for the exhibition:");
            foreach (var item in db)
            {
                if (item.Value.Rating.Count == 0)
                {
                    item.Value.Rating.Add(0);
                }
            }
            

            foreach (var item in db.OrderByDescending(x=>x.Value.Rarity).ThenByDescending(x => x.Value.Rating.Average()))
            {
               
                
                Console.WriteLine($"- {item.Key}; Rarity: {item.Value.Rarity}; Rating: {item.Value.Rating.Average():f2}");
            }
        }
    }
    class Plant
    {
        public Plant()
        {
            Rating = new List<double>();
        }
        public string PlantName { get; set; }
        public int Rarity { get; set; }
      public List<double> Rating { get; set; }
        
    }
    
}
