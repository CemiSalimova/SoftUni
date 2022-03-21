using System;
using System.Collections.Generic;
using System.Linq;

namespace SnowWhite
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<Drafs, int> drafsInfo = new Dictionary<Drafs, int>();
            while (true)
            {
                string line = Console.ReadLine();
                if (line == "Once upon a time")
                {
                    break;
                }
                else
                {
                    string[] drawfsSkills = line.Split(" <:> ", StringSplitOptions.RemoveEmptyEntries);
                    Drafs drafs = new Drafs
                    {
                        Name = drawfsSkills[0],
                        Color = drawfsSkills[1],

                    };
                    int Physic = int.Parse(drawfsSkills[2]);
                    if (!drafsInfo.ContainsKey(drafs))
                    {
                        drafsInfo.Add(drafs, Physic);
                    }
                    if (drafsInfo[drafs] < Physic)
                    {
                        drafsInfo[drafs] = Physic;
                    }

                    

                }
            }
            Dictionary<Drafs, int> sorted = new Dictionary<Drafs, int>();
            sorted= drafsInfo.OrderByDescending(x => x.Value).ToDictionary(x=>x.Key,x=>x.Value);
            foreach (var item in sorted)
            {
                Console.WriteLine($"{item.Key} <-> {item.Value}");
            }
        }
    }
    class Drafs
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public override string ToString()
        {
            return $"({Color}) {Name}";
        }
    }
}
    

