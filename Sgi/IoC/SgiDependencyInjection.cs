using Microsoft.EntityFrameworkCore;
using Sgi.Application.Interfaces;
using Sgi.Application.Services;
using Sgi.CrossCutting.Options;
using Sgi.DistributedServices;
using Sgi.Repository;
using Sgi.Repository.Contexts;
using Sgi.Security;

namespace Sgi.IoC
{
    public static class SgiDependencyInjection
    {
        public static void AddSgiDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSgiApiConfigDependencyInjection();

            services.AddDbContext<SgiContext>(optionsAction =>
                         optionsAction.UseSqlServer(configuration["ConnectionStrings:Context"]));

            services.Configure<JwtOptions>(configuration?.GetSection("Jwt"));
            services.Configure<SecurityOptions>(configuration?.GetSection("Security"));
            services.Configure<SwaggerConfigurationOptions>(configuration?.GetSection("SwaggerConfiguration"));
            services.Configure<SmtpOptions>(configuration?.GetSection("Smtp"));

            services.AddScoped<IGerarTokenService, GerarTokenService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IEventoService, EventoService>();
            services.AddScoped<ICompraService, CompraService>();
            services.AddScoped<ICriptografiaService, CriptografiaService>();

            services.AddScoped<ISgiRepository, SgiRepository>();

            services.AddScoped<IEnvioEmailService, EnvioEmailService>();
        }
    }
}
