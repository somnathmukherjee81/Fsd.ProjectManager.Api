// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaskContext.cs" company="Somnath Mukherjee">
//   Copyright (c) Somnath Mukherjee. All rights reserved.
// </copyright>
// <summary>
//  Task database context for Tasks
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fsd.Capsule.TaskManagerApi.Models
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The database context for Tasks
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class TaskContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaskContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public TaskContext(DbContextOptions<TaskContext> options) : base(options)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskContext"/> class. This is for unit testing
        /// </summary>
        public TaskContext()
        {
        }

        /// <summary>
        /// Gets or sets the tasks.
        /// </summary>
        public virtual DbSet<Task> Tasks { get; set; }

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <remarks>
        /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run.
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>().ToTable("Task");
        }
    }
}
