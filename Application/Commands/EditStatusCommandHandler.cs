using Application.Validation;
using Domain.Domain.AggregateModels.FlightAggregate.cs;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Commands;

public class EditStatusCommandHandler : ICommandHandler<SetFlightStatusCommand>
{
    private readonly IFlightRepository _flightRepository;
    private readonly ILogger<CreateFlightCommandHandler> _logger;
    private readonly IHttpContextAccessor _accessor;
    private readonly IValidator<SetFlightStatusCommand> _validator;


    public EditStatusCommandHandler(IFlightRepository flightRepository, ILogger<CreateFlightCommandHandler> logger,
        IHttpContextAccessor accessor,
        IValidator<SetFlightStatusCommand> validator)
    {
        _flightRepository = flightRepository;
        _logger = logger;
        _accessor = accessor;
        _validator = validator;
    }
    
    public async Task<bool> Handle(SetFlightStatusCommand command,CancellationToken cancellationToken)
    {
        var validate = await _validator.ValidateAsync(command, cancellationToken);
        if (!validate.IsValid)
        {
            throw new ArgumentException(validate.ToString());
        }
        var flightToUpdate = await _flightRepository.FindByIdAsync(command.Id);
        if (flightToUpdate == null)
        {
            return false;
        }

        flightToUpdate.SetArrivalStatus(command.Status);
        var userName = _accessor.HttpContext?.User.Identity?.Name;
        _logger.LogInformation($"{DateTime.Now}; FlightId: {flightToUpdate.ID} status was changed by {userName}");
        return await _flightRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}