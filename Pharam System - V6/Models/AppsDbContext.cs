using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Pharam_System___V6.Models
{
    public class AppsDbContext : IdentityDbContext
    {
        public AppsDbContext(DbContextOptions<AppsDbContext> options) : base(options)
        {

        }
        DbSet<Category> Categories { get; set; }
        DbSet<Product> Products { get; set; }
    }
}
