using Workers.Domain.Entities;

namespace Workers.Application.Common.Interfaces.Repositories;

public interface IUserRepository
{
    void Add(User user);
    User? GetUserByEmail(string email);
}