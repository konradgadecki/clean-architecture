using FluentValidation;

namespace GymManagement.Application.Gyms.Commands.CreateGym;

public class CreateGymCommandValidator : AbstractValidator<CreateGymCommand>
{
    public CreateGymCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(3).WithMessage("Name must exceed 3 characters.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
    }
}