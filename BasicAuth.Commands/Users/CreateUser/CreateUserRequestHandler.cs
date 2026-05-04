using BasicAuth.Commands.Auth.Signup;
using BasicAuth.Data;
using BasicAuth.Data.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicAuth.Commands.Users.CreateUser {
    public class CreateUserRequestHandler : IRequestHandler<CreateUserRequest, bool> {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<User> _hasher = new PasswordHasher<User>();

        public CreateUserRequestHandler(AppDbContext context) {
            _context = context;
        }

        public async Task<bool> Handle(CreateUserRequest request, CancellationToken cancellationToken) {
            if (await _context.Users.AnyAsync(e => e.Email == request.Email)) return false;
            User user = new User {
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                Email = request.Email
            };

            user.PasswordHash = _hasher.HashPassword(user, request.Password);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
