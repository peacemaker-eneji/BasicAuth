using BasicAuth.Commands.Auth.Login.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicAuth.Commands.Auth.Signup {
    public record SignupRequest(string Firstname, string Lastname, string Email, string Password) : IRequest<bool>;
}
