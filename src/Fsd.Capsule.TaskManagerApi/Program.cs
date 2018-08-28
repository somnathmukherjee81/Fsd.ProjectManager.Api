// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Somnath Mukherjee">
//   Copyright (c) Somnath Mukherjee. All rights reserved.
// </copyright>
// <summary>
//   Main entry point of the service
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fsd.Capsule.TaskManagerApi
{
    using System;
    using System.IO;
    using System.Net;

    using Fsd.Capsule.TaskManagerApi.Data;
    using Fsd.Capsule.TaskManagerApi.Models;

    using Helpers;

    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Start of the application
    /// </summary>
    public class Program
    {
        /// <summary>
        /// This is the starting point of the service.
        /// </summary>
        /// <param name="args">Argument list.</param>
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<TaskContext>();
                    DbInitializer.Initialize(context);
                }
                catch (System.Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            host.Run();
        }

        /// <summary>
        /// Builds the web host of the application.
        /// </summary>
        /// <param name="args">Argument list.</param>
        /// <returns>Returns the web host object.</returns>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseApplicationInsights(Configuration.AppInsightsKey)
                .UseStartup<Startup>()
                .UseKestrel(options =>
                {
                    // If there is no certificate specified or doesn't exist we don't run https.
                    if (!string.IsNullOrEmpty(Configuration.CertPath) && File.Exists(Configuration.CertPath))
                    {
                        Console.WriteLine($"{Configuration.CertPath}: Exists.");
                        options.Listen(
                            IPAddress.Any,
                            Configuration.HttpsPort,
                            listenOptions =>
                            {
                                listenOptions.UseHttps(Configuration.CertPath);
                            });
                    }
                    else
                    {
                        Console.WriteLine($"{Configuration.CertPath}: DOES NOT Exist. NO HTTPS");
                        options.Listen(IPAddress.Any, Configuration.HttpPort);
                    }
                })
                .PreferHostingUrls(false)
                .Build();
    }
}
