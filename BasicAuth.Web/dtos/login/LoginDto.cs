using System;
using System.ComponentModel.DataAnnotations;

namespace BasicAuth.api.dtos.login;

public record LoginDto(
    [Required] string Email,
    [Required] string Password
);
