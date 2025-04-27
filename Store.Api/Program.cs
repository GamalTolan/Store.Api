
using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
<<<<<<< Updated upstream
=======
using Persistence.Repositories;
using Services;
using Services.Abstractions;
using Services.MappingProfiles;
using StackExchange.Redis;
using Store.Api.Factories;
using Store.Api.Middelwares;
using System.Reflection.Metadata;
>>>>>>> Stashed changes

namespace Store.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
            } );
            builder.Services.AddSingleton<IConnectionMultiplexer>(
                _ => ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis"))
                );

            builder.Services.AddScoped<IDbInitializer, DbInitializer>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
<<<<<<< Updated upstream
=======
            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.AddAutoMapper(typeof(AssemblyReference).Assembly);
            builder.Services.AddAutoMapper(x => x.AddProfile(new ProductProfile()));
>>>>>>> Stashed changes

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidationErrorResponse;
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            seedDb(app);

            //using var scope = app.Services.CreateScope();
            //var DbInitializer = scope.ServiceProvider.GetService<IDbInitializer>();
            //DbInitializer?.Initialize();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
<<<<<<< Updated upstream

=======
            app.UseStaticFiles();
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();
>>>>>>> Stashed changes
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            

            app.Run();
        }
        static void seedDb(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var DbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            DbInitializer?.Initialize();
        }
    }
}
