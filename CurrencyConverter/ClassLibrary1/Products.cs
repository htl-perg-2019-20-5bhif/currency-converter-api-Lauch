using System;
using System.Globalization;

namespace ClassLibrary1
{

    public class Products
    {
        public string Description { get; set; }

        public string Currency { get; set; }

        public decimal Price { get; set; }
        public Products(string Line)
        {
            string[] item = Line.Split(",");
            this.Description = item[0];
            this.Currency = item[1];
            this.Price = Decimal.Parse(item[2], CultureInfo.InvariantCulture);
        }

    }
}
