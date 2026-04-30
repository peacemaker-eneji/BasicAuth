using BasicAuth.api.data;
using BasicAuth.api.dtos.login;
using BasicAuth.api.dtos.signup;
using BasicAuth.api.models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;



// Builder Config
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidation();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSqlite<AppDbContext>("Data Source=Users.db");



// APP Confing
var app = builder.Build();
app.UseHttpsRedirection();
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope()) { // Migrate DB
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}




var hasher = new PasswordHasher<User>();

app.MapPost("/signup", (CreateUserDto userDto) => {
    using (var scope = app.Services.CreateScope()) {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
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


app.MapPost("/login", (LoginDto loginDto) => {
    User? user;
    using (var scope = app.Services.CreateScope()) {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        user = dbContext.Users.FirstOrDefault(x=>x.Email==loginDto.Email);
        if (user is null) return Results.BadRequest("User not found");

        var result = hasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);
        if (result==PasswordVerificationResult.Failed) return Results.BadRequest("Incorrect Password");
    }
    return Results.Ok(new UserInfoDto(user.Firstname, user.Lastname, user.Email));
});

app.Run();
