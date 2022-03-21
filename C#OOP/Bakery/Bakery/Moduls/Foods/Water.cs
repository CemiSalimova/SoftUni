using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Moduls.Foods
{
    public class Water : Drink
    {
        public const decimal WaterPrice = 1.50m;

        public Water(string name, int portion, decimal price, string brand)
            : base(name, portion, WaterPrice, brand)
        {
          
        }
    }
}
