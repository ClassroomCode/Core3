using EComm.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EComm.Data
{
    public interface IRepository
    {
        Task<IEnumerable<Product>> GetAllProducts(bool includeSuppliers = false);
        Task<Product> GetProduct(int id, bool includeSupplier = false);
        Task<IEnumerable<Supplier>> GetAllSuppliers();
    }
}
