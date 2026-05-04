using BasicAuth.Data;
using BasicAuth.Data.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicAuth.Queries.Users.GetUser {
    public class GetUserRequestHandler : IRequestHandler<GetUserRequest, User?> {
        private readonly AppDbContext _context;

        public GetUserRequestHandler(AppDbContext context) {
            _context = context;
        }
        public async Task<User?> Handle(GetUserRequest request, CancellationToken cancellationToken) {
            var user = await _context.Users.FindAsync(request.Id);
            if (user is null) return null;
            return user;
        }
    }
}
