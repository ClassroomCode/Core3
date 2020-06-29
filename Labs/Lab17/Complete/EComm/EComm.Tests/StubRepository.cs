using EComm.Data;
using EComm.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EComm.Tests
{
    public class StubRepository : IRepository
    {
        public Task<IEnumerable<Product>> GetAllProducts(bool includeSuppliers = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Supplier>> GetAllSuppliers()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProduct(int id, bool includeSuppliers = false)
        {
            var product = new Product {
                Id = 1,
                ProductName = "Bread",
                UnitPrice = 15.00M,
                Package = "Bag",
                IsDiscontinued = false,
                SupplierId = 1
            };
            if (includeSuppliers) {
                product.Supplier = new Supplier {
                    Id = 1,
                    CompanyName = "Acme, Inc."
                };
            }
            return Task.Run(() => product);
        }

        public Task SaveProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
