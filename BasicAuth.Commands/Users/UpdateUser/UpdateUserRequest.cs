using BasicAuth.Data;
using BasicAuth.Data.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BasicAuth.Commands.Users.UpdateUser {
    public record UpdateUserRequest(string? Firstname, string? Lastname, [Required] string Email) : IRequest<User?>;

    public class UpdateUserRequestHandler : IRequestHandler<UpdateUserRequest, User?> {
        private readonly AppDbContext _context;

        public UpdateUserRequestHandler(AppDbContext context) {
            _context = context;
        }

        public async Task<User?> Handle(UpdateUserRequest request, CancellationToken cancellationToken) {
            var user = await _context.Users.FirstOrDefaultAsync(e => e.Email == request.Email);
            if (user is null) return null;

            if (request.Firstname is not null) user.Firstname = request.Firstname;
            if (request.Lastname is not null) user.Lastname = request.Lastname;
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
