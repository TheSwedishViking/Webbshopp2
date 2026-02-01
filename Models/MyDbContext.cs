using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;
using Webbshopp2.Migrations;

namespace Webbshopp2.Models
{
    internal class MyDbContext : DbContext
    {
        public DbSet<Clothes> Clothes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<KöptaProdukter> KöptaProdukters { get; set; }
        public DbSet<BeställdaProdukter> BeställdaProdukters { get; set; }
        public DbSet<Användare> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:robinsdb.database.windows.net,1433;Initial Catalog=RobinsDb;Persist Security Info=False;User ID=dbadmin;Password=System2025;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }




    }
}
