using Application.Commands;
using Application.Queries;
using Application.Services;
using Application.Validation;
using Domain.Domain.AggregateModels.FlightAggregate.cs;
using Infrastructure.Identity;
using Infrastructure.Repository;

namespace FlighStatusApi.Extensions;

public static class DiExtension
{
    public static void RegisterDependencies(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IFlightQueries, FlightQueries>();
        builder.Services.AddScoped<IFlightRepository, FlightRepository>();

        builder.Services.AddScoped<IAuthService, AuthService>();

        builder.Services.AddTransient<ICommandHandler<CreateFlightCommand>, CreateFlightCommandHandler>();
        builder.Services.AddTransient<ICommandHandler<SetFlightStatusCommand>, EditStatusCommandHandler>();
        builder.Services.AddTransient<CommandDispatcher>();
        builder.Services.AddTransient<FlightService>();

        
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddTransient<CreateFlightCommandValidator>();
        builder.Services.AddTransient<EditStatusCommandValidator>();
    }
}