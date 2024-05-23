using Application.Commands;
using Domain.Domain.AggregateModels.FlightAggregate.cs;
using FlighStatusApi.Domain.AggregateModels.FlightAggregate.cs;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;

namespace FlightApiTest.Application
{
    public class CreateFlightCommandHandlerTest
    {
        private readonly IFlightRepository _flightRepositoryMock;

        public CreateFlightCommandHandlerTest()
        {
            _flightRepositoryMock = Substitute.For<IFlightRepository>();
        }


        [Fact]
        public async Task Handle_create_flight_success()
        {
            // Arrange
            _flightRepositoryMock.UnitOfWork.SaveEntitiesAsync(Arg.Any<CancellationToken>()).Returns(Task.FromResult(true));

            var validatorMock = Substitute.For<IValidator<CreateFlightCommand>>();
            validatorMock.ValidateAsync(Arg.Any<CreateFlightCommand>(), Arg.Any<CancellationToken>())
                         .Returns(Task.FromResult(new ValidationResult())); // No errors

            var loggerMock = Substitute.For<ILogger<CreateFlightCommandHandler>>();
            var accessorMock = Substitute.For<IHttpContextAccessor>();
            accessorMock.HttpContext?.User?.Identity?.Name.Returns("TestUser");

            var handler = new CreateFlightCommandHandler(_flightRepositoryMock, validatorMock, loggerMock, accessorMock);
            var command = new CreateFlightCommand(
                origin: "ALA",
                destination: "AST",
                departure: DateTime.Now.AddHours(12),
                arrival: DateTime.Now.AddHours(14),
                status: Status.InTime
            );
            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            _flightRepositoryMock.Received().Add(Arg.Any<Flight>());
            await _flightRepositoryMock.UnitOfWork.Received().SaveEntitiesAsync(CancellationToken.None);
            Assert.True(result);


        }

        private Flight FakeFlight()
        {
            return new Flight("ALA", "AST", DateTimeOffset.Now.AddHours(12), DateTime.Now.AddHours(14), Status.InTime);
        }

    }
}
