using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProductsShop.Models
{
    public class Product
    {
        public Product()
        {
            this.CategoryProducts = new HashSet<CategoryProduct>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }


       [ForeignKey(nameof(Buyer))]
        public int BuyerId { get; set; }
        public User Buyer { get; set; }
       
       [ForeignKey(nameof(Seller))]
        public int SellerId { get; set; }
        public User Seller { get; set; }

       
        //NB!
        public ICollection<CategoryProduct> CategoryProducts { get; set; }

    }
}
