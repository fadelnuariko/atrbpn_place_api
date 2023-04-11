using Microsoft.EntityFrameworkCore;
using PlacesAPI.Models;

namespace PlacesAPI.Data
{
    public class PlacesDbContext : DbContext
    {
        public PlacesDbContext(DbContextOptions<PlacesDbContext> options) : base(options) { }
        
        public DbSet<Place> Places { get; set; }
    }
    
}