using Bakery.Constructors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bakery.BusinesLogic.Constructors.Models
{
    public class Controller : IController
    {
        private readonly FoodFactory foodFactory;
        public ICollection<IBakedFood> bakedFoods { get; set; }

        public ICollection<IDrink> drinks { get; set; }

        public ICollection<ITable> tables { get; set; }
        public Controller()
        {
            bakedFoods = new HashSet<IBakedFood>();
            drinks = new HashSet<IDrink>();
            this.foodFactory = new FoodFactory();
        }
        public string AddFood(string type, string name, decimal price)
        {

            IBakedFood food = this.foodFactory.CreatFood(type, name, price);
            bakedFoods.Add(food);
            return $"Added {food.Name} ({food.GetType().Name}) to the menu";

        }
        public string AddDrink(IDrink drink)
        {
            drinks.Add(drink);
            return $"Added {drink.Name} ({drink.Brand}) to the drink menu";
        }
        public string AddTable(ITable table)
        {
            tables.Add(table);
            return $"Added table number {table.TableNumber} in the bakery";
        }
        public string GetFreeTablesInfo()
        {
            var freeTbales = tables.Where(t => t.IsReserved = false).ToArray();
            return $"{String.Join("\n", freeTbales.ToString())}";
        }
        public string GetTotalIncome()
        {
            throw new NotImplementedException();
        }
        public string LeaveTable(ITable table)
        {
            throw new NotImplementedException();
        }
        public string OrderFood(int tableNumber, string foodName)
        {
            var orderTable = tables.FirstOrDefault(t => t.TableNumber == tableNumber);
            var orderFood = bakedFoods.FirstOrDefault(t => t.Name == foodName);
            if (orderTable == null)
            {
                throw new ArgumentException($"Could not find table {tableNumber}");
            }
            if (orderFood == null)
            {
                throw new ArgumentException($"No {foodName} in the menu");
            }
            return $"Table {tableNumber} ordered {foodName}";

        }
        public string OrderDrink(int tableNumber, string drinkName, string drinkBrand)
        {

            var orderTable = tables.FirstOrDefault(t => t.TableNumber == tableNumber);
            var orderDrink = drinks.FirstOrDefault(t => t.Name == drinkName && t.Brand == drinkBrand);
            if (orderTable == null)
            {
                throw new ArgumentException($"Could not find table {tableNumber}");
            }
            if (orderDrink == null)
            {
                throw new ArgumentException($"There is no {drinkName} {drinkBrand} available");
            }
            return $"Table {tableNumber} ordered {drinkName} {drinkBrand}";
        }
        public string ReserveTable(ITable table)
        {
            throw new NotImplementedException();
        }


    }
}

