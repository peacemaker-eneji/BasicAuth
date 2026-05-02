using System;
using System.Collections.Generic;
using System.Text;

namespace BasicAuth.Commands.Auth.Sigup.Dtos {
    public record SignupResponseDto(
        string Firstname,
        string Lastname,
        string Email
    );
}
