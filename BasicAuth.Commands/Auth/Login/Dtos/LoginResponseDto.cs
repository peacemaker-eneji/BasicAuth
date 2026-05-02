using System;
using System.Collections.Generic;
using System.Text;

namespace BasicAuth.Commands.Auth.Login.Dtos {
    public record LoginResponseDto(
        string Firstname,
        string Lastname,
        string Email,
        string Token
    );
}
