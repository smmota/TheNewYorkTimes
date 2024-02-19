using FluentValidation;
using FluentValidation.Results;
using System;
using TheNewYorkTimes.Data.Repositories;
using TheNewYorkTimes.Interfaces.Repositories;
using TheNewYorkTimes.Interfaces.Services;
using TheNewYorkTimes.Models;
using TheNewYorkTimes.Services;
using TheNewYorkTimes.Validations;

namespace TheNewYorkTimes.Infrastructure.Configurations
{
    public static class Extension
    {
        public static IServiceCollection DependencyMap(this IServiceCollection services)
        {
            services.AddTransient<INoticiaRepository, NoticiaRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IHashService, HashService>();
            services.AddTransient<ITokenService, TokenService>(); 

            return services;
        }

        public static IServiceCollection ValidationMap(this IServiceCollection services)
        {
            services.AddScoped<IValidator<Noticia>, NoticiaValidator>();
            services.AddScoped<IValidator<Usuario>, UsuarioValidator>();
            services.AddScoped<IValidator<Login>, LoginValidator>();

            return services;
        }

        public static IDictionary<string, string[]> FluentValidationExtensions(this ValidationResult validationResult)
        {
            return validationResult.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.ErrorMessage).ToArray()
                );
        }
    }
}
