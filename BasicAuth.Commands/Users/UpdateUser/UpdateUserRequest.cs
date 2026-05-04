using BasicAuth.Data.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BasicAuth.Commands.Users.UpdateUser {
    public record UpdateUserRequest(string? Firstname, string? Lastname, [Required] string Email) : IRequest<User?>;
}
