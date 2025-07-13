using System.Data.Common;
using System.Net;
using System.Net.Mime;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace StudentProgressTracker.Api.Middleware;

public sealed class ExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        httpContext.Response.ContentType = MediaTypeNames.Application.Json;

        switch (exception)
        {
            case ValidationException:
                await HandleValidationException(httpContext, exception, cancellationToken);
                break;
            case DbException
            or DbUpdateException:
                await HandleDatabaseException(httpContext, exception, cancellationToken);
                break;
            default:
                await HandleException(httpContext, exception, cancellationToken);
                break;
        }

        return true;
    }

    private static async Task HandleException(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        var problemDetails = new ProblemDetails
        {
            Title = nameof(Exception),
            Detail = exception.Message,
            Status = httpContext.Response.StatusCode,
        };

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
    }

    private static async Task HandleValidationException(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        var validationException = (ValidationException)exception;

        var validationErrors = validationException
            .Errors.ToLookup(validationFailure => validationFailure.PropertyName)
            .ToDictionary(
                x => $"property: {x.Key}",
                x =>
                    x.Select(validationFailure => new
                        {
                            validationFailure.ErrorMessage,
                            ErrorCode = validationFailure.ErrorCode == "NotEmptyValidator"
                                ? string.Empty
                                : validationFailure.ErrorCode,
                            Value = validationFailure.AttemptedValue,
                        })
                        .ToList()
            );

        var extensions = new Dictionary<string, object>
        {
            ["Validation Errors"] = validationErrors,
        };

        var problemDetails = new ProblemDetails
        {
            Title = nameof(ValidationException),
            Status = httpContext.Response.StatusCode,
            Extensions = extensions!,
        };

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
    }

    private static async Task HandleDatabaseException(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        var problemDetails = new ProblemDetails
        {
            Title = exception is DbException ? nameof(DbException) : nameof(DbUpdateException),
            Detail = exception.InnerException is not null
                ? exception.InnerException.Message
                : exception.Message,
            Status = httpContext.Response.StatusCode,
        };

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
    }
}
