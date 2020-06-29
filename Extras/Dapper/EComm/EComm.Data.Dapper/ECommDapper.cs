using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using EComm.Data.Entities;

namespace EComm.Data.Dapper
{
    public class ECommDapper : IRepository
    {
        private readonly string _connStr;

        public ECommDapper(string connStr)
        {
            _connStr = connStr;
        }

        public async Task<IEnumerable<Product>> GetAllProducts(bool includeSuppliers = false)
        {
            string sql = includeSuppliers switch
            {
                false => "SELECT * FROM Products",
                true => "SELECT [p].[Id], [p].[IsDiscontinued], [p].[Package], [p].[ProductName], " +
                        "[p].[SupplierId], [p].[UnitPrice], [s].[Id] AS SId, [s].[City], " +
                        "[s].[CompanyName], [s].[ContactName], [s].[Country], [s].[Fax], [s].[Phone] " +
                        "FROM[Products] AS [p] INNER JOIN [Suppliers] AS[s] ON [p].[SupplierId] = [s].[Id]"
            };
            using IDbConnection db = new SqlConnection(_connStr);

            if (!includeSuppliers) {
                var products = await db.QueryAsync<Product>(sql);
                return products.ToList();
            }
            var results = await db.QueryAsync(sql);
            // mapping could can be made simpler by using a 3rd-party mapping library
            var retVal = new List<Product>();
            foreach (var result in results) {
                retVal.Add(new Product {
                    Id = result.Id,
                    ProductName = result.ProductName,
                    UnitPrice = result.UnitPrice,
                    Package = result.Package,
                    IsDiscontinued = result.IsDiscontinued,
                    SupplierId = result.SupplierId,
                    Supplier = new Supplier {
                        Id = result.SId,
                        City = result.City,
                        CompanyName = result.CompanyName,
                        ContactName = result.ContactName,
                        Country = result.Country,
                        Fax = result.Country,
                        Phone = result.Phone
                    }
                });
            }
            return retVal;
        }

        public async Task<IEnumerable<Supplier>> GetAllSuppliers()
        {
            string sql = "SELECT * FROM Suppliers";   
            using IDbConnection db = new SqlConnection(_connStr);
            var suppliers = await db.QueryAsync<Supplier>(sql);
            return suppliers.ToList();
        }

        public async Task<Product> GetProduct(int id, bool includeSuppliers = false)
        {
            string sql = includeSuppliers switch
            {
                false => "SELECT * FROM Products WHERE [p].Id=@Id",
                true => "SELECT [p].[Id], [p].[IsDiscontinued], [p].[Package], [p].[ProductName], " +
                        "[p].[SupplierId], [p].[UnitPrice], [s].[Id] AS SId, [s].[City], " +
                        "[s].[CompanyName], [s].[ContactName], [s].[Country], [s].[Fax], [s].[Phone] " +
                        "FROM [Products] AS [p] INNER JOIN [Suppliers] AS[s] ON [p].[SupplierId] = [s].[Id] " +
                        "WHERE [p].Id=@Id"
            };
            using IDbConnection db = new SqlConnection(_connStr);

            if (!includeSuppliers) {
                var products = await db.QueryAsync<Product>(sql, new { Id = id });
                return products.SingleOrDefault();
            }
            var results = await db.QueryAsync(sql, new { Id = id });
            // mapping could be made simpler by using a 3rd-party mapping library
            var retVal = new List<Product>();
            foreach (var result in results) {
                retVal.Add(new Product {
                    Id = result.Id,
                    ProductName = result.ProductName,
                    UnitPrice = result.UnitPrice,
                    Package = result.Package,
                    IsDiscontinued = result.IsDiscontinued,
                    SupplierId = result.SupplierId,
                    Supplier = new Supplier {
                        Id = result.SId,
                        City = result.City,
                        CompanyName = result.CompanyName,
                        ContactName = result.ContactName,
                        Country = result.Country,
                        Fax = result.Country,
                        Phone = result.Phone
                    }
                });
            }
            return retVal.SingleOrDefault();
        }

        public async Task SaveProduct(Product product)
        {
            string sql = "UPDATE Products SET " +
                         "ProductName=@ProductName, " +
                         "UnitPrice=@UnitPrice, " +
                         "Package=@Package, " +
                         "IsDiscontinued=@IsDiscontinued, " +
                         "SupplierId=@SupplierId " +
                         "WHERE Id=@Id";

            using IDbConnection db = new SqlConnection(_connStr);
            await db.ExecuteAsync(sql, new {
                Id = product.Id,
                ProductName = product.ProductName,
                UnitPrice = product.UnitPrice,
                Package = product.Package,
                IsDiscontinued = product.IsDiscontinued,
                SupplierId = product.SupplierId
            });
        }
    }
}
