using Microsoft.AspNetCore.Mvc;
using Workers.Application.Services.Authentication;
using Workers.Contracts.Authentication;
using Workers.Domain.Common.Errors;

namespace Workers.Api.Controllers;

[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly IAuthenticationService _auth;
    public AuthenticationController(IAuthenticationService auth)
    {
        _auth = auth ?? throw new ArgumentNullException(nameof(auth));
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var result = _auth.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        return result.Match(
            result => Ok(MapToResult(result)),
            errors => Problem(errors)
        );
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var result = _auth.Login(
            request.Email,
            request.Password);

        return result.IsError && result.FirstError == Errors.User.InvalidCredentials
                ? Problem(
                statusCode: StatusCodes.Status401Unauthorized,
                title: result.FirstError.Description
            )
                : result.Match(
            result => Ok(MapToResult(result)),
            errors => Problem(errors)
        );
    }

    private static AuthenticationResponse MapToResult(AuthenticationResult result)
    {
        return new AuthenticationResponse(
                    result.User.Id,
                    result.User.FirstName.Value,
                    result.User.LastName,
                    result.User.Email,
                    result.Token
                );
    }
}
