using BasicAuth.Data;
using BasicAuth.Data.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicAuth.Queries.Users.GetUser {
    public class GetUserIdRequestHandler : IRequestHandler<GetUserIdRequest, int?> {
        private readonly AppDbContext _context;

        public GetUserIdRequestHandler(AppDbContext context) {
            _context = context;
        }
        public async Task<int?> Handle(GetUserIdRequest request, CancellationToken cancellationToken) {
            var user = await _context.Users.FirstOrDefaultAsync(e => e.Email == request.Email);
            if (user is null) return null;
            return user.Id;
        }
    }
}
