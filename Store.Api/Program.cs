
using Domian.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Services;
using Services.Abstractions;
using Shared.ErroresModels;
using Store.Api.Extentions;
using Store.Api.Middlewares;


namespace Store.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.RegistarAllServices(builder.Configuration);

            var app = builder.Build();

            await app.ConfigureMiddlewares();

            app.Run();
        }
    }
}
