using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Constructors
{
    public interface ITable
    {
       public ICollection<IFood> FoodOrders { get; set; }
      public  ICollection<IDrink>DrinkOrders { get; set; }
        public int TableNumber { get; set; }
        public int Capacity { get; set; }
        public int NumberOfPeople { get; set; }
        public decimal PricePerPerson { get; set; }
        public  bool IsReserved { get; set; } 
        public decimal Price { get; }
        void Reserve(int numberOfPeople);
        void OrderFood(IFood food);
        void OrderDrink(IDrink drink);
        decimal GetBill();
        void Clear();
        string GetFreeTableInfo();


    }
}
