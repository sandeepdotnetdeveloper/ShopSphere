using ShopSphere.Api.Models;
using System.Xml.Linq;

namespace ShopSphere.Api.Services
{
    public class ProductCatalog
    {
        private readonly List<Product> _products =
        [
            new Product
        {
            Id = 1,
            Name = "Mechanical Keyboard",
            Price = 2499,
            Stock = 20
        },
        new Product
        {
            Id = 2,
            Name = "Wireless Mouse",
            Price = 999,
            Stock = 35
        },
        new Product
        {
            Id = 3,
            Name = "USB-C Hub",
            Price = 1799,
            Stock = 15
        }
        ];

        public List<Product> GetAll()
        {
            return _products;
        }

        public Product? GetById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public Product Add(Product product)
        {
            product.Id = _products.Count == 0
                ? 1
                : _products.Max(p => p.Id) + 1;

            _products.Add(product);

            return product;
        }

        public bool Delete(int id)
        {
            var product = GetById(id);

            if (product is null)
                return false;

            _products.Remove(product);

            return true;
        }
    }
}
