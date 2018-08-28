// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ErrorHandlingMiddleware.cs" company="Somnath Mukherjee">
//   Copyright (c) Somnath Mukherjee. All rights reserved.
// </copyright>
// <summary>
//  The Middleware for Error Handling
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fsd.Capsule.TaskManagerApi.Middlewares
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Helpers;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Exception = System.Exception;

    /// <summary>
    /// Middleware for Error Handling
    /// </summary>
    public sealed class ErrorHandlingMiddleware
    {
        /// <summary>
        /// The delegate to the next processor
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// The logger factory
        /// </summary>
        private readonly ILoggerFactory _loggerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorHandlingMiddleware"/> class. 
        /// </summary>
        /// <param name="next">
        /// The delegate to the next processor
        /// </param>
        /// <param name="loggerFactory">The logger factory.</param>
        public ErrorHandlingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _loggerFactory = loggerFactory;
        }

        /// <summary>
        /// Invokes the ErrorHandlingMiddleware
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/></param>
        /// <returns>Returns the <see cref="Task"/> of teh next processor</returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Asynchronously handle the Exception
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/></param>
        /// <param name="exception">The <see cref="Exception"/></param>
        /// <returns>Returns the <see cref="Task"/> of teh next processor</returns>
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            try
            {
                var result = ErrorHelper.ProcessException(context, exception, _loggerFactory);

                return context.Response.WriteAsync(result);
            }
            catch (Exception ex)
            {
                Trace.TraceError($"An exception was thrown attempting  to execute the error handler:{ex}");

                throw;
            }
        }
    }
}
