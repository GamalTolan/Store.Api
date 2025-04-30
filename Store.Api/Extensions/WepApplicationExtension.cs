using Domain.Contracts;
using Microsoft.AspNetCore.Builder;

namespace Store.Api.Extensions
{
    public static class WepApplicationExtension
    {
        public static async Task<WebApplication> seedDbAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbInitializer.Initialize();
            await dbInitializer.InitializeIdentityAsync();
            return app;
        }
    }
}
