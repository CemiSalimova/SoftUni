using System;
using System.Linq;
namespace Problem_1._World_Tour
{
    class Program
    {
        static void Main(string[] args)
        {
            string routMap = Console.ReadLine();
            string input = Console.ReadLine();
            while (input!= "Travel")
            {
                var command = input.Split(":");
                switch (command[0])
                {
                    case "Add Stop":
                        //•	Add Stop:{index}:{string}
                        int startIndex = int.Parse(command[1]);
                        string str = command[2];
                        if (startIndex>=0&&startIndex<routMap.Length)
                        {
                            routMap=routMap.Insert(startIndex, str);
                        }
                            Console.WriteLine(routMap);

                        break;
                    case "Remove Stop":
                        //•	Remove Stop:{start_index}:{end_index} 
                        int start_Index = int.Parse(command[1]);
                        int end_Index = int.Parse(command[2]);
                        
                        if (start_Index >=0&& end_Index < routMap.Length&&start_Index<=end_Index)
                        {
                            routMap = routMap.Remove(start_Index, end_Index - start_Index+1);
                        }
                            Console.WriteLine(routMap);

                        break;
                    case "Switch":
                        //•	Switch:{old_string}:{new_string} 
                        string old_string = command[1];
                        string new_string = command[2];

                        if (routMap.Contains(old_string))
                        {
                            routMap = routMap.Replace(old_string, new_string);
                        }
                            Console.WriteLine(routMap);

                        break;
                }
                
                input = Console.ReadLine();
            }
            Console.WriteLine($"Ready for world tour! Planned stops: {routMap}");
        }
    }
}
