using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Constructors
{
   public interface IFood 
    { 
        public string Name { get; }
        public int Portion { get; }
        public decimal Price { get; }
        
    }
}
