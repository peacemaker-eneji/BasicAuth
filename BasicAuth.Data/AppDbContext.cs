using BasicAuth.Data.Models;
using Microsoft.EntityFrameworkCore;


namespace BasicAuth.Data;
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) {
    public DbSet<User> Users { get; set; }
}
