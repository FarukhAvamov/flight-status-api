namespace Application.Queries;

public class FlightViewModel
{
    public int Id { get; init; }
    public string Origin { get; init; }
    public string Destination { get; init; }
    public DateTimeOffset Arrival { get; init; }
    public DateTimeOffset Departure { get; init; }
    public string Status { get; init; }
}