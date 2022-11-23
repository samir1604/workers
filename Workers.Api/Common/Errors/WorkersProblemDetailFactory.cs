using System.Diagnostics;

using ErrorOr;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;

using Workers.Api.Common.Constant;

namespace Workers.Api.Common.Errors;

public class WorkersProblemDetailFactory : ProblemDetailsFactory
{
    private readonly ApiBehaviorOptions _options;

    public WorkersProblemDetailFactory(IOptions<ApiBehaviorOptions> options)
    {
        _options = options.Value ?? throw new ArgumentNullException(nameof(options));
    }

    public override ValidationProblemDetails CreateValidationProblemDetails(
        HttpContext httpContext,
        ModelStateDictionary modelStateDictionary,
        int? statusCode = null,
        string? title = null,
        string? type = null,
        string? detail = null,
        string? instance = null)
    {
        if (modelStateDictionary == null)
			{
				throw new ArgumentNullException(nameof(modelStateDictionary));
			}

			statusCode ??= 400;

			var problemDetails = new ValidationProblemDetails(modelStateDictionary)
			{
				Status = statusCode,
				Type = type,
				Detail = detail,
				Instance = instance,
			};

			if (title != null)
			{
				problemDetails.Title = title;
			}

			ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

			return problemDetails;
    }

    public override ProblemDetails CreateProblemDetails(
        HttpContext httpContext,
        int? statusCode = null,
        string? title = null,
        string? type = null,
        string? detail = null,
        string? instance = null)
    {
        statusCode ??= 500;

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Type = type,
            Detail = detail,
            Instance = instance,
        };

        if(title != null)
        {
            problemDetails.Title = title;
        }
        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode);

        return problemDetails;
    }

    private void ApplyProblemDetailsDefaults(
        HttpContext httpContext,
        ProblemDetails problemDetails,
        int? statusCode)
    {
        problemDetails.Status ??= statusCode;

        if(_options.ClientErrorMapping.TryGetValue(statusCode ?? 500, out var clientErrorData))
        {
            problemDetails.Title ??= clientErrorData.Title;
            problemDetails.Type ??= clientErrorData.Link;
        }

        var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
        if (traceId != null)
        {
            problemDetails.Extensions["traceId"] = traceId;
        }

        if (httpContext?.Items[HttpContextKeys.ProblemErrors] is List<Error> errors && errors.Count > 0)
        {
            problemDetails.Extensions.Add("errorCodes", errors.Select(e => e.Code));
        }
    }
}