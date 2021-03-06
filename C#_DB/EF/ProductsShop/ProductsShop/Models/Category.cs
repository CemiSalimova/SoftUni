using System;
using System.Collections.Generic;
using System.Text;

namespace ProductsShop.Models
{
   public class Category
    {
        public Category()
        {
            this.CategoryProducts = new HashSet<CategoryProduct>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        //NB!
        public ICollection<CategoryProduct> CategoryProducts { get; set; }
    }
}
