using JsonApiDotNetCore.Demo.Auth.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace JsonApiDotNetCore.Demo.Auth.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Car> Car { get; set; }
        
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Car>(b =>
            {
                b.HasKey(c => c.Id);

                b.Property(c => c.Model).HasMaxLength(25).IsRequired();
                b.Property(c => c.Brand).HasMaxLength(25).IsRequired();
                b.Property(c => c.Released).IsRequired();
            });
            
            base.OnModelCreating(builder);
        }
    }
}
