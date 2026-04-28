using System;
using Microsoft.EntityFrameworkCore;

namespace BasicAuth.api.data;

public static class DataExtensions {
    public static void MigrateDB(this WebApplication app) {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();
        dbContext.Database.Migrate();
    }
}
