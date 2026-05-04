using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicAuth.Commands.Users.CreateUser {

    public record CreateUserRequest(string Firstname, string Lastname, string Email, string Password) : IRequest<bool>;
}
