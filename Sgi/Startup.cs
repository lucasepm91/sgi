using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.IdentityModel.Tokens;
using Sgi.CrossCutting.ApiConcerns;
using Sgi.CrossCutting.Options;
using Sgi.CrossCutting.Swagger;
using Sgi.IoC;
using System.Text;

namespace Sgi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }        
        private readonly SwaggerConfigurationOptions _swaggerConfiguration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;            
            _swaggerConfiguration = Configuration.GetSection("SwaggerConfiguration").Get<SwaggerConfigurationOptions>();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSgiDependencyInjection(Configuration);
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ClockSkew = TimeSpan.FromHours(4),
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["Jwt:Key"])),
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        RequireExpirationTime = false,
                        ValidIssuer = Configuration["Jwt:Issuer"]
                    };
                });
            
            services.AddSwaggerConfig(Configuration);            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            if (_swaggerConfiguration.Enable)
                app.UseSwaggerConfig(provider, Configuration["AppConfig:IisApplication"]);
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseApiError();
            app.UseResponseCompression();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
