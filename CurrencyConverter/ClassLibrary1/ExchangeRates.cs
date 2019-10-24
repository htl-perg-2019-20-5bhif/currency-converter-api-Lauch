using System;
using System.Globalization;

namespace ClassLibrary1
{
    public class ExchangeRates
    {
        public string Currency { get; set; }

        public decimal Rate { get; set; }
        public ExchangeRates(string Line)
        {
            string[] item = Line.Split(",");
            this.Currency = item[0];
            this.Rate = Decimal.Parse(item[1], CultureInfo.InvariantCulture);
        }
    }
}
