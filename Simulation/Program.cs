using System;
using System.Collections.Generic;

namespace StoreSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введіть кількість покупців:");
            int numberOfCustomers = int.Parse(Console.ReadLine());

            List<Customer> customers = new List<Customer>();
            List<Product> products = GenerateProducts();
            double totalProfit = 0;

            Random rnd = new Random();

            for (int i = 0; i < numberOfCustomers; i++)
            {
                Customer customer = new Customer(rnd.Next(1, 1001), rnd.Next(1000, 10001));
                customers.Add(customer);
                Console.WriteLine($"Покупець {customer.CardNumber} зі стартовими грошима: {customer.Money}");

                while (customer.Money > 0 && products.Count > 0)
                {
                    Product product = products[rnd.Next(products.Count)];

                    if (product.Quantity > 0 && customer.Money >= product.Price)
                    {
                        customer.BuyProduct(product);
                        totalProfit += product.Price;
                        Console.WriteLine($"Покупець {customer.CardNumber} купив {product.Name} за {product.Price}.");
                    }
                    else
                    {
                        products.Remove(product);
                    }
                }
            }

            double maxSpent = 0;
            int maxProductsBought = 0;
            int maxSpentCustomerCard = -1;
            int maxProductsCustomerCard = -1;

            foreach (var customer in customers)
            {
                if (customer.TotalSpent > maxSpent)
                {
                    maxSpent = customer.TotalSpent;
                    maxSpentCustomerCard = customer.CardNumber;
                }

                if (customer.ProductsBought > maxProductsBought)
                {
                    maxProductsBought = customer.ProductsBought;
                    maxProductsCustomerCard = customer.CardNumber;
                }
            }

            Console.WriteLine($"Загальний прибуток магазину: {totalProfit}");
            Console.WriteLine($"Покупець з найбільшою кількістю товарів: {maxProductsCustomerCard}");
            Console.WriteLine($"Покупець, що витратив найбільше грошей: {maxSpentCustomerCard}");
        }

        static List<Product> GenerateProducts()
        {
            return new List<Product>
            {
                new Product("Яблука", 5.0, 10),
                new Product("Банани", 4.0, 15),
                new Product("Хліб", 2.0, 20),
                new Product("Молоко", 3.0, 25),
                new Product("Яйця", 1.0, 30)
            };
        }
    }

    class Customer
    {
        public int CardNumber { get; private set; }
        public double Money { get; set; }
        public double TotalSpent { get; private set; }
        public int ProductsBought { get; private set; }

        public Customer(int cardNumber, double money)
        {
            CardNumber = cardNumber;
            Money = money;
            TotalSpent = 0;
            ProductsBought = 0;
        }

        public void BuyProduct(Product product)
        {
            Money -= product.Price;
            TotalSpent += product.Price;
            ProductsBought++;
            product.Quantity--;
        }
    }

    class Product
    {
        public string Name { get; private set; }
        public double Price { get; private set; }
        public int Quantity { get; set; }

        public Product(string name, double price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }
    }
}
