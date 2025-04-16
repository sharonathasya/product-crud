using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace backend_product.Models
{
    public class dbContext(DbContextOptions<dbContext> options) : DbContext(options)
    {

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Product> Product { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost;Initial Catalog=dbProduct;User ID=sa;Password=sapassword;multipleactiveresultsets=true;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
            .HasOne(a => a.User)
            .WithOne(u => u.Account)
            .HasForeignKey<Account>(a => a.UserId); // Or whatever your FK is
        }
    }

}
