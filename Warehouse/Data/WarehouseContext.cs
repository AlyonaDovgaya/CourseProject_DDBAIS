using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Warehouse.Models;

namespace Warehouse.Data
{
    public class WarehouseContext: DbContext
    {
        public WarehouseContext(DbContextOptions <WarehouseContext> options) : base(options)
        {
        }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Dialer> Dialers { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }

    }
}
