using System;
using System.ComponentModel.DataAnnotations;

namespace BasicAuth.api.dtos;

public record CreateUserDto(
    [Required] string Firstname, 
    [Required] string Lastname,
    [Required] string Email,
    [Required] string Password
);