// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaskContext.cs" company="Somnath Mukherjee">
//   Copyright (c) Somnath Mukherjee. All rights reserved.
// </copyright>
// <summary>
//  Task database context for Tasks
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fsd.ProjectManager.Api.Models
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The database context for Tasks
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class ProjectManagerContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectManagerContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public ProjectManagerContext(DbContextOptions<ProjectManagerContext> options) : base(options)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectManagerContext"/> class. This is for unit testing
        /// </summary>
        public ProjectManagerContext()
        {
        }

        /// <summary>
        /// Gets or sets the Users.
        /// </summary>
        public virtual DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the Projects.
        /// </summary>
        public virtual DbSet<Project> Projects { get; set; }

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
            modelBuilder.Entity<User>(builder =>
                {
                    builder.ToTable("User");
                    builder.HasAlternateKey(u => u.EmployeeId);
                    builder
                        .HasOne(u => u.Project)
                        .WithMany(p => p.Members)
                        .HasForeignKey(u => u.ProjectId)
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity<Project>(builder =>
                {
                    builder.ToTable("Project");
                    builder
                        .HasOne(p => p.Manager)
                        .WithOne(u => u.ManagedProject)
                        .HasForeignKey<Project>(p => p.ManagerId)
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity<Task>(builder =>
                {
                    builder.ToTable("Task");
                    builder
                        .HasOne(t => t.Project)
                        .WithMany(p => p.Tasks)
                        .HasForeignKey(t => t.ProjectId)
                        .OnDelete(DeleteBehavior.Cascade);
                    builder
                        .HasOne(t => t.AssignedTo)
                        .WithMany(u => u.Tasks)
                        .HasForeignKey(t => t.UserId)
                        .OnDelete(DeleteBehavior.SetNull);
                    builder
                        .HasOne(t => t.Parent)
                        .WithMany(t => t.ChildTasks)
                        .HasForeignKey(t => t.ParentId)
                        .OnDelete(DeleteBehavior.ClientSetNull);
                });
        }
    }
}
