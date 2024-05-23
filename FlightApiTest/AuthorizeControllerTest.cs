using System.Security.Claims;
using System.Threading.Tasks;
using Application.Commands;
using Application.Queries;
using Application.Services;
using FlighStatusApi.Controllers.v1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

public class FlightControllerTests
{
    private FlightController CreateControllerWithUser(string role, FlightService flightService, CommandDispatcher commandDispatcher)
    {
        var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "testuser"),
            new Claim(ClaimTypes.Role, role)
        }, "TestAuthentication"));

        var controller = new FlightController(flightService, commandDispatcher)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            }
        };

        return controller;
    }

    [Fact]
    public async Task Create_flight_user_role_returns200()
    {
        // Arrange
        var flightService = Substitute.For<FlightService>(Substitute.For<IFlightQueries>());
        var commandDispatcher = Substitute.For<CommandDispatcher>();
        var controller = CreateControllerWithUser("user", flightService, commandDispatcher);

        var command = new CreateFlightCommand();

        // Act
        var result = await controller.CreateFligth(command);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task Create_flight_moderator_returns200()
    {
        // Arrange
        var flightService = Substitute.For<FlightService>(Substitute.For<IFlightQueries>());
        var commandDispatcher = Substitute.For<CommandDispatcher>();
        var controller = CreateControllerWithUser("moderator", flightService, commandDispatcher);

        var command = new CreateFlightCommand();

        // Act
        var result = await controller.CreateFligth(command);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task Create_flight_no_role_returns403()
    {
        // Arrange
        var flightService = Substitute.For<FlightService>(Substitute.For<IFlightQueries>());
        var commandDispatcher = Substitute.For<CommandDispatcher>();
        var controller = CreateControllerWithUser("guest", flightService, commandDispatcher);

        var command = new CreateFlightCommand();

        // Act
        var result = await controller.CreateFligth(command);

        // Assert
        Assert.IsType<ForbidResult>(result);
    }
}
