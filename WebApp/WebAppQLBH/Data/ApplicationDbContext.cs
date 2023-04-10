using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using WebAppQLBH.Models;

namespace WebAppQLBH.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
        }
        public DbSet<Category > Categories { get; set; }
    }
}
