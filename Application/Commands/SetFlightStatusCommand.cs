using System.Runtime.Serialization;
using FlighStatusApi.Domain.AggregateModels.FlightAggregate.cs;

namespace Application.Commands;

public class SetFlightStatusCommand : ICommand
{
    [DataMember] 
    public int Id { get; set; }
    [DataMember] 
    public Status Status { get; set; }

    public SetFlightStatusCommand()
    {
    }

    public SetFlightStatusCommand(int id, Status status)
    {
        Id = id;
        Status = status;
    }
}