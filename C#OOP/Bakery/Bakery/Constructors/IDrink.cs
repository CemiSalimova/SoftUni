using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Constructors
{
    public interface IDrink
    {
        public string Name { get; }
        public int Portion { get; }
        public decimal Price { get; }
        public string Brand { get; }
    }
}
