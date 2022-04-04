using BulkyBookWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) // We will recieve some options, and pass them to the base class
        {

        }

        public DbSet<Category> Categories { get; set; }  // Create our category tabel
    }
}
