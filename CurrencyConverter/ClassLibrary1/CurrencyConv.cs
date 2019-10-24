using System;
using System.Collections.Generic;

namespace ClassLibrary1
{
    public class CurrencyConv
    {
        public List<ExchangeRates> exchangeRates;
        public List<Products> products;
        public CurrencyConv()
        {
        }

        public void AddItems(List<Products> product, List<ExchangeRates> rate)
        {
            this.products = product;
            this.exchangeRates = rate;
        }

        public decimal PriceFromEur(decimal price, string targetCurrency)
        {

            foreach (ExchangeRates er in exchangeRates)
            {
                if (er.Currency.ToLower().Equals(targetCurrency.ToLower()))
                {
                    return Math.Round(price * er.Rate, 2);
                }
            }

            return 0.0m;
        }
        public decimal PriceToEur(Products p)
        {
            foreach (ExchangeRates er in exchangeRates)
            {
                if (er.Currency.ToLower().Equals(p.Currency.ToLower()))
                {
                    return Math.Round(p.Price / er.Rate, 2);
                }
            }
            return 0.0m;
        }
    }
}
