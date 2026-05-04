using BasicAuth.Data;
using BasicAuth.Data.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BasicAuth.Queries.Users.GetUsers {
    public record GetUsersRequest() : IRequest<List<User>>;
    public class GetUsersRequestHandler : IRequestHandler<GetUsersRequest, List<User>> {
        private readonly AppDbContext _context;

        public GetUsersRequestHandler(AppDbContext context) {
            _context = context;
        }

        public async Task<List<User>> Handle(GetUsersRequest request, CancellationToken cancellationToken) {
            return await _context.Users.ToListAsync();
        }
    }
}
