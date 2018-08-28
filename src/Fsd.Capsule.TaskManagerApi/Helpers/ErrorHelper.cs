// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ErrorHelper.cs" company="Somnath Mukherjee">
//   Copyright (c) Somnath Mukherjee. All rights reserved.
// </copyright>
// <summary>
// Class to provide utility for exception
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fsd.Capsule.TaskManagerApi.Helpers
{
    using System;
    using System.Net;
    using System.Net.Http;
    using Exception;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Models;
    using Newtonsoft.Json;

    /// <summary>
    /// Class to provide utility for exception
    /// </summary>
    public static class ErrorHelper
    {
        /// <summary>
        /// JSON content type
        /// </summary>
        private const string ContentTypeJson = "application/json";

        /// <summary>
        /// Handles the exception.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <returns>The serialized JSON response for the exception</returns>
        public static string ProcessException(HttpContext context, Exception exception, ILoggerFactory loggerFactory)
        {
            HttpStatusCode code;

            if (exception is ApiException)
            {
                code = HttpStatusCode.InternalServerError;
            }
            else if (exception is HttpRequestException)
            {
                code = HttpStatusCode.BadRequest;
            }
            else
            {
                code = HttpStatusCode.InternalServerError;
            }

            var logger = loggerFactory.CreateLogger("Global exception logger");
            logger.LogError(
                (int)code,
                exception,
                exception.Message);

            var result = JsonConvert.SerializeObject(new Error { Code = (int)code, Message = exception.Message, StackTrace = exception.StackTrace });

            context.Response.ContentType = ContentTypeJson;
            context.Response.StatusCode = (int)code;

            return result;
        }
    }
}
