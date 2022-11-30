using ErrorOr;
using Workers.Application.Common.Interfaces.Authentication;
using Workers.Application.Common.Interfaces.Repositories;
using Workers.Domain.Common.Errors;
using Workers.Domain.Entities;
using Workers.Domain.ValueObjects;

namespace Workers.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJwtGenerator jwtGenerator, IUserRepository userRepository)
    {
        _jwtGenerator = jwtGenerator;
        _userRepository = userRepository;
    }

    public ErrorOr<AuthenticationResult> Login(string email, string password)
    {
        if(_userRepository.GetUserByEmail(email) is not User user)
            return Errors.User.InvalidCredentials;

        if(user.Password != password)
            return Errors.User.InvalidCredentials;

        var token = _jwtGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token
        );
    }

    public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
    {
        if(_userRepository.GetUserByEmail(email) is not null)
            return Errors.User.DuplicateEmail;

        var name = FirstName.Create(firstName);
        if(name.IsError)
        {
            return name.Errors;
        }

        var user = new User(Guid.NewGuid())
        {
            FirstName = name.Value,
            LastName = lastName,
            Email = email,
            Password = password
        };

        _userRepository.Add(user);
        var token = _jwtGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token
        );
    }
}
