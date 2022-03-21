using System;
using System.Linq;
namespace Exam15AugustRetake
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = Console.ReadLine();
            string input= Console.ReadLine();
            while (input!="Decode")
            {
                var command = input.Split("|");
                var action = command[0];
                switch (action)
                {
                    case "ChangeAll":
                        //•	ChangeAll {substring} {replacement} 
                        text = text.Replace(command[1], command[2]);
                        break;
                    case "Insert":
                        //•	Insert {index} {value}
                        var index = int.Parse(command[1]);
                        text = text.Insert(index, command[2]);
                        break;
                    case "Move":
                        //•	Move {number of letters}
                        var n = int.Parse(command[1]);
                        var subTextEnd = text.Substring(0, n);
                        var subTextStart = text.Substring(n);
                        text = subTextStart + subTextEnd;
                        break;
                }

                input = Console.ReadLine();
            }
            Console.WriteLine($"The decrypted message is: {text}");
        }
    }
}
