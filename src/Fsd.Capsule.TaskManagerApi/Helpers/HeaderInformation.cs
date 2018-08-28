// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HeaderInformation.cs" company="Somnath Mukherjee">
//   Copyright (c) Somnath Mukherjee. All rights reserved.
// </copyright>
// <summary>
//  Class used to get the header Information
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fsd.Capsule.TaskManagerApi.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Class used to get the GTID and Host Information
    /// </summary>
    public class HeaderInformation
    {
        /// <summary>
        /// Header name Debug
        /// </summary>
        private const string HeaderDebug = "DEBUG";

        /// <summary>
        /// Http headers property.
        /// </summary>
        private readonly IHeaderDictionary _headers;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderInformation"/> class.
        /// </summary>
        /// <param name="headers">Http headers of the request.</param>
        public HeaderInformation(IHeaderDictionary headers)
        {
            _headers = headers;
            Debug = RetrieveHeaderValue(HeaderDebug);
        }

        /// <summary>
        /// Gets the InteractionId
        /// ets the interaction id information.
        /// </summary>
        public string Debug { get; }

        /// <summary>
        /// Gets the specified key from the headers.
        /// </summary>
        /// <param name="key">Key value.</param>
        /// <returns>Header value for the key specified.</returns>
        private string RetrieveHeaderValue(string key)
        {
            string value = null;
            var invariantKey = RetrieveKey(key);

            if (!string.IsNullOrEmpty(invariantKey))
            {
                value = _headers[invariantKey].ToString();
            }

            return value;
        }

        /// <summary>
        /// Gets the key within the format specified in the request.
        /// </summary>
        /// <param name="key">Key name expected.</param>
        /// <returns>Key name specified.</returns>
        private string RetrieveKey(string key)
        {
            var invariantKey =
                _headers.Keys.FirstOrDefault(x => x.Equals(key, StringComparison.InvariantCultureIgnoreCase));

            return invariantKey;
        }        
    }
}
