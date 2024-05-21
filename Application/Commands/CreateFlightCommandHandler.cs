using Application.Validation;
using Domain.Domain.AggregateModels.FlightAggregate.cs;
using FlighStatusApi.Domain.AggregateModels.FlightAggregate.cs;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Commands;

public class CreateFlightCommandHandler : ICommandHandler<CreateFlightCommand>
{
    private readonly IFlightRepository _flightRepository;
    private readonly CreateFlightCommandValidator _validator;
    private readonly ILogger<CreateFlightCommandHandler> _logger;
    private readonly IHttpContextAccessor _accessor;

    public CreateFlightCommandHandler(IFlightRepository flightRepository, CreateFlightCommandValidator validator,
        ILogger<CreateFlightCommandHandler> logger,
        IHttpContextAccessor accessor)
    {
        _flightRepository = flightRepository ?? throw new ArgumentNullException(nameof(flightRepository));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _logger = logger;
        _accessor = accessor;
    }
    
    public async Task<bool> Handle(CreateFlightCommand command,CancellationToken cancellationToken)
    {
        var validate = await _validator.ValidateAsync(command,cancellationToken);

        if (!validate.IsValid)
        {
            throw new ArgumentException(validate.ToString());
        }
        var userName = _accessor.HttpContext?.User.Identity?.Name;
        var flight = new Flight(command.Origin, command.Destination, command.Arrival, command.Departure, command.Status);
        _logger.LogInformation($"{DateTime.Now}; {flight.ToString()} was added by {userName}");
        _flightRepository.Add(flight);
    
        return await _flightRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
    
