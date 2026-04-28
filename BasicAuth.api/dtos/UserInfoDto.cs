using System;
using System.ComponentModel.DataAnnotations;

namespace BasicAuth.api.dtos;

public record UserInfoDto(
    [Required] string Firstname,
    [Required] string Lastname,
    [Required] string Email
);
