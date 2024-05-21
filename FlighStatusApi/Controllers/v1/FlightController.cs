using System.Diagnostics.Eventing.Reader;
using Application.Commands;
using Application.Queries;
using Application.Services;
using FlighStatusApi.Domain.AggregateModels.FlightAggregate.cs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.IdentityModel.Tokens;

namespace FlighStatusApi.Controllers.v1;

public class FlightController : BaseApiController<FlightController>
{
    private readonly FlightService _flightService;
    private readonly CommandDispatcher _commandDispatcher;
    public FlightController(FlightService flightService, CommandDispatcher commandDispatcher)
    {
        _flightService = flightService;
        _commandDispatcher = commandDispatcher;
    }
    
    [HttpGet]
    [ActionName("getFlights")]
    [Authorize(Roles = "user, moderator")]
    public async Task<Results<Ok<IList<FlightViewModel>>, NoContent>> GetFlightsAsync(string origin = "", string destination = "")
    {
        var flights = await _flightService.Queries.GetFlightsAsync(origin, destination);
        if (flights.IsNullOrEmpty())
        {
            return TypedResults.NoContent();
        }

        return TypedResults.Ok(flights);
    }    
    
    [HttpPatch]
    [ActionName("updateFlightStatus")]
    [Authorize(Roles = "moderator")]
    public async Task<Results<Ok, UnprocessableEntity>> UpdateFlightStatus(SetFlightStatusCommand command)
    {
        await _commandDispatcher.Dispatch(command);
        return TypedResults.Ok();
    }

    [HttpPost]
    [ActionName("CreateFlight")]
    [Authorize(Roles = "moderator")]
    public async Task<Results<Ok, UnprocessableEntity>> CreateFligth(CreateFlightCommand command)
    {
        await _commandDispatcher.Dispatch(command);
        return TypedResults.Ok();
    }
    
    
}