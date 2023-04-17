using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApp.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; } //add-migration addCategoryToDataBase => update-database

        private DbSet<CoverType> CoverTypes { get; set; }

    }
}
