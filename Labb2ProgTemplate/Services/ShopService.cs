using Labb2ProgTemplate.Entities;
using Labb2ProgTemplate.Entities.CustomerMembership;


namespace Labb2ProgTemplate.Services
{
    public class ShopService
    {
        public CustomerService customerservice = new CustomerService();
        private Customer CurrentCustomer { get; set; }
        public List<Customer> CustomerList { get; set; } = new()
        {
            new GoldCustomer("Knatte", "123", "Gold", 0.85),
            new SilverCustomer("Fnatte", "321", "Silver", 0.90),
            new BronzeCustomer("Tjatte", "213", "Bronze", 0.95)
        };
        private List<Product> Products { get; set; } = new()
        {
            new Product("Teineken Non-Alcoholic", 0, 10.90, ""),
            new Product("Bella Artois", 0, 21.50, ""),
            new Product("Walcon Export", 0, 12.90, ""),
            new Product("Hofiero Original", 0, 11.90, ""),
            new Product("Castillo de Gredos Azul", 0, 70.90, ""),
            new Product("The Famous Mouse", 0, 170.90, "")
        };
        public ShopService()
        {
            CreateCustomerFile();
            MainMenu();
        }
        public void MainMenu()
        {
            Console.Clear();
            ReadCustomerFile();
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Welcome to version 1.0 of DrinkStore Online. Select a menu item below using the number keys");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("Copyright \u00a9 1994 by Anders Öberg\n");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            var menuInput = Console.ReadKey();
            switch (menuInput.Key)
            {
                case ConsoleKey.D1:
                    Login();
                    break;
                case ConsoleKey.D2:
                    Register();
                    break;
                default:
                    Console.Clear();
                    MainMenu();
                    break;
            }
        }
        private void Login()
        {
            Console.Clear();
            Console.WriteLine("Please enter your username and password:");
            while (true)
            {
                Console.Write("Username : ");
                string inputName = Console.ReadLine();
                foreach (var customer in CustomerList)
                {
                    if (inputName == customer.Name)
                    {
                        Console.Write("Password : ");
                        customer.CheckPassword(Console.ReadLine());
                        CurrentCustomer = customer;
                        SelectCurrency();
                    }
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("There are no users with this username. Press any key to go back.");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.ReadKey();
                break;
            }
            MainMenu();
        }
        private void Register()
        {
            Console.Clear();
            Console.WriteLine("Please enter a username:");
            Console.Write("Username : ");
            string inputName = Console.ReadLine();
            while (true)
            {
                foreach (var customer in CustomerList)
                {
                    if (inputName == customer.Name)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("That username is already taken. Press any key to go back.");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.ReadKey();
                        MainMenu();
                    }
                    else if (inputName == string.Empty)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Usernames must at least contain 1 letter. Press any key to go back.");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.ReadKey();
                        MainMenu();
                    }
                }
                break;
            }
            Console.Write("Password : ");
            string inputPassword = Console.ReadLine();
            Customer userCustomer = new Customer(inputName, inputPassword);
            SaveCustomerToFile(userCustomer);
            CustomerList.Add(userCustomer);
            CurrentCustomer = userCustomer;
            SelectCurrency();
        }
        private void SelectCurrency()
        {
            Console.Clear();
            Console.WriteLine("Please select the currency you wish to use:");
            Console.WriteLine("1. Euro / EUR");
            Console.WriteLine("2. Svenska kronor / SEK");
            Console.WriteLine("3. US Dollars / USD");
            var menuInput = Console.ReadKey();
            switch (menuInput.Key)
            {
                case ConsoleKey.D1:
                    for (int i = 0; i < Products.Count; i++)
                    {
                        Products[i].Currency = " EUR";
                        double euro = Products[i].BasePrice * 0.087;
                        Products[i].Price = euro;
                    }
                    break;
                case ConsoleKey.D2:
                    for (int i = 0; i < Products.Count; i++)
                    {
                        Products[i].Currency = "kr";
                        Products[i].Price = Products[i].BasePrice;
                    }
                    break;
                case ConsoleKey.D3:
                    for (int i = 0; i < Products.Count; i++)
                    {
                        Products[i].Currency = " USD";
                        double usd = Products[i].BasePrice * 0.091;
                        Products[i].Price = usd;
                    }
                    break;
                default:
                    SelectCurrency();
                    break;
            }
            ShopMenu();
        }
        private void ShopMenu()
        {
            Console.Clear();
            while (true)
            {
                if (CurrentCustomer.Member is "Gold" or "Silver" or "Bronze")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("You're logged in as: " + CurrentCustomer.Name + "\nMembership level: " + CurrentCustomer.Member);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("You're logged in as: " + CurrentCustomer.Name);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                Console.WriteLine("Select which items you wish to add to your cart: ");
                for (int i = 0; i < Products.Count; i++)
                {
                    int index = i + 1;
                    Console.WriteLine(index + ". " + Products[i].Name + " | " + Products[i].Price.ToString("0.00") + Products[i].Currency);
                }
                Console.WriteLine("--------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Currently " + CurrentCustomer.Cart.Count + " products in " + CurrentCustomer.Name + "'s cart");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine("To show the shop menu. Press [ENTER]");
                bool input = int.TryParse(Console.ReadLine(), out var prodIndex);
                if (input == false)
                {
                    break;
                }
                if (prodIndex - 1 < Products.Count && prodIndex - 1 >= 0)
                {
                    Console.Clear();
                    customerservice.AddToCart(CurrentCustomer, Products[prodIndex - 1]);
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please select a valid product");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
            Menu();
        }
        private void ViewCart()
        {
            Console.Clear();
            while (true)
            {
                if (CurrentCustomer.Cart.Count >= 1)
                {
                    customerservice.CartTotal(CurrentCustomer);
                }
                if (CurrentCustomer.Cart.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The cart is empty");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Press any key to go back to the shop menu");
                    Console.ReadKey();
                    break;
                }
                Console.WriteLine("To remove from cart, press the relevant number key");
                Console.WriteLine("-------------------------------");
                Console.WriteLine("To show the shop menu. Press any key");
                bool input = int.TryParse(Console.ReadLine(), out var cartIndex);
                if (input == false)
                {
                    break;
                }
                if (cartIndex - 1 < CurrentCustomer.Cart.Count && cartIndex - 1 >= 0)
                {
                    Console.Clear();
                    customerservice.RemoveFromCart(CurrentCustomer, CurrentCustomer.Cart[cartIndex - 1]);
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please choose a valid product");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
            ShopMenu();
        }
        private void Checkout()
        {
            Console.Clear();
            if (CurrentCustomer.Cart.Count >= 1)
            {
                customerservice.CartTotal(CurrentCustomer);
            }

            if (CurrentCustomer.Cart.Count <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Your cart is empty.");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Press any key to go back to the shopping menu");
                Console.ReadKey();
                ShopMenu();
            }
            Console.WriteLine("------------------------------------");
            Console.WriteLine("How would you like to pay?");
            Console.WriteLine("1. Electronic Payment System (EPS)");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("To show the shop menu. Press any key"); ;
            var menuInput = Console.ReadKey();
            switch (menuInput.Key)
            {
                case ConsoleKey.D1:
                    Console.Clear();
                    Console.WriteLine("Please await confirmation. Don't close this window.");
                    string pattern = "......";
                    foreach (char dot in pattern)
                    {
                        Console.Write(dot);
                        Thread.Sleep(700);
                    }
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("\n Payment successful ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine("-------------------------------------------------------");
                    Console.WriteLine("Thank you for your patronage " + CurrentCustomer.Name + ". Your order is on its way");
                    Console.WriteLine("-------------------------------------------------------");
                    CurrentCustomer.Cart.Clear();
                    Console.WriteLine("Press any key to log out and go back to the main menu");
                    Console.ReadKey();
                    MainMenu();
                    break;
                default:
                    Menu();
                    break;
            }
        }
        private void Menu()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[V]");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("iew Cart");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\n[C]");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("heckout");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\n[S]");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("elect currency");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\n[L]");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("ogout");
            Console.WriteLine("\nContinue shopping? | Press any other key to go to store ");
            var menuInput = Console.ReadKey();
            switch (menuInput.Key)
            {
                case ConsoleKey.V:
                    ViewCart();
                    break;
                case ConsoleKey.C:
                    Checkout();
                    break;
                case ConsoleKey.L:
                    MainMenu();
                    break;
                case ConsoleKey.S:
                    SelectCurrency();
                    break;
                default:
                    ShopMenu();
                    break;
            }
        }
        #region CustomerToAndFromTxt
        public void CreateCustomerFile()
        {
            if (!File.Exists(CheckFile()))
            {
                using StreamWriter sw = new StreamWriter(CheckFile());
                sw.WriteLine(CustomerList[0]);
                sw.WriteLine(CustomerList[1]);
                sw.WriteLine(CustomerList[2]);
                sw.Close();
            }
        }
        public void SaveCustomerToFile(Customer customer)
        {
            if (File.Exists(CheckFile()))
            {
                using StreamWriter sw = new StreamWriter(CheckFile(), append: true);
                sw.WriteLine(customer);
                sw.Close();
            }
        }
        public List<Customer> ReadCustomerFile()
        {
            if (!File.Exists(CheckFile()))
            {
                return new List<Customer>();
            }
            string? line = "";
            string name = "";
            string password = "";
            string member = "";
            double discount = 0;
            using StreamReader sr = new StreamReader(CheckFile());
            while ((line = sr.ReadLine()) != null)
            {
                if (line.StartsWith("Name:"))
                {
                    name = line.Substring(6);
                }
                else if (line.StartsWith("Password:"))
                {
                    password = line.Substring(10);
                }
                else if (line.StartsWith("Membership:"))
                {
                    member = line.Substring(12);
                }
                else if (line.StartsWith("Discount:"))
                {
                    var discountString = line.Substring(10);
                    double.TryParse(discountString, out discount);
                }
                else if (name is not ("Knatte" or "Tjatte" or "Fnatte"))
                {
                    Customer tempCustomer = new Customer(name, password);
                    tempCustomer.Member = member;
                    tempCustomer.Discount = discount;
                    CustomerList.Add(tempCustomer);
                }
            }
            return CustomerList;
        }
        public string CheckFile()
        {
            var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Anders");
            Directory.CreateDirectory(directory);
            var path = Path.Combine(directory, "CustomerList.txt");
            return path;
        }
    }
    #endregion
}
