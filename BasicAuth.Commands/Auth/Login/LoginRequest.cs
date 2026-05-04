using BasicAuth.Commands.Auth.Login.Dtos;
using BasicAuth.Data;
using BasicAuth.Data.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BasicAuth.Commands.Auth.Login {
    public record LoginRequest(string Email, string Password) : IRequest<LoginResponseDto?>;

    public class LoginRequestHandler : IRequestHandler<LoginRequest, LoginResponseDto?> {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<User> _hasher = new PasswordHasher<User>();

        public LoginRequestHandler(AppDbContext context) {
            _context = context;
        }

        public async Task<LoginResponseDto?> Handle(LoginRequest request, CancellationToken cancellationToken) {
            User? user = _context.Users.FirstOrDefault(x => x.Email == request.Email);
            if (user is null) return null;

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (result == PasswordVerificationResult.Failed) return null;

            return new LoginResponseDto(user.Firstname, user.Lastname, user.Email, GenerateJwtToken(user));
        }
        private string GenerateJwtToken(User user) {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET_KEY")!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "localhost",
                audience: "user",
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
