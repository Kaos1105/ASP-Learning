using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LanguageFeature.Models
{
    public static class ExtentionMethods
    {
        //public static decimal TotalPrices(this ShoppingCart cartParam)
        //{
        //    decimal total = 0;
        //    foreach (Product prod in cartParam.Products)
        //    {
        //        total += prod?.Price ?? 0;
        //    }
        //    return total;
        //}
        public static decimal TotalPrices(this IEnumerable<Product> products)
        {
            decimal total = 0;
            foreach(Product prod in products)
            {
                total += prod?.Price ?? 0;
            }
            return total;
        }
        public static IEnumerable<Product> FilterByPrice(this IEnumerable<Product> products, decimal minPrice)
        {
            foreach(Product prod in products)
            {
                if((prod?.Price ??0) >=minPrice)
                {
                    yield return prod;
                }
            }
        }
        public static IEnumerable<Product> FilterByName(this IEnumerable<Product> productEnum, char firstLetter)
        {
            foreach(Product prod in productEnum)
            {
                if(prod?.Name[0]==firstLetter)
                {
                    yield return prod;
                }
            }
        }
        public static IEnumerable<Product> Filter(this IEnumerable<Product> productEnum, Func<Product, bool> selector)
        {
            foreach(Product prod in productEnum)
            {
                if (selector(prod))
                    yield return prod;
            }
        }
    }
}
