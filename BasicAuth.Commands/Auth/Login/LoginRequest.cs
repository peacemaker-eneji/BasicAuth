using BasicAuth.Commands.Auth.Login.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicAuth.Commands.Auth.Login {
    public record LoginRequest(string Email, string Password) : IRequest<LoginResponseDto?>;
}
