using Labb2ProgTemplate.Entities;

namespace Labb2ProgTemplate.Entities.CustomerMembership;

public class BronzeCustomer : Customer
{
    public override string Member { get; set; }
    public override double Discount { get; set; }
    public BronzeCustomer(string name, string password, string member, double discount) : base(name, password)
    {
        Member = member;
        Discount = discount;
    }
}

