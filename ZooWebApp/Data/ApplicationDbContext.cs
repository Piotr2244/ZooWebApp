using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZooWebApp.Models;

namespace ZooWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ZooWebApp.Models.Animal>? Animal { get; set; }
        public DbSet<ZooWebApp.Models.Snack>? Snack { get; set; }
    }
}