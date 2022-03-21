using Bakery.BusinesLogic.Constructors.Models;
using Bakery.Constructors;
using Bakery.Moduls.Foods;
using Bakery.Moduls.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Bakery
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            FoodFactory foodFactory = new FoodFactory();
          var newfood=  foodFactory.CreatFood("Bread", "White", 2.90m);
            Console.WriteLine(newfood);

            //// AddFood Bread White 2.90
            // Controller controller = new Controller();
            // controller.AddFood("Bread", "White", 2.90m);
            // Console.WriteLine(controller.drinks);


            //Water water1 = new Water("Spring", 500, 1.2m, "divna");
            //Drink water2 = new Water("Spring", 330, 1.2m, "Divna");
            //Drink tea01 = new Tea("Lipton", 500, 1.2m, "Divna");
            //Tea tea02 = new Tea("Golden", 500, 1.2m, "Divna");
            //Bread bread = new Bread("white", 200, 2.9m);
            //Cake cake = new Cake("Happy", 245, 12.00m);


            //List<Table> tables = new List<Table>();
            //Table table1 = new OutsideTable(1, 5, 2.5m);
            //Table table2 = new InsideTable(2, 7, 2.5m);
            //Table table3 = new InsideTable(3, 2, 2.5m);

            //tables.Add(table1);
            //tables.Add(table2);
            //tables.Add(table3);

            //table2.Reserve(7);
            //table3.OrderDrink(tea01);
            //table3.OrderDrink(water1);
            //table3.OrderDrink(water1);
            //table3.OrderFood(cake);
            //table3.OrderFood(cake);
            //table3.OrderFood(bread);
            //table3.OrderFood(cake);
            //table3.OrderFood(bread);
            //foreach (var t in tables)
            //{
            //    Console.WriteLine(t.GetFreeTableInfo());
            //}
            //Console.WriteLine($"{String.Join("\n", table3.DrinkOrders)}");
            //Console.WriteLine($"{String.Join("\n", table3.FoodOrders)}");
            //Console.WriteLine(table3.GetBill());
            //Console.WriteLine(table2.IsReserved);
            //Console.WriteLine(table1.Price);
        }
        
    }
}
