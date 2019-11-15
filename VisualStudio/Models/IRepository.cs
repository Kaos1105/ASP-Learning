using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisualStudio.Models;

namespace VisualStudio_UnitTesting.Models
{
    public interface IRepository
    {
        IEnumerable<Product> Products { get; }
        void AddProduct(Product p);
    }
}
