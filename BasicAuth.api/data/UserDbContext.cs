using System;
using BasicAuth.api;
using Microsoft.EntityFrameworkCore;

namespace BasicAuth.api.data;

public class UserDbContext(DbContextOptions<UserDbContext> options) : DbContext(options) {
    public DbSet<User> Users => Set<User>();
}
