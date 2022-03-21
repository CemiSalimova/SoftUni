using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            
                //INPUT [uncomment & modify if required]
          string sampleInput = null;
                int result = -404;
           
                sampleInput = Console.ReadLine();

            //write your Logic here:

            Random rand = new Random();
            List<string> listOfString = new List<string>();
            for (int j = 0; j < sampleInput.Length* sampleInput.Length; j++)
            {
                List<int> listOfIndex = new List<int>();
                string input = null;
                var indexFind = rand.Next(0, sampleInput.Length);
                listOfIndex.Add(indexFind);
                input += sampleInput[indexFind];
                
                while (input.Length<sampleInput.Length)
                {
                  
                    indexFind = rand.Next(0, sampleInput.Length);
                    //  var randText= sampleInput[rand.Next(0, sampleInput.Length)];
                    
                    if (listOfIndex .Contains(indexFind))
                    {
                        continue;
                    }
                    input += sampleInput[indexFind];
                    listOfIndex.Add(indexFind);
                }
                if (!listOfString.Contains(input))
                {
                    listOfString.Add(input);
                    Console.WriteLine(input);
                }
                j--;
                
            }

            //OUTPUT [uncomment & modify if required]
            //Console.WriteLine(result);

        }
    }
}
