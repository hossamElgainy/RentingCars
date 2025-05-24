

using Microsoft.AspNetCore.Builder;
using RentingCars.APIS.Middlewares;

namespace RentingCars.APIS.Extensions
{
    public static class MiddlewareExtensions
    {
        public static WebApplication UseCustomMiddleware(this WebApplication app)
        {
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors("CorsPolicy");
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            return app;
        }
    }
}
