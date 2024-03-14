using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Labb2ProgTemplate.Entities
{
    public record Product(string _name, double _price, double _basePrice, string _currency)
    {
        public string Name { get; set; } = _name;
        public double Price { get; set; } = _price;
        public double BasePrice { get; set; } = _basePrice;
        public string Currency { get; set; } = _currency;
        public override string ToString()
        {
            var output = string.Empty;
            output += "Name: " + Name + "\n";
            output += "Price: " + BasePrice + "\n";

            return output;
        }
    }

}
