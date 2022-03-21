using Bakery.Constructors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Moduls.Foods
{
   
    public class Cake : BakedFood
    {
        public const int InitialCakePortion = 245;

        public Cake(string name, int portion, decimal price) 
            : base(name, InitialCakePortion, price)
        {
           
        }
    }
}
