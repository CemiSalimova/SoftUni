using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Moduls.Foods
{
    public class Tea : Drink
    {
        public const decimal TeaPrice = 2.50m;

        public Tea(string name, int portion,decimal price,  string brand) 
            : base(name, portion, TeaPrice, brand)
        {
            
        }
    }
}
