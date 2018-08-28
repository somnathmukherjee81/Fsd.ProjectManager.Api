//-----------------------------------------------------------------------
// <copyright file="TelemetryInitializer.cs" company="Somnath Mukherjee">
//     Copyright (c) Somnath Mukherjee. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Fsd.Capsule.TaskManagerApi.Initializers
{
    using Microsoft.ApplicationInsights.Channel;
    using Microsoft.ApplicationInsights.DataContracts;
    using Microsoft.ApplicationInsights.Extensibility;

    /// <summary>
    /// telemetry initializer
    /// </summary>
    public class TelemetryInitializer : ITelemetryInitializer
    {
        /// <summary>
        /// Initializes the telemetry object with the assembly name.
        /// </summary>
        /// <param name="telemetry">The telemetry object.</param>
        public void Initialize(ITelemetry telemetry)
        {
            var requestTelemetry = telemetry as RequestTelemetry;
            if (requestTelemetry?.Context?.Cloud != null)
            {
                requestTelemetry.Context.Cloud.RoleName = "task-manager-api";
            }
        }
    }
}