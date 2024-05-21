using Domain.Domain.SeedWork;
using FlighStatusApi.Domain.SeedWork;

namespace Domain.Domain.AggregateModels.FlightAggregate.cs;

public class City(string name) : Entity, IAggregateRoot
{
    public string Name { get; set; } = name;
    public int TimeZone { get; set; }
    
    public override string ToString()
    {
        return Name;
    }
}