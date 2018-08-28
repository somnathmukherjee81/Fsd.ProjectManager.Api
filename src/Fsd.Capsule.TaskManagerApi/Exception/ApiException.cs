// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApiException.cs" company="Somnath Mukherjee">
//   Copyright (c) Somnath Mukherjee. All rights reserved.
// </copyright>
// <summary>
//  Custom Exception for this Api
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fsd.Capsule.TaskManagerApi.Exception
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    /// <summary>
    /// Custom exception to store relevant exception code and related description
    /// </summary>
    public class ApiException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class.
        /// </summary>
        public ApiException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ApiException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class. 
        /// </summary>
        /// <param name="message">
        /// Message of the exception
        /// </param>
        /// <param name="statusCode">
        /// The HTTP Status Code
        /// </param>
        /// <param name="modelState">
        /// The Model State
        /// </param>
        public ApiException(string message, int statusCode = 500, ModelStateDictionary modelState = null) : base(message)
        {
            StatusCode = statusCode;
            ModelState = modelState;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public ApiException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class. 
        /// </summary>
        /// <param name="ex">
        /// The <see cref="Exception"/>
        /// </param>
        /// <param name="statusCode">
        /// The HTTP Status Code
        /// </param>
        public ApiException(Exception ex, int statusCode = 500) : base(ex.Message)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"></see> that contains contextual information about the source or destination.</param>
        protected ApiException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the state of the model.
        /// </summary>
        public ModelStateDictionary ModelState { get; set; }

        /// <summary>
        /// Gets the validation errors.
        /// </summary>
        /// <returns>
        /// The list of validation errors.
        /// </returns>
        public List<ModelError> GetValidationErrors()
        {
            return ModelState?.Values.Where(mse => mse.ValidationState == ModelValidationState.Invalid).SelectMany(mse => mse.Errors).ToList();
        }
    }
}
