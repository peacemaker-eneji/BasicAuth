using BasicAuth.Commands.Auth.Login.Dtos;
using BasicAuth.Data;
using BasicAuth.Data.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicAuth.Commands.Auth.Signup {
    public class SignupRequestHandler : IRequestHandler<SignupRequest, bool> {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<User> _hasher = new PasswordHasher<User>();

        public SignupRequestHandler(AppDbContext context) {
            _context = context;
        }

        public async Task<bool> Handle(SignupRequest request, CancellationToken cancellationToken) {
            bool exists = _context.Users.Any(x => x.Email == request.Email);
            if (exists) return false;

            User user = new User {
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                Email = request.Email
            };

            user.PasswordHash = _hasher.HashPassword(user, request.Password);
            _context.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
