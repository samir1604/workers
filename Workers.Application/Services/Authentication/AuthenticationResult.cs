using Workers.Domain.Entities;

namespace Workers.Application.Services.Authentication;

public record AuthenticationResult (
    User User,
    string Token
);