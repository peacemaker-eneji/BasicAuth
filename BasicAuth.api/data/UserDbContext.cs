using System;
using BasicAuth.api;
using Microsoft.EntityFrameworkCore;

namespace BasicAuth.api.data;

public class UserDbContext : DbContext {
    UserDbContext(DbContextOptions<UserDbContext> options) : base(options) {}

    public DbSet<User> Users => Set<User>();
}
