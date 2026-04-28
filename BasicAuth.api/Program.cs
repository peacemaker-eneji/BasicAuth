using BasicAuth.api;
using BasicAuth.api.data;
using BasicAuth.api.dtos;
using Microsoft.AspNetCore.Http.HttpResults;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidation();
string connString = "Data Source=Users.db";
builder.Services.AddSqlite<UserDbContext>(connString);

var app = builder.Build();


app.MapPost("/signup", (CreateUserDto userDto) => {
    return userDto;
});


app.Run();
