using System.Text;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FlighStatusApi.Extensions;

public static class Extensions
{
    public static void AddServices(this IHostApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        var connectionStr = configuration.GetConnectionString("DefaultConnection");
        var authConnectionStr = configuration.GetConnectionString("AuthConnection");

       
        builder.Services.AddDbContext<AirContext>(options =>
        {
            options.UseSqlServer(connectionStr).LogTo(Log.Logger.Information, LogLevel.Information, null);
        });
        
        builder.Services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
        });
    }
}