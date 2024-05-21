using System.Runtime.Serialization;
using FlighStatusApi.Domain.AggregateModels.FlightAggregate.cs;

namespace Application.Commands;

public class CreateFlightCommand : ICommand
{
    [DataMember]
    public string Origin { get; set; }
    [DataMember]
    public string Destination { get; set; }
    [DataMember]
    public DateTimeOffset Arrival { get; set; }
    [DataMember]
    public DateTimeOffset Departure { get; set; }
    [DataMember]
    public Status Status { get; set; }

    public CreateFlightCommand()
    {
        var qwe = new DateTimeOffset().LocalDateTime;
    }

    public CreateFlightCommand(string origin, string destination, 
        DateTime arrival, 
        DateTime departure, 
        Status status)
    {
        Origin = origin;
        Destination = destination;
        Arrival = new DateTimeOffset(arrival);
        Departure = new DateTimeOffset(departure);
        Status = status;
    }
}