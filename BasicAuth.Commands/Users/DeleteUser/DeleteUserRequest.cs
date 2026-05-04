using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicAuth.Commands.Users.DeleteUser {
    public record DeleteUserRequest(int Id) : IRequest<Unit>;
}
