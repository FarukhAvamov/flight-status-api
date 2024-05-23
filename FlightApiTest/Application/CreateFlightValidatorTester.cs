using Application.Commands;
using Application.Validation;
using FlighStatusApi.Domain.AggregateModels.FlightAggregate.cs;
using FluentValidation;
using FluentValidation.TestHelper;


namespace FlightApiTest.Application
{
    public class CreateFlightValidatorTester
    {
        private  CreateFlightCommandValidator _validator;

        public CreateFlightValidatorTester()
        {
            _validator = new CreateFlightCommandValidator();
        }

        [Fact]
        public void Destination_later_than_arrival()
        {
            var command = new CreateFlightCommand(
               origin: "ALA",
               destination: "AST",
               departure: DateTime.Now.AddHours(12),
               arrival: DateTime.Now.AddHours(11),
               status: Status.InTime
            );
            var result = _validator.Validate(command);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Status_is_not_in_enum()
        {
            var command = new CreateFlightCommand(
               origin: "ALA",
               destination: "AST",
               departure: DateTime.Now.AddHours(12),
               arrival: DateTime.Now.AddHours(13),
               status: (Status)4// Status not in enum
            );
            var result = _validator.Validate(command);
            Assert.False( result.IsValid );
        }

        [Fact]
        public void Status_is_in_enum()
        {
            var command = new CreateFlightCommand(
               origin: "AST",
               destination: "ALA",
               departure: DateTime.Now.AddHours(12),
               arrival: DateTime.Now.AddHours(13),
               status: Status.InTime // Status in enum
            );
            var result = _validator.Validate(command);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Origin_equals_with_destination()
        {
            var command = new CreateFlightCommand(
               origin: "AST",
               destination: "AST",
               departure: DateTime.Now.AddHours(12),
               arrival: DateTime.Now.AddHours(13),
               status: Status.Delayed // Status in enum
            );
            var result = _validator.Validate(command);
            Assert.False(result.IsValid);
        }
    }
}
