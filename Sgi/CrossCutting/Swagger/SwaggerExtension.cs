using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Sgi.CrossCutting.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Sgi.CrossCutting.Swagger
{    
    public class ConfigureSwaggerOption : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;
        private readonly SwaggerConfigurationOptions _swaggerConfiguration;

        public ConfigureSwaggerOption(IApiVersionDescriptionProvider provider,
                                      IOptionsMonitor<SwaggerConfigurationOptions> swaggerConfigurationOptions)
        {
            _provider = provider;
            _swaggerConfiguration = swaggerConfigurationOptions?.CurrentValue;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo
            {
                Title = _swaggerConfiguration.Title,
                Version = description.ApiVersion.ToString(),
                Description = _swaggerConfiguration.Description
            };

            if (description.IsDeprecated)
            {
                info.Description += " Esta versão está obsoleta.";
            }

            return info;
        }
    }
    
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SwaggerConfigurationOptions>(configuration?.GetSection("SwaggerConfiguration"));
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOption>();

            services.AddSwaggerGen(s =>
            {
                s.OperationFilter<SwaggerDefaultValues>();
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Jwt Authorization"
                });
                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app, IApiVersionDescriptionProvider provider, string iisApplication)
        {
            var inicioEndpoint = string.IsNullOrWhiteSpace(iisApplication) ? string.Empty : $"/{iisApplication}";

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"{inicioEndpoint}/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });

            return app;
        }
    }
    
    public class SwaggerDefaultValues : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var apiDescription = context?.ApiDescription;

            if (operation != null && apiDescription != null)
            {
                operation.Deprecated = apiDescription.IsDeprecated();

                if (operation.Parameters == null)
                {
                    return;
                }

                foreach (var parameter in operation.Parameters)
                {
                    var description = apiDescription.ParameterDescriptions.FirstOrDefault(p => p.Name == parameter.Name);
                    parameter.Description ??= description.ModelMetadata?.Description;
                    parameter.Required |= description.IsRequired;
                }
            }
        }
    }
}
