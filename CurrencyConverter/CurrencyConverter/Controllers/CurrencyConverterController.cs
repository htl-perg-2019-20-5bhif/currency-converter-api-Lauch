using ClassLibrary1;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyConverter.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class CurrencyConverterController : ControllerBase
    {
        readonly List<ExchangeRates> exchangeRates = System.IO.File.ReadAllLines("ExchangeRates.csv").Skip(1).Select(line => new ExchangeRates(line)).ToList();
        readonly List<Products> products = System.IO.File.ReadAllLines("Prices.csv").Skip(1).Select(line => new Products(line)).ToList();
        CurrencyConv currC = new CurrencyConv();


        [HttpGet]
        public IActionResult GetAllItems()
        {
            return Ok(products);
        }

        [HttpGet]
        [Route("{product}/price")]
        public IActionResult GetProduct([FromRoute] string product, [FromQuery] string targetCurrency)
        {
            currC.AddItems(products, exchangeRates);
            decimal price = 0.0M;
            foreach (Products p in products)
            {
                if (p.Description.ToLower().Equals(product.ToLower()))
                {
                    if (p.Currency.ToLower().Equals(targetCurrency.ToLower()))
                    {
                        return Ok("price: " + p.Price + " in " + p.Currency);
                    }
                    else
                    {
                        if (!targetCurrency.ToLower().Equals("eur".ToLower()))
                        {
                            price = currC.PriceFromEur(p.Price, targetCurrency);
                        }
                        if (!p.Currency.ToLower().Equals("eur".ToLower()))
                        {
                            price = currC.PriceToEur(p);
                        }
                        return Ok("price in " + targetCurrency + ": " + price);
                    }
                }
            }
            return BadRequest("Invalid or missing name");
        }

        /*public decimal PriceFromEur(decimal price, string targetCurrency)
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
        }*/
    }
}
