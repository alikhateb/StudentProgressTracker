namespace StudentProgressTracker.Shared.ExceptionModels;

public sealed class ExceptionResult
{
    public string Message { get; set; }
    public int StatusCode { get; set; }
    public string ExceptionType { get; set; }
    public List<ValidationErrorResult> ValidationErrorResults { get; set; } = [];
}
