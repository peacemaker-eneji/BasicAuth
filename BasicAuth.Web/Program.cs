using BasicAuth.Commands.Auth.Login;
using BasicAuth.Data;
using BasicAuth.Queries.Users.GetUser;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;


Env.Load();


string host_url = "localhost:7140";
var handlerAssemblies = new[] {
    typeof(LoginRequest).Assembly,
    typeof(GetUserRequest).Assembly
};



// Builder Config
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidation();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSqlite<AppDbContext>(Environment.GetEnvironmentVariable("DbConnection")!);
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "localhost",
        ValidAudience = "user",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET_KEY")!)) // @peacemaker-eneji TODO
    };
});
builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies(handlerAssemblies));
builder.Services.AddCors(options => options.AddPolicy("AllowSpecificOrigin", policy =>
            policy.WithOrigins("https://localhost:7140")
                  .AllowAnyHeader()
                  .AllowAnyMethod()));



//// APP Confing
var app = builder.Build();
app.UseCors("AllowSpecificOrigin");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope()) { // Migrate DB
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}


app.Run();