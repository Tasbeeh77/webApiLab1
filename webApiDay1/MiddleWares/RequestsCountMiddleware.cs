namespace webApiDay1.MiddleWares
{
    public class RequestsCountMiddleware
    {
        private static int requestCount = 0;
        private readonly RequestDelegate _next;

        public RequestsCountMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            requestCount++;
            context.Response.OnStarting(() =>
            {
                context.Response.Headers.Add("Request-Count", requestCount.ToString());
                return Task.CompletedTask;
            });
            await _next(context);
        }
    }
}
