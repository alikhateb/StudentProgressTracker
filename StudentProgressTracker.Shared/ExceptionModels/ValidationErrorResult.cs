namespace StudentProgressTracker.Shared.ExceptionModels;

public sealed class ValidationErrorResult
{
    public string PropertyName { get; set; }
    public List<ValidationPropertyErrorResult> PropertyErrorResults { get; set; } = [];
}
