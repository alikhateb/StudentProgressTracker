using FluentValidation;

namespace StudentProgressTracker.Core.Application.Courses.Add;

public sealed class AddStudentCommandValidator : AbstractValidator<AddStudentCommand>
{
    public AddStudentCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("name is required.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("email is required.")
            .WithErrorCode("2")
            .EmailAddress()
            .WithMessage("invalid email address.")
            .WithErrorCode("3");
    }
}
