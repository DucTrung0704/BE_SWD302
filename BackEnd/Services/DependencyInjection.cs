using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Repositories.Interfaces;
using Repositories.Mapper;
using Repositories.Repositories;
using Services.Interfaces;
using Services.Services;
using Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddService(this IServiceCollection services, ConfigurationManager configuration)
        {
            // repository
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBackgroundDoctorRepository, BackgroundDoctorRepository>();

            // service
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBackgroundDoctorService, BackgroundDoctorService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(MapperConfigurationsProfile).Assembly);
            services.AddScoped<TokenProvider>();

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme =
            //    options.DefaultChallengeScheme =
            //    options.DefaultForbidScheme =
            //    options.DefaultScheme =
            //    options.DefaultSignInScheme =
            //    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidIssuer = configuration["Jwt:Issuer"],
            //        ValidateAudience = true,
            //        ValidAudience = configuration["Jwt:Audience"],
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(
            //            System.Text.Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!)
            //        ),
            //        ValidateLifetime = true,
            //        LifetimeValidator = CustomLifetimeValidator
            //    };
            //    //options.Authority = "Authority URL"; //TODO: UDPATE URL

            //    options.Events = new JwtBearerEvents()
            //    {
            //        OnMessageReceived = context =>
            //        {
            //            var accessToken = context.Request.Query["access_token"];

            //            // If the request is for our hub...
            //            var path = context.HttpContext.Request.Path;
            //            if (!string.IsNullOrEmpty(accessToken) &&
            //                (path.StartsWithSegments("/hub")))
            //            {
            //                // Read the token out of the query string
            //                context.Token = accessToken;
            //            }

            //            return Task.CompletedTask;
            //        }
            //    };
            //});

            return services;
        }

        private static bool CustomLifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken token, TokenValidationParameters @params)
        {
            if (expires != null)
            {
                return expires > DateTime.UtcNow;
            }
            return false;
        }
    }
}
