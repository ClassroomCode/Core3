﻿using EComm.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EComm.Data.EF
{
    public class ECommDataContext : DbContext, IRepository
    {
        public ECommDataContext(DbContextOptions options)
          : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        public async Task<IEnumerable<Product>> GetAllProducts(bool includeSuppliers = false)
        {
            return includeSuppliers switch
            {
                false => await Products.ToListAsync(),
                true => await Products.FromSqlRaw("SELECT [p].[Id], [p].[IsDiscontinued], [p].[Package], [p].[ProductName], [p].[SupplierId], [p].[UnitPrice], [s].[CompanyName] AS SupplierName FROM [Products] AS[p] INNER JOIN [Suppliers] AS[s] ON[p].[SupplierId] = [s].[Id]")
                    .ToListAsync()
            };
        }

        public async Task<Product> GetProduct(int id, bool includeSupplier = false)
        {
            return includeSupplier switch
            {
                false => await Products.SingleOrDefaultAsync(p => p.Id == id),
                true => await Products.Include(p => p.Supplier)
                          .SingleOrDefaultAsync(p => p.Id == id)
            };
        }
    }
}
