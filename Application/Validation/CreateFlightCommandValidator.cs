using Application.Commands;
using FluentValidation;

namespace Application.Validation;

public class CreateFlightCommandValidator : AbstractValidator<CreateFlightCommand>
{
    public CreateFlightCommandValidator()
    {
        RuleFor(command => command.Origin).NotEmpty()
            .NotEqual(c => c.Destination)
            .WithMessage("Origin cannot be equal to destination");
        RuleFor(command => command.Destination).NotEmpty()
            .NotEqual(c => c.Origin)
            .WithMessage("Destination cannot be equal to origin");
        RuleFor(command => command.Arrival).NotEmpty()
            .GreaterThan(c => c.Departure)
            .WithMessage("Arrival must be greater than Departure.");;
        RuleFor(command => command.Departure).NotEmpty();
        RuleFor(command => command.Status).NotEmpty()
            .IsInEnum().WithMessage("Invalid status value.");
        ;
            
        
    }
}