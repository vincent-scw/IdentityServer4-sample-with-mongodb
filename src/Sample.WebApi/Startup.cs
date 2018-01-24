using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sample.Common;
using Swashbuckle.AspNetCore.Swagger;

namespace Sample.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureSwagger(services);
            services.AddCors(options =>
            {
                options.AddPolicy("AllCors",
                    builder => builder.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());
            });

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    var config = Configuration.GetSection("IdServerConfig").Get<IdentityServerConfig>();
                    options.Authority = config.Authority;
                    options.RequireHttpsMetadata = config.RequireHttpsMetadata;
                    options.ApiName = config.ApiName;
                });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllCors");
            app.UseSwagger();
            app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", string.Empty); });
            app.UseAuthentication();
            app.UseMvc();
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "IdentityServer4 sample",
                    TermsOfService = "None"
                });
                options.DescribeAllEnumsAsStrings();

                var config = Configuration.GetSection("IdServerConfig").Get<IdentityServerConfig>();
                options.AddSecurityDefinition("oauth2", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "application",
                    TokenUrl = "http://localhost:44329/connect/token",
                    Scopes = new Dictionary<string, string>
                    {
                        { config.ApiName, "Select this scope" }
                    }
                });
                options.OperationFilter<AuthResponseOperationFilter>();
            });
        }
    }
}
