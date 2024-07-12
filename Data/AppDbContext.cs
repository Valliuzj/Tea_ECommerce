using Microsoft.EntityFrameworkCore;
public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options){
    }
    public DbSet<Blog>? blogs {get; set;}
    public DbSet<Product> products { get; set; }
}
