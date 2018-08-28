//-----------------------------------------------------------------------
// <copyright file="Configuration.cs" company="Somnath Mukherjee">
//     Copyright (c) Somnath Mukherjee. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Fsd.Capsule.TaskManagerApi.Helpers
{
    using System;

    /// <summary>
    /// Defines the <see cref="Configuration" />
    /// </summary>
    internal static class Configuration
    {
        /// <summary>
        /// Gets the HttpPort
        /// The http port.
        /// </summary>
        public static int HttpPort => int.Parse(GetValue("HTTP_PORT", "9090"));

        /// <summary>
        /// Gets the HttpsPort
        /// The https port.
        /// </summary>
        public static int HttpsPort => int.Parse(GetValue("HTTPS_PORT", "9091"));

        /// <summary>
        /// Gets the CertPath
        /// The certificate Path.
        /// </summary>
        public static string CertPath => GetValue("CERT_PATH", "__fsd_capsule_taskmanager.pfx");

        /// <summary>
        /// Gets the DbType
        /// Database Type.
        /// </summary>
        public static string DbType => GetValue("DB_TYPE", "LOCALDB");

        /// <summary>
        /// Gets the DbConnectionString
        /// Database Connection String.
        /// </summary>
        public static string DbConnectionString => GetValue(
            "DB_CONNECTION_STRING", 
            "Server=(localdb)\\mssqllocaldb;Database=TasksDb;Trusted_Connection=True;MultipleActiveResultSets=true");

        /// <summary>
        /// Gets the DatabaseName
        /// Database Name.
        /// </summary>
        public static string DatabaseName => GetValue("DB_NAME", "TasksDb");

        /// <summary>
        /// Gets the AppInsightsKey
        /// App Insights Key.
        /// </summary>
        public static string AppInsightsKey => GetValue("APP_INSIGHTS_KEY", null);

        /// <summary>
        /// Gets the configuration value.
        /// </summary>
        /// <param name="key">Environment key.</param>
        /// <param name="defaultValue">Default value.</param>
        /// <returns>The environment value or the default value if not found.</returns>
        private static string GetValue(string key, string defaultValue)
        {
            string result =
            #if DEBUG
                defaultValue;
            #else
                null;
            #endif

            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable(key)))
            {
                result = Environment.GetEnvironmentVariable(key);
                Console.WriteLine($"{key}: {result}");
            }
            else
            {
                Console.WriteLine($"{key} ENVIRONMENT VARIABLE NOT FOUND OR EMPTY. RETURNING DEFAULT VALUE {result}");
            }

            return result;
        }
    }
}
