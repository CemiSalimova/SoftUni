using System;
using System.Collections.Generic;
using System.Linq;
namespace Problem_3._The_Pianist
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            Dictionary<string, Song> pieces = new Dictionary<string, Song>();
            for (int i = 0; i < n; i++)
            {
                var input = Console.ReadLine().Split("|");
                //{piece}|{composer}|{key}
                var piece = input[0];
                var composer = input[1];
                var key = input[2];
                
                Song song = new Song() { Composer = composer, Key = key };
                if (!pieces.ContainsKey(piece))
                {
                    pieces.Add(piece, song);
                }
            }
            var command = Console.ReadLine();
            while (command!="Stop")
            {
                var action = command.Split("|");
                switch (action[0])
                {
                    case "Add":
                        //•	Add|{piece}|{composer}|{key}
                        var piece = action[1];
                        var composer = action[2];
                        var key = action[3];
                        if (pieces.ContainsKey(piece))
                        {
                            Console.WriteLine($"{piece} is already in the collection!");
                        }
                        else
                        {
                            pieces.Add(piece, new Song() {Key=key,Composer=composer });
                            Console.WriteLine($"{piece} by {action[2]} in {action[3]} added to the collection!");
                        }
                        break;
                    case "Remove":
                        //•	Remove|{piece}
                        piece = action[1];
                        if (!pieces.ContainsKey(piece))
                        {
                            Console.WriteLine($"Invalid operation! {piece} does not exist in the collection.");
                        }
                        else
                        {
                            pieces.Remove(piece);
                            Console.WriteLine($"Successfully removed {piece}!");
                        }
                        break;
                    case "ChangeKey":
                        //  •	ChangeKey|{piece}|{new key}
                        piece = action[1];
                        var newKey = action[2].ToString();
                        if (!pieces.ContainsKey(piece))
                        {
                            Console.WriteLine($"Invalid operation! {piece} does not exist in the collection.");
                        }
                        else
                        {
                            Console.WriteLine($"Changed the key of {piece} to {newKey}!");

                            pieces[piece].Key = newKey;
                        }
                        break;
                }


                command = Console.ReadLine();
            }
            //var pieces2=pieces.OrderBy(x=>x.Value.Composer))
            foreach (var item in pieces.OrderBy(x => x.Key).ThenBy(y=>y.Value.Composer))
            {
                Console.WriteLine($"{item.Key} -> Composer: {item.Value.Composer}, Key: {item.Value.Key}");
            }
        }
    }
    class Song
    {//{piece}|{composer}|{key}
       // public string Piece { get; set; }
        public string Composer { get; set; }
        public string Key { get; set; }

    }
}
