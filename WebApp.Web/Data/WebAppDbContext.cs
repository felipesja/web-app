using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Web.Domain;

namespace WebApp.Web.Data;

public class WebAppDbContext : IdentityDbContext<IdentityUser>
{
    public WebAppDbContext(DbContextOptions<WebAppDbContext> options) : base(options)
    {
    }
    // Registered DB Model in WebAppDbContext file
    public DbSet<Product> Products { get; set; }

    /*
     OnModelCreating mainly used to configured our EF model
     And insert master data if required
    */
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasSequence<int>("ProductSequence", schema: "dbo")
            .StartsAt(2)
            .IncrementsBy(1);

        modelBuilder.Entity<Product>()
            .Property(p => p.Id)
            .HasDefaultValueSql("NEXT VALUE FOR dbo.ProductSequence");

        modelBuilder.Entity<Product>().HasKey(x => x.Id);
        modelBuilder.Entity<Product>().Property(x => x.Price).HasPrecision(18, 4);        
    }
}
