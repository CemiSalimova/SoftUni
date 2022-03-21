using Bakery.Constructors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Moduls.Tables
{
    public abstract class Table : ITable
    {
        private int capacity;
        private int numberOfPeople;
        public ICollection<IFood> FoodOrders { get;set; }
        public ICollection<IDrink> DrinkOrders { get; set; }
        public int TableNumber { get; set; }
        public int Capacity { 
            get => this.capacity;
            set
            {
                if (value<0)
                {
                    throw new ArgumentException("Capacity has to be greater than 0");
                }
                this.capacity = value;
            }
        }
        public int NumberOfPeople
        { 
            get=>this.numberOfPeople;
            
            set
            {
                if (value<=0)
                {
                    throw new ArgumentException("Cannot place zero or less people!");
                }
                this.numberOfPeople = value;
            }
        }
        public decimal PricePerPerson { get; set; }
        public bool IsReserved { get; set; } 
        public Table(int tableNumber,int capacity,decimal pricePerPerson )
        {
            this.TableNumber = tableNumber;
            this.Capacity = capacity;
            this.PricePerPerson = pricePerPerson;
            this.FoodOrders = new List<IFood>();
            this.DrinkOrders = new List<IDrink>();
        }
        public decimal Price
        {
            get
            {
                if (IsReserved)
                {
                    return PricePerPerson * NumberOfPeople;
                }
                else
                {
                    return PricePerPerson * Capacity;
                }
            }
        }

        public void Reserve(int numberOfPeople)
        {
            IsReserved = true;
           this.NumberOfPeople = numberOfPeople;
        }

        public void OrderFood(IFood food)
        {
            FoodOrders.Add(food);
        }

        public void OrderDrink(IDrink drink)
        {
            DrinkOrders.Add(drink);
        }

        public decimal GetBill()
        {
            decimal sumOfFoods = 0m;
            decimal sumOfDrinks = 0m;
            foreach (var food in FoodOrders)
            {
                sumOfFoods += food.Price;
            }
            foreach (var drink in DrinkOrders)
            {
                sumOfDrinks += drink.Price;
            }
            return sumOfDrinks + sumOfFoods+Price;
        }

        public void Clear()
        {
            FoodOrders.Clear();
            DrinkOrders.Clear();
            IsReserved = false;
            this.NumberOfPeople = 0;
            
        }

        public string GetFreeTableInfo()
        {
            if (!IsReserved)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"Table: {this.TableNumber}");
                sb.AppendLine($"Type: {this.GetType().Name}");
                sb.AppendLine($"Capacity: {this.Capacity}");
                sb.AppendLine($"Price per Person: {this.PricePerPerson}");
                return sb.ToString().TrimEnd();
            }

            return null;

        }
    }
}
