using Workers.Domain.Entities;

namespace Workers.Application.Common.Interfaces.Authentication;

public interface  IJwtGenerator
{
    string GenerateToken(User user);
}