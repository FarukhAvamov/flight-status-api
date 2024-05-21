using Application.Commands;
using FluentValidation;

namespace Application.Validation;

public class EditStatusCommandValidator : AbstractValidator<SetFlightStatusCommand>
{
    public EditStatusCommandValidator()
    {
        RuleFor(command => command.Status).NotEmpty()
            .IsInEnum().WithMessage("Invalid status value.");
    }
}