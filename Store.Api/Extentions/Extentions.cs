using Microsoft.AspNetCore.Mvc;
using Shared.ErroresModels;
using Services;
using Persistence;
using Microsoft.AspNetCore.Builder;
using Domian.Contracts;
using Store.Api.Middlewares;

namespace Store.Api.Extentions
{
    public static class Extentions
    {

        //before builde
        public static IServiceCollection RegistarAllServices(this IServiceCollection services , IConfiguration configuration)
        {

            services.AddControllers();
           services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            //DbContext
            services.AddInfrastructureServices(configuration);
            services.AddApplicationServices();


            #region validtions Error
            services.Configure<ApiBehaviorOptions>(config =>
            {
                config.InvalidModelStateResponseFactory = (ActionContext) =>
                {
                    var errors = ActionContext.ModelState.Where(m => m.Value.Errors.Any())
                                               .Select(m => new ValidationError()
                                               {
                                                   Field = m.Key,
                                                   Errors = m.Value.Errors.Select(errors => errors.ErrorMessage)
                                               });
                    var response = new ValidtionErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(response);
                };
            });
            #endregion

            return services;
        }


        //after builde
        public async static Task<WebApplication> ConfigureMiddlewares(this WebApplication app)
        {

            #region Seeding
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbInitializer.InitializeAsync();
            #endregion

            app.UseMiddleware<GlobelErrorHandlingMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            return app;
        }
    }


   
}
