using Domain.Domain.SeedWork;
using FlighStatusApi.Domain.AggregateModels.FlightAggregate.cs;

namespace Domain.Domain.AggregateModels.FlightAggregate.cs
{
    public interface IFlightRepository : IRepository<Flight>
    {
        Flight Add(Flight buyer);
        Flight Update(Flight buyer);
        Task<IReadOnlyCollection<Flight>> GetAllAsync();
        Task<Flight> FindByIdAsync(int id);
    }
}
