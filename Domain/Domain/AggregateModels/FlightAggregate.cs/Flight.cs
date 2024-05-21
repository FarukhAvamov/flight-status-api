using Domain.Domain.AggregateModels.FlightAggregate.cs;
using Domain.Domain.SeedWork;
using FlighStatusApi.Domain.SeedWork;

namespace FlighStatusApi.Domain.AggregateModels.FlightAggregate.cs
{
    public class Flight : Entity, IAggregateRoot
    {
        public string Origin { get; init; }
        public string Destination { get; init; }
        public DateTimeOffset Arrival { get; private set; }
        public DateTimeOffset Departure { get; private set; }
        public Status Status { get; set; }

        protected Flight() {}
        public Flight(string origin, string destination, DateTimeOffset arrival, DateTimeOffset departure, Status status) 
        {
            Origin = origin;
            Destination = destination;
            Arrival = arrival;
            Departure = departure;
            Status = status;
        }
        
        public void SetArrivalStatus(Status status)
        {
            Status = status;
        }

        public override string ToString()
        {
            return $"Flight from {Origin} to {Destination} - Departure: {Departure}, Arrival: {Arrival}, Status: {Status}";
        }
    }
}
