// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="Somnath Mukherjee">
//   Copyright (c) Somnath Mukherjee. All rights reserved.
// </copyright>
// <summary>
//   Startup of the service
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fsd.Capsule.TaskManagerApi
{
    using System;
    using System.IO;

    using Fsd.Capsule.TaskManagerApi.Models;    
    using Initializers;
    using Microsoft.ApplicationInsights.Extensibility;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.PlatformAbstractions;

    using Middlewares;

    using Newtonsoft.Json;

    using Swashbuckle.AspNetCore.Swagger;

    using ConfigurationHelper = Helpers.Configuration;

    /// <summary>
    /// The Startup class of the service
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Title of the service
        /// </summary>
        private const string ServiceTitle = "task-manager-api";

        /// <summary>
        /// Name of the service
        /// </summary>
        private const string ServiceNameVersion = "v1";

        /// <summary>
        /// Version of the service
        /// </summary>
        private const string Version = "v1";

        /// <summary>
        /// Logging config section
        /// </summary>
        private const string ConfigSectionLogging = "Logging";

        /// <summary>
        /// Swagger file path
        /// </summary>
        private const string SwaggerPath = "/swagger/v1/swagger.json";

        /// <summary>
        /// Description of the service
        /// </summary>
        private const string ServiceDescription = "task-manager-api v1";

        /// <summary>
        /// The swagger document description
        /// </summary>
        private const string SwaggerDocumentDescription = "Task Manager Api";

        /// <summary>
        /// The team chandra name
        /// </summary>
        private const string AuthorName = "Somnath Mukherjee";

        /// <summary>
        /// The team chandra email
        /// </summary>
        private const string AuthorEmail = "somnath.mukherjee@cognizant.com";

        /// <summary>
        /// The team chandra URL
        /// </summary>
        private const string DocumentationUrl = "";

        /// <summary>
        /// The XML comments file
        /// </summary>
        private const string XmlCommentsFile = "Fsd.Capsule.TaskManagerApi.xml";

        /// <summary>
        /// The database type in memory
        /// </summary>
        private const string DbTypeInMemory = "INMEMORY";

        /// <summary>
        /// The database type Local Db
        /// </summary>
        private const string DbTypeLocalDb = "LOCALDB";

        /// <summary>
        /// The database type SQL server
        /// </summary>
        private const string DbTypeSqlServer = "SQLSERVER";

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="configuration">Configuration values.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration for the service.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add Cors management.
            services.AddCors();

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(service =>
            {
                service.SwaggerDoc(
                    ServiceNameVersion,
                    new Info
                    {
                        Version = Version,
                        Title = ServiceTitle,
                        Description = SwaggerDocumentDescription,
                        Contact = new Contact
                        {
                            Name = AuthorName,
                            Email = AuthorEmail,
                            Url = DocumentationUrl
                        }
                    });

                // Set the comments path for the Swagger JSON and UI.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, XmlCommentsFile);

                if (File.Exists(xmlPath))
                {
                    service.IncludeXmlComments(xmlPath);
                }
            });

            // Register the database context
            RegisterDatabaseContext(services);

            // Add framework services.
            services.AddMvc(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;
            })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });

            // Enable ApplicationInsights
            services.AddApplicationInsightsTelemetry();

            // Enable ApplicationInsights for Kubernetes
            services.EnableKubernetes();

            // Add default (gzip) response compression 
            services.AddResponseCompression();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/>.</param>
        /// <param name="env">The <see cref="IHostingEnvironment"/>.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Adding new TelemetryInitializer.
            var configuration = app.ApplicationServices.GetService<TelemetryConfiguration>();
            configuration.TelemetryInitializers.Add(new TelemetryInitializer());

            // Shows UseCors with CorsPolicyBuilder.
            app.UseCors(builder =>
                builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseErrorHandlingMiddleware(loggerFactory);
            }

            loggerFactory.AddConsole(Configuration.GetSection(ConfigSectionLogging));
            loggerFactory.AddDebug();

            // Enable response compression
            app.UseResponseCompression();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(SwaggerPath, ServiceDescription);
            });

            app.UseMvc();
        }

        /// <summary>
        /// Registers the database context.
        /// </summary>
        /// <param name="services">The services.</param>
        private static void RegisterDatabaseContext(IServiceCollection services)
        {
            string databaseType = ConfigurationHelper.DbType;
            switch (databaseType?.ToUpper())
            {
                case DbTypeInMemory:
                    services.AddDbContext<TaskContext>(options => options.UseInMemoryDatabase(ConfigurationHelper.DatabaseName));
                    break;
                case DbTypeLocalDb:
                case DbTypeSqlServer:
                    services.AddDbContext<TaskContext>(options => options.UseSqlServer(ConfigurationHelper.DbConnectionString));
                    break;
            }
        }
    }
}
