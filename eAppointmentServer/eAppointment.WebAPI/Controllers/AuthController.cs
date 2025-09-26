
using eAppointment.Application.Auth.Login;
using eAppointment.WebAPI.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TS.MediatR;
using TS.Result;

namespace eAppointment.WebAPI.Controllers;
public sealed class AuthController(ISender sender) : ApiController(sender)
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginCommand request, CancellationToken cancellationToken)
    {
        Result<LoginCommandResponse> response = await _sender.Send(request, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }
    
}