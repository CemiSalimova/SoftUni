using ProductsShop.Data;
using System;

namespace ProductsShop
{
   public class Program
    {
        static void Main(string[] args)
        {
            var context = new ProductShopContext();
            context.Database.EnsureCreated();
            Console.WriteLine("Succesfuly created!");
        }
    }
}
