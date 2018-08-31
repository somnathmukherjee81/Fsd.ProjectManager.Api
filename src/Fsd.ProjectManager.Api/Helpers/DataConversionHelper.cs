// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataConversionHelper.cs" company="Somnath Mukherjee">
// Copyright (c) Somnath Mukherjee. All rights reserved.
// </copyright>
// <summary>
// Data Conversion Utilities
// </summary>
// --------------------------------------------------------------------------------------------------------------------


namespace Fsd.ProjectManager.Api.Helpers
{
    /// <summary>
    /// Data Conversion Utilities
    /// </summary>
    public static class DataConversionHelper
    {
        /// <summary>
        /// Converts the string to int.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="defaultValue">The default value for output.</param>
        /// <returns>The string converted to integer</returns>
        public static int ConvertStringToInt(string text, int defaultValue = 0)
        {
            int value;

            if (int.TryParse(text, out value))
            {
                return value;
            }

            return defaultValue;
        }
    }
}
