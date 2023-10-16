using AccountingWebApi.Data.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Diagnostics.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingWebApi.Data.Models;

namespace AccountingWebApi.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext([NotNull] DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Expenses> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
        {
            public AppDbContext CreateDbContext(string[] args)
            {
                var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Accounting;Encrypt=True;Trusted_Connection=True;TrustServerCertificate=true;");

                return new AppDbContext(optionBuilder.Options);
            }
        }

    }
}
