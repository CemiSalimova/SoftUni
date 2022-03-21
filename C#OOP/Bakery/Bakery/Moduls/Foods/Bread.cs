using Bakery.Constructors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Moduls.Foods
{
    public class Bread : BakedFood
    {
        public const int InitialBreadPortion = 200;

        public Bread(string name, int portion, decimal price) : base(name, InitialBreadPortion, price)
        {
            
        }
    }

   
    
}
