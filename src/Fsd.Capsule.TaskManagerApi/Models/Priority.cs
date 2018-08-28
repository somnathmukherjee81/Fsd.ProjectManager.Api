// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Priority.cs" company="Somnath Mukherjee">
//   Copyright (c) Somnath Mukherjee. All rights reserved.
// </copyright>
// <summary>
//  Priority Model Enumeration
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fsd.Capsule.TaskManagerApi.Models
{
    /// <summary>
    /// Enumeration for Task Priority
    /// </summary>
    public enum Priority
    {
        /// <summary>
        /// Low priority
        /// </summary>
        Low = 0,

        /// <summary>
        /// Medium priority
        /// </summary>
        Medium = 2,

        /// <summary>
        /// High priority
        /// </summary>
        High = 3,

        /// <summary>
        /// Urgent priority
        /// </summary>
        Urgent = 4
    }
}
