using Bakery.Constructors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Moduls.Foods
{

    public abstract class Drink :IDrink
    {
        private string name;
        private int portion;
        private decimal price;
        private string brand;

        public string Name
        {
            get
            {
                return this.name;
            }

            protected set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be null or white space!");
                }
                this.name = value;
            }
        }
        public int Portion
        {
            get { return this.portion; }
            protected set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Portion cannot be less or equal to zero");
                }
                this.portion = value;
            }

        }
        public decimal Price
        {
            get { return this.price; }
            protected set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Price cannot be less or equal to zero!");
                }
                this.price = value;
            }
        }

        public string Brand
        {
            get => this.brand;
            protected set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Brand cannot be null or white space!");
                }
                this.brand = value;
            }
        }
        public Drink(string name, int portion,decimal price,string brand)
        {
            this.Name = name;
            this.Portion = portion;
            this.Price = price;
            this.Brand = brand;
        }
        public override string ToString()
        {
            return $"{this.Name} {this.Name} - {this.Portion}ml - {this.Price:f2}lv";
        }
    }
}
