using Workers.Application.Common.Interfaces.Services;

namespace Workers.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}