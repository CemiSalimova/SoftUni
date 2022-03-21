using Bakery.Constructors;
using Bakery.Moduls.Foods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Bakery.BusinesLogic.Constructors.Models
{
  public  class FoodFactory
    {


        public BakedFood CreatFood(string strtype, string name, decimal price)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type type = assembly.GetTypes().FirstOrDefault(t => t.Name == strtype);
            if (type == null)
            {
                throw new ArgumentException($"No this type food");
            }
            object[] ctorParams = new object[] {name,price };
            BakedFood food = (BakedFood)Activator.CreateInstance(type, ctorParams);
            return food;
        }

    }
}
