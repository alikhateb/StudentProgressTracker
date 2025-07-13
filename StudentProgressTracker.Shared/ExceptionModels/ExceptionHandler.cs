using System.Data.Common;
using System.Net;
using System.Net.Mime;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace StudentProgressTracker.Shared.ExceptionModels;

public class ExceptionHandler
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
        var exceptionResult = new ExceptionResult
        {
            Message = exception.Message,
            StatusCode = httpContext.Response.StatusCode,
            ExceptionType = nameof(Exception),
        };

        await httpContext.Response.WriteAsJsonAsync(exceptionResult, cancellationToken);
    }

    private static async Task HandleValidationException(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        var validationException = (ValidationException)exception;

        var exceptionResult = new ExceptionResult
        {
            Message = nameof(ValidationException),
            ExceptionType = nameof(ValidationException),
        };

        var validationErrors = validationException.Errors.ToLookup(validationFailure =>
            validationFailure.PropertyName
        );

        foreach (var validationError in validationErrors)
        {
            var validationErrorResult = new ValidationErrorResult
            {
                PropertyName = validationError.Key,
            };

            foreach (var validationFailure in validationError)
            {
                validationErrorResult.PropertyErrorResults.Add(
                    new ValidationPropertyErrorResult
                    {
                        Message = validationFailure.ErrorMessage,
                        ErrorCode = validationFailure.ErrorCode,
                        Value = validationFailure.AttemptedValue,
                        Severity = validationFailure.Severity.ToString(),
                    }
                );
            }

            exceptionResult.ValidationErrorResults.Add(validationErrorResult);
        }

        exceptionResult.StatusCode = httpContext.Response.StatusCode;

        await httpContext.Response.WriteAsJsonAsync(exceptionResult, cancellationToken);
    }

    private static async Task HandleDatabaseException(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        var exceptionResult = new ExceptionResult
        {
            Message = exception.InnerException is not null
                ? exception.InnerException.Message
                : exception.Message,
            ExceptionType = nameof(DbException),
            StatusCode = httpContext.Response.StatusCode,
        };

        await httpContext.Response.WriteAsJsonAsync(exceptionResult, cancellationToken);
    }
}
