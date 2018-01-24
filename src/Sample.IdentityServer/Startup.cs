using System.IO;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Sample.IdentityServer.MongoDb;
using Sample.IdentityServer.MongoDb.Models;

namespace Sample.IdentityServer
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            _environment = env;
        }

        private IHostingEnvironment _environment { get; }
        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            InjectMongoClient(services);

            var cert = new X509Certificate2(Path.Combine(_environment.ContentRootPath, "idsrv4test.pfx"), "idsrv3test");

            services.AddIdentityServer()
                .AddInMemoryClients(IdServerResources.GetClients())
                //.AddClientStore<MongoDbClientStore>()
                .AddInMemoryIdentityResources(IdServerResources.GetIdentityResources())
                .AddInMemoryApiResources(IdServerResources.GetApiResources())
                //.AddResourceStoreCache<MongoDbResourceStore>()
                .AddProfileService<MongoDbProfileService>()
                .AddResourceOwnerValidator<MongoDbResourceOwnerPasswordValidator>()
                //.AddDeveloperSigningCredential()
                .AddSigningCredential(cert)
                .AddTestUsers(IdServerResources.GetTestUsers());

            // Add service and create Policy with options 
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services.Configure<DbConnectionSettingsModel>(options =>
            {
                // Use connection string, or use settings below
                options.ConnectionString = Configuration.GetSection("MongoConnection:ConnectionString").Value;

                options.Host = Configuration.GetValue<string>("MongoConnection:Host");
                options.Port = Configuration.GetValue<int>("MongoConnection:Port");
                options.UserName = Configuration.GetValue<string>("MongoConnection:UserName");
                options.Password = Configuration.GetValue<string>("MongoConnection:Password");
                options.UseSsL = Configuration.GetValue<bool>("MongoConnection:UseSsl");
                options.Database = Configuration.GetValue<string>("MongoConnection:Database");
            });

            // Add framework services.
            services.AddMvc().AddJsonOptions(options =>
            {
                // use standard name conversion of properties
                options.SerializerSettings.ContractResolver =
                    new CamelCasePropertyNamesContractResolver();

                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");

            app.UseIdentityServer();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }

        private void InjectMongoClient(IServiceCollection services)
        {
            services.AddScoped<IRepository, MongoDbRepository>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddSingleton<IPasswordHasher<MongoDbUser>, PasswordHasher<MongoDbUser>>();
            services.AddSingleton<DbConnectionSettings>();
        }
    }
}
