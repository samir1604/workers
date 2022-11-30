using Workers.Domain.Common.Models;
using Workers.Domain.ValueObjects;

namespace Workers.Domain.Entities;

public sealed class User : Entity
{
    public User(Guid id) : base(id)  { }

    public FirstName FirstName { get; set; }

    public string LastName {get; set;} = "";
    public string Email {get; set;} = "";
    public string Password {get; set;} = "";
}