using Microsoft.AspNetCore.Mvc;
using Store.Api.Factories;
using System.Text.Json.Serialization;

namespace Store.Api.Extensions
{
    public static class PresentationServicesExtension
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            services.Configure<ApiBehaviorOptions>(Options =>
            {
                Options.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidationErrorResponse;
            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;

        }
    }
}
