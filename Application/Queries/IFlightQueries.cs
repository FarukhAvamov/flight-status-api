using Domain.Domain.AggregateModels.FlightAggregate.cs;
using FlighStatusApi.Domain.AggregateModels.FlightAggregate.cs;

namespace Application.Queries;

public interface IFlightQueries
{
    Task<IList<FlightViewModel>> GetFlightsAsync(string origin, string destination);
}