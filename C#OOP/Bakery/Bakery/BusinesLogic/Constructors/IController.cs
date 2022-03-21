using Bakery.Constructors;
using Bakery.Moduls.Foods;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.BusinesLogic.Constructors
{
   public interface IController
    {
        ICollection<IBakedFood> bakedFoods { get; set; }
        ICollection<IDrink> drinks { get; set; }
        ICollection<ITable> tables { get; set; }
        public string AddFood(string type, string name, decimal price);
        public string AddDrink(IDrink drink);
        public string AddTable(ITable table);
        public string ReserveTable(ITable table);
        public string OrderFood(int tableNumber,string foodName);
        public string OrderDrink(int tableNumber, string drinkName,string drinkBrand);
        public string LeaveTable(ITable table);
        public string GetFreeTablesInfo();
        public string GetTotalIncome();
        
    }
}
