using Microsoft.AspNetCore.Builder;
using Nancy.Owin;

namespace CustomerInviter.Api.Service
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Use(async (httpContext, next) =>
            {
                httpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                httpContext.Response.Headers.Add("Access-Control-Allow-Methods", "GET,POST,PUT,DELETE,OPTIONS");
                httpContext.Response.Headers.Add("Access-Control-Allow-Headers", "Origin,X-Requested-With,Content-Type,Accept,Authorization,Accept-Encoding");
                httpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Type,Content-Encoding,Content-Length,ETag,Location");
                
                await next();
            });
            app.UseOwin(x => x.UseNancy(new NancyOptions
            {
                Bootstrapper = new Bootstrapper()
            }));
        }
    }
}