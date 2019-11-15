using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LanguageFeature.Models;
using Microsoft.AspNetCore.Mvc;

namespace LanguageFeature.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View(AnonymousTypes());
        }
        //public async Task<ViewResult> Index()
        //{
        //    long? length = await AsyncMethod.GetPageLength();
        //    return View(new string[] { $"Length: {length}" });
        //}
        public List<string> ConditionalAndCoalescing()
        {
            List<string> results = new List<string>();
            foreach (Product p in Product.GetProducts())
            {
                string name = p?.Name ?? "<No Name>";
                decimal? price = p?.Price ?? 0;
                string relatedName = p?.Related?.Name ?? "<None>";
                results.Add(string.Format("Name: {0}, Price: {1}, Related: {2}",
                name, price, relatedName));
            }
            return results;
        }
        public List<string> StringInterpolation()
        {
            List<string> results = new List<string>();
            foreach (Product p in Product.GetProducts())
            {
                string name = p?.Name ?? "<No Name>";
                decimal? price = p?.Price ?? 0;
                string relatedName = p?.Related?.Name ?? "<None>";
                results.Add($"Name: {name}, Price: {price}, Related: {relatedName}");
            }
            return results;
        }
        public List<string> IndexIntializer()
        {
            Dictionary<string, Product> products = new Dictionary<string, Product>
            {
                ["Kayak"] = new Product { Name = "Kayak", Price = 275M },
                ["Lifejacket"] = new Product { Name = "Lifejacket", Price = 48.95M }
            };
            return new List<string>(products.Keys.ToArray()){};
        }
        public List<string> SwitchPatternMatching()
        {
            object[] data = new object[] { 275M, 29.95M,"apple", "orange", 100, 10 };
            decimal total = 0;
            for (int i = 0; i < data.Length; i++)
            {
                switch (data[i])
                {
                    case decimal decimalValue:
                        total += decimalValue;
                        break;
                    case int intValue when intValue > 50:
                        total += intValue;
                        break;
                }
            }
            return new List<string>( new string[] { $"Total: {total:C2}" }){ };
        }
        public List<string> ExtentionMethodInterface()
        {
            ShoppingCart cart= new ShoppingCart { Products = Product.GetProducts() };
            Product[] productArray = {
                new Product {Name = "Kayak", Price = 275M},
                new Product {Name = "Lifejacket", Price = 48.95M} };
            decimal cartTotal = cart.TotalPrices();
            decimal arrayTotal = productArray.TotalPrices();
            return new List<string>(new string[] {$"Cart Total: {cartTotal:C2}",$"Array Total: {arrayTotal:C2}" });
        }
        public List<string> ExtentionMethodInterfaceWithFilter()
        {
            Product[] productArray = { 
                new Product {Name = "Kayak", Price = 275M},
                new Product {Name = "Lifejacket", Price = 48.95M},
                new Product {Name = "Soccer ball", Price = 19.50M},
                new Product {Name = "Corner flag", Price = 34.95M}
            };
            decimal arrayTotal = productArray.FilterByPrice(20).TotalPrices();
            return new List<string>(new string[] { $"Array Total: {arrayTotal:C2}" });
        }
        public List<string> LambdaExpression()
        {
            Product[] productArray = {
                new Product {Name = "Kayak", Price = 275M},
                new Product {Name = "Lifejacket", Price = 48.95M},
                new Product {Name = "Soccer ball", Price = 19.50M},
                new Product {Name = "Corner flag", Price = 34.95M}
            };
            decimal priceFilterTotal = productArray.Filter(p => (p?.Price ?? 0) >= 20).TotalPrices();
            decimal nameFilterTotal = productArray.Filter(p => p?.Name?[0] == 'S').TotalPrices();
            return new List<string>(new string[] { $"Price Total: {priceFilterTotal:C2}",$"Name Total: {nameFilterTotal:C2}" });
        }
        public List<string> Lambda()
        {
            return new List<string>(Product.GetProducts().Select(p=>p?.Name??"<None>"));
        }
        public List<string> AnonymousTypes()
        {
            var products = new []{
                new {Name = "Kayak", Price = 275M},
                new {Name = "Lifejacket", Price = 48.95M},
                new {Name = "Soccer ball", Price = 19.50M},
                new {Name = "Corner flag", Price = 34.95M}
            };
            return new List<string>(products.Select(p => p?.Name));
            // return new List<string>(products.Select(p => p.GetType().Name));
        }
        public List<string> GettingName()
        {
            var products = new[] {
            new { Name = "Kayak", Price = 275M },
            new { Name = "Lifejacket", Price = 48.95M },
            new { Name = "Soccer ball", Price = 19.50M },
            new { Name = "Corner flag", Price = 34.95M }
            };
            return new List<string>(products.Select(p =>$"{nameof(p.Name)}: {p.Name}, {nameof(p.Price)}: {p.Price}"));
        }
    }
}
