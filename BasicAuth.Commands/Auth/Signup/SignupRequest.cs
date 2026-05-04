using BasicAuth.Commands.Users.CreateUser;
using BasicAuth.Data;
using BasicAuth.Data.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BasicAuth.Commands.Auth.Signup {
    public record SignupRequest(string Firstname, string Lastname, string Email, string Password) : IRequest<bool>;

    public class SignupRequestHandler : IRequestHandler<SignupRequest, bool> {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<User> _hasher = new PasswordHasher<User>();
        private readonly IMediator _mediator;

        public SignupRequestHandler(AppDbContext context, IMediator mediator) {
            _context = context;
            _mediator = mediator;
        }

        public async Task<bool> Handle(SignupRequest request, CancellationToken cancellationToken) {
            return await _mediator.Send(new CreateUserRequest(request.Firstname, request.Lastname, request.Email, request.Password));
        }
    }
}

