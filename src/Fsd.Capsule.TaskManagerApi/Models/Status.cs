// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Status.cs" company="Somnath Mukherjee">
//   Copyright (c) Somnath Mukherjee. All rights reserved.
// </copyright>
// <summary>
//  Status Model Enumeration
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fsd.Capsule.TaskManagerApi.Models
{
    /// <summary>
    /// Enumeration for Task Status
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// The task is not started
        /// </summary>
        NotStarted = 0,

        /// <summary>
        /// The task is in progress
        /// </summary>
        InProgress = 1,

        /// <summary>
        /// The task is deferred
        /// </summary>
        Deferred = 2,

        /// <summary>
        /// The task is completed
        /// </summary>
        Completed = 3
    }
}
