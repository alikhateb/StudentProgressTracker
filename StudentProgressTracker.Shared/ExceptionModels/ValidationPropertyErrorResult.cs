namespace StudentProgressTracker.Shared.ExceptionModels;

public class ValidationPropertyErrorResult
{
    public object Value { get; set; }

    public string? ErrorCode { get; set; }

    public string Message { get; set; }

    public string Severity { get; set; }
}
