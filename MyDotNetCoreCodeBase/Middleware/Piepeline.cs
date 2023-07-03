using MyDotNetCoreCodeBase.Middleware;

namespace MyDotNetCoreCodeBase.Middleware
{
    public class Piepeline
    {
        public class CustomerExceptionPipeline
        {
            public void Configure(IApplicationBuilder app)
            {
                app.UseMiddleware<CustomerExceptionMiddleware>();
            }
        }
    }
}
