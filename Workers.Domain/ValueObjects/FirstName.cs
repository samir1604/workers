using ErrorOr;

using Workers.Domain.Common.Errors;
using Workers.Domain.Common.Models;

namespace Workers.Domain.ValueObjects;

public sealed class FirstName : ValueObject
{
    public const int MaxLength = 30;

    private FirstName(string value)
    {
        Value = value;
    }

    public static ErrorOr<FirstName> Create(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            return Validation.User.EmptyFirstName;
        }

        if(firstName.Length > MaxLength)
        {
            return Validation.User.MaxLength;
        }

        return new FirstName(firstName);
    }

    public string Value { get; }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}