using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Commands;

public class CommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;


    public CommandDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task Dispatch<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand
    {
        var handler = _serviceProvider.GetService(typeof(ICommandHandler<TCommand>)) as ICommandHandler<TCommand>;
        if (handler != null)
        {
            await handler.Handle(command, cancellationToken);
        }
        else
        {
            throw new InvalidOperationException($"Handler for command {typeof(TCommand).Name} not found");
        }
    }
}