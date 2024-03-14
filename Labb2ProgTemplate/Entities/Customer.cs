namespace Labb2ProgTemplate.Entities;

public class Customer
{
    public string Name { get; private set; }
    private string Password { get; set; }
    public virtual string Member { get; set; }
    public virtual double Discount { get; set; }

    private List<Product> _cart;
    public List<Product> Cart { get { return _cart; } }

    public Customer(string name, string password)
    {
        Name = name;
        Password = password;
        _cart = new List<Product>();
    }
    public override string ToString()
    {
        var output = string.Empty;
        output += ("Name: " + Name + "\n");
        output += ("Password: " + Password + "\n");
        output += ("Membership: " + Member + "\n");
        output += ("Discount: " + Discount + "\n");
        foreach (var product in Cart)
        {
            output += product.ToString();
            Console.WriteLine("/n");
        }
        return output;
    }
    public bool CheckPassword(string password)
    {
        while (password != Password)
        {
            if (password == Password)
            {
                return true;
            }

            if (password != Password)
            {

                Console.WriteLine("Wrong password.");
                Console.Write("Password : ");
                password = Console.ReadLine();
            }
        }
        return true;
    }
}