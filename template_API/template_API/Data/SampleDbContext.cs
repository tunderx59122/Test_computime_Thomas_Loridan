using Microsoft.EntityFrameworkCore;
using template_API.Models;

namespace template_API.Data
{
    public class SampleDbContext : DbContext
    {
        public SampleDbContext(DbContextOptions<SampleDbContext> options): base(options) { 
        }

        public DbSet<Sample> Samples { get; set; } // Ajoutez cette ligne


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public bool Save()
        {
            return this.SaveChanges() != 0;
        }
    }
}
