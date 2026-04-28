using BasicAuth.api;
using BasicAuth.api.data;
using BasicAuth.api.dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidation();

string connString = "Data Source=Users.db";
builder.Services.AddSqlite<UserDbContext>(connString);


var app = builder.Build();

using (var scope = app.Services.CreateScope()) {
    var dbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();
    dbContext.Database.Migrate();
}

var hasher = new PasswordHasher<User>();

app.MapPost("/signup", (CreateUserDto userDto) => {
    using (var scope = app.Services.CreateScope()) {
        var dbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();
        
        bool exists = dbContext.Users.Any(x=>x.Email==userDto.Email);
        if (exists) return Results.BadRequest("Email already exists");

        User user = new User{
            Firstname = userDto.Firstname,
            Lastname = userDto.Lastname,
            Email = userDto.Email
        };

        user.PasswordHash = hasher.HashPassword(user, userDto.Password);
        dbContext.Add(user);
        dbContext.SaveChanges();
    }
    return Results.Ok(new UserInfoDto(userDto.Firstname, userDto.Lastname, userDto.Email));
});


app.Run();
