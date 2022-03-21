using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProductsShop.Models
{
  public  class User
    {
        public User()
        {
            this.ProductsSold = new HashSet<Product>();
            this.ProductsBought = new HashSet<Product>();
        }
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
        //NB!

       //[InverseProperty("Seller")]
        public ICollection<Product> ProductsSold  { get; set; }
     
       // [InverseProperty("Buyer")]
        public ICollection<Product> ProductsBought  { get; set; }
    }
}
