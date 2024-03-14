using Labb2ProgTemplate.Entities;
using Console = System.Console;

namespace Labb2ProgTemplate.Services
{
    public class CustomerService
    {
        public List<Product> cart = new List<Product>();
        public void AddToCart(Customer customer, Product product)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("You added " + product._name + " to the cart.");
            Console.ForegroundColor = ConsoleColor.Gray;
            customer.Cart.Add(product);
        }
        public void RemoveFromCart(Customer customer, Product product)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("You removed " + product._name + " from the cart.");
            Console.ForegroundColor = ConsoleColor.Gray;
            customer.Cart.Remove(product);
        }
        public double CartTotal(Customer customer)
        {
            double sum = 0;
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(customer.Name + "'s Shopping Cart");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;
            for (int i = 0; i < customer.Cart.Count; i++)
            {
                sum += customer.Cart[i].Price;
                int index = i + 1;
                Console.WriteLine(index + ". | " + customer.Cart[i].Name + " | " + customer.Cart[i].Price.ToString("0.00") + customer.Cart[i].Currency);
            }
            if (customer.Member is "Gold" or "Silver" or "Bronze")
            {
                double discountedTotal = sum * customer.Discount;
                double discountPercentage = 100 - customer.Discount * 100;
                Console.WriteLine("\nTotal amount: " + sum.ToString("0.00") + customer.Cart[0].Currency);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(customer.Name + ", as our loyal customer you get " + discountPercentage + "% off your purchase!");
                Console.WriteLine("Total amount with " + customer.Member + "-discount: " + discountedTotal.ToString("0.00") + customer.Cart[0].Currency);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                Console.WriteLine("\nTotal amount: " + sum.ToString("0.00") + customer.Cart[0].Currency);
            }
            return sum;
        }
    }
}
