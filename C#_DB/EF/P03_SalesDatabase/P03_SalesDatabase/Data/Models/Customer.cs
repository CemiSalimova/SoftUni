using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P03_SalesDatabase.Data.Models
{
    public class Customer
    {
        public Customer()
        {
            this.Sales = new HashSet<Sale>();
        }
        [Key]
        public int CustomerId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(80)]
        public string Email { get; set; }

        [StringLength(20)]
        public string CreditCardNumber  { get; set; }
        public ICollection<Sale> Sales { get; set; }
    }
}
