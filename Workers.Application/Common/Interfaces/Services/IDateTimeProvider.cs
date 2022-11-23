using System;
namespace Workers.Application.Common.Interfaces.Services;

public interface IDateTimeProvider {
    DateTime UtcNow { get; }
}