using System;
using BasicAuth.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BasicAuth.api.data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) {
    public DbSet<User> Users { get; set; }
}
