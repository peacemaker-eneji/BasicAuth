using BasicAuth.Data;
using BasicAuth.Data.Models;

using MediatR;

namespace BasicAuth.Queries.Users.GetUser {
    public record GetUserRequest(int Id) : IRequest<User?>;

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
