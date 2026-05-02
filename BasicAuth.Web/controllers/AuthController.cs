using BasicAuth.Commands.Auth.Login;
using BasicAuth.Commands.Auth.Login.Dtos;
using BasicAuth.Commands.Auth.Signup;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace BasicAuth.Commands.Auth {

    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase {
        public readonly IMediator _mediator;

        public AuthController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginRequest command) {
            var response = await _mediator.Send(command);
            if (response is null) return Unauthorized();
            return Ok(response);
        }

        [HttpPost("signup")]
        public async Task<ActionResult> Signup(SignupRequest command) {
            bool created = await _mediator.Send(command);
            if (created) return Ok(new { message = "Created Successfully" });
            return Conflict(new { error = "Email Already Exists" });
        }
    }
}
