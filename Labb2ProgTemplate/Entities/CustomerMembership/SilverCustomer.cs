using Labb2ProgTemplate.Entities;

namespace Labb2ProgTemplate.Entities.CustomerMembership;

public class SilverCustomer : Customer
{
    public override string Member { get; set; }
    public override double Discount { get; set; }
    public SilverCustomer(string name, string password, string member, double discount) : base(name, password)
    {
        Member = member;
        Discount = discount;
    }
}

