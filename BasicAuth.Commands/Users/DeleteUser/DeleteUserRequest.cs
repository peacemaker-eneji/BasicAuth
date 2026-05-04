using BasicAuth.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicAuth.Commands.Users.DeleteUser {
    public record DeleteUserRequest(int Id) : IRequest<Unit>;

    public class DeleteUserRequestHandler : IRequestHandler<DeleteUserRequest, Unit> {
        private readonly AppDbContext _context;

        public DeleteUserRequestHandler(AppDbContext context) {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteUserRequest request, CancellationToken cancellationToken) {
            var user = await _context.Users.FindAsync(request.Id);
            if (user is not null) {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            return Unit.Value;
        }
    }
}
