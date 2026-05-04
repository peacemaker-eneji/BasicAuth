using BasicAuth.Data.Models;

using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicAuth.Queries.Users.GetUser {
    public record GetUserRequest(int Id) : IRequest<User?>;
}
