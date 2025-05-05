using Microsoft.AspNetCore.Mvc;
using Shared.ErroresModels;
using Services;
using Persistence;
using Microsoft.AspNetCore.Builder;
using Domian.Contracts;
using Store.Api.Middlewares;
using Microsoft.AspNetCore.Identity;
using Persistence.Identity;
using Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using Domian.Models.Identity;

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
            services.AddIdentityServices();
            services.AddApplicationServices(configuration);
            services.ConfigureJwtService(configuration);


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
            await dbInitializer.InitializeIdentityAsync();
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

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            return app;
        }


        private static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<StoreIdentityDbContext>();
            return services;
        }

        private static IServiceCollection ConfigureJwtService(this IServiceCollection services , IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,

                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecrietKey))
                };
            });

            return services;
        }
    }

   

   
}
