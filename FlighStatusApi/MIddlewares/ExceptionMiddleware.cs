namespace FlighStatusApi.MIddlewares;

public class ExceptionMiddleware
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger _logger;
        public GlobalExceptionHandlingMiddleware(ILogger logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, "TestParam");
            }
        }
    }
}