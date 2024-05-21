using Domain.Domain.AggregateModels.FlightAggregate.cs;
using FlighStatusApi.Domain.AggregateModels.FlightAggregate.cs;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries;

public class FlightQueries(AirContext context) : IFlightQueries
{
    public async Task<IList<FlightViewModel>> GetFlightsAsync(string origin, string destination)
    {
        return await context.Flights.AsNoTracking()
            .Where(c => c.Origin.Contains(origin) && 
                        c.Destination.Contains(destination))
            .Select(f => new FlightViewModel()
        {
            Id = f.ID,
            Origin = f.Origin.ToString(),
            Destination = f.Destination.ToString(),
            Departure = f.Departure,
            Arrival = f.Arrival,
            Status = f.Status.ToString()

        }).OrderBy(c => c.Arrival).ToListAsync();
    }
}