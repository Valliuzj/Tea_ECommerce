using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class AppDbContext: IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options){
    }
    public DbSet<Blog>? blogs {get; set;}
    public DbSet<Product> products { get; set; }
}
