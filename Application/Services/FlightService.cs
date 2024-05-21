using Application.Queries;
using Domain.Domain.AggregateModels.FlightAggregate.cs;

namespace Application.Services;

public class FlightService(
    IFlightQueries queries )
{
    public IFlightQueries Queries { get; } = queries;
}
