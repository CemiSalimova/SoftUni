using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Moduls.Tables
{
    public class OutsideTable : Table
    {
        private const decimal InitialPricePerPerson = 3.50m;
        public OutsideTable(int tableNumber, int capacity, decimal pricePerPerson) 
            : base(tableNumber, capacity, InitialPricePerPerson)
        {
        }
    }
}
