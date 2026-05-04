using BasicAuth.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BasicAuth.Queries.Users.GetUser {
    public record GetUserIdRequest(string Email) : IRequest<int?>;
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
