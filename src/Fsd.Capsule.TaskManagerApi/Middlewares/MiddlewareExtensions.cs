// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MiddlewareExtensions.cs" company="Somnath Mukherjee">
//   Copyright (c) Somnath Mukherjee. All rights reserved.
// </copyright>
// <summary>
//  Extension methods for Middlewares
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fsd.Capsule.TaskManagerApi.Middlewares
{
    using System.Net;
    using Helpers;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    using Exception = System.Exception;

    /// <summary>
    /// Extension methods for Middlewares
    /// </summary>
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// The default error message
        /// </summary>
        private const string DefaultErrorMessage = "An unhandled exception has occurred";

        /// <summary>
        /// Uses the <see cref="ErrorHandlingMiddleware"/>
        /// </summary>
        /// <param name="builder">The <see cref="IApplicationBuilder"/></param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <param name="useDefaultDiagnosticMiddleware">Flag indicating whether to use default ASP.NET Core diagnostic middleware</param>
        /// <returns>Returns the <see cref="IApplicationBuilder"/></returns>
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder, ILoggerFactory loggerFactory, bool useDefaultDiagnosticMiddleware = false)
        {
            if (!useDefaultDiagnosticMiddleware)
            {
                return builder.UseMiddleware<ErrorHandlingMiddleware>(loggerFactory);
            }

            return builder.UseExceptionHandler(
                options =>
                {
                    options.Run(
                        async context =>
                        {
                            string result = DefaultErrorMessage;
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                            var error = context.Features.Get<IExceptionHandlerFeature>();

                            if (error != null)
                            {
                                Exception exception = error.Error;

                                result = ErrorHelper.ProcessException(context, exception, loggerFactory);
                            }

                            await context.Response.WriteAsync(result).ConfigureAwait(false);
                        });
                });
        }
    }
}
