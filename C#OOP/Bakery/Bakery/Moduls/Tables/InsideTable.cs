using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Moduls.Tables
{
    public class InsideTable : Table
    { private const decimal InitialPricePerPerson = 2.5m;
        public InsideTable(int tableNumber, int capacity, decimal pricePerPerson ) 
            : base(tableNumber, capacity, InitialPricePerPerson)
        {
        }
    }
}
