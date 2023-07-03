using Serilog;
using MyDotNetCoreCodeBase.Middleware;

namespace MyDotNetCoreCodeBase.Middleware
{
    public class CustomerExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public CustomerExceptionMiddleware(RequestDelegate next, IServiceProvider serviceProvider) 
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex) 
            {
                Log.Fatal(ex, "錯誤訊息 => " + ex.Message);
                if (ex.InnerException != null)
                {
                    Log.Fatal(ex.InnerException, ex.InnerException.Message);
                }

            }
        }
    }
}
