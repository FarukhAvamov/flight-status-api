using System;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands;
using Application.Queries;
using Application.Services;
using FlighStatusApi.Controllers.v1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
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
    public async Task CreateFlight_UserRole_Returns200()
    {
        // Arrange
        var flightQueries = Substitute.For<IFlightQueries>();
        var flightService = new FlightService(flightQueries);
        var serviceProvider = Substitute.For<IServiceProvider>();
        var commandHandler = Substitute.For<ICommandHandler<CreateFlightCommand>>();
        serviceProvider.GetService(typeof(ICommandHandler<CreateFlightCommand>)).Returns(commandHandler);

        var commandDispatcher = new CommandDispatcher(serviceProvider);
        var controller = CreateControllerWithUser("user", flightService, commandDispatcher);

        var command = new CreateFlightCommand();

        // Act
        var result = await controller.CreateFligth(command);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task CreateFlight_ModeratorRole_Returns200()
    {
        // Arrange
        var flightQueries = Substitute.For<IFlightQueries>();
        var flightService = new FlightService(flightQueries);
        var serviceProvider = Substitute.For<IServiceProvider>();
        var commandHandler = Substitute.For<ICommandHandler<CreateFlightCommand>>();
        serviceProvider.GetService(typeof(ICommandHandler<CreateFlightCommand>)).Returns(commandHandler);

        var commandDispatcher = new CommandDispatcher(serviceProvider);
        var controller = CreateControllerWithUser("moderator", flightService, commandDispatcher);

        var command = new CreateFlightCommand();

        // Act
        var result = await controller.CreateFligth(command);

        // Assert
        Assert.IsType<Ok>(result);
    }

    [Fact]
    public async Task CreateFlight_NoRole_Returns401()
    {
        // Arrange
        var flightQueries = Substitute.For<IFlightQueries>();
        var flightService = new FlightService(flightQueries);
        var serviceProvider = Substitute.For<IServiceProvider>();
        var commandHandler = Substitute.For<ICommandHandler<CreateFlightCommand>>();
        serviceProvider.GetService(typeof(ICommandHandler<CreateFlightCommand>)).Returns(commandHandler);

        var commandDispatcher = new CommandDispatcher(serviceProvider);
        var controller = CreateControllerWithUser("guest", flightService, commandDispatcher);

        var command = new CreateFlightCommand();

        // Act
        var result = await controller.CreateFligth(command);
        // Assert
        var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
        Assert.Equal(StatusCodes.Status200OK, result.Result);
    }
}
