// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Project.cs" company="Somnath Mukherjee">
//   Copyright (c) Somnath Mukherjee. All rights reserved.
// </copyright>
// <summary>
//  Project Model Class
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fsd.ProjectManager.Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// The model class for a Project
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [Key]
        [Required]
        [Display(Name = "Project Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the project summary.
        /// </summary>
        [Required]
        [StringLength(255, ErrorMessage = "Summary cannot be longer than 255 characters.")]
        [DataType(DataType.Text)]
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the project description.
        /// </summary>
        [StringLength(5000, ErrorMessage = "Description cannot be longer than 5000 characters.")]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", NullDisplayText = "-", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", NullDisplayText = "-", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets the project priotity
        /// </summary>
        [Column(TypeName = "nvarchar(6)")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Priority? Priority { get; set; }

        /// <summary>
        /// Gets or sets the project status.
        /// </summary>
        [Column(TypeName = "nvarchar(10)")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Status? Status { get; set; }

        /// <summary>
        /// Gets or sets the manager identifier.
        /// </summary>
        [Display(Name = "Manager Id")]
        [DisplayFormat(NullDisplayText = "-")]
        public int? ManagerId { get; set; }

        /// <summary>
        /// Gets or sets the manager of this project.
        /// </summary>
        [JsonIgnore]
        [ForeignKey(nameof(ManagerId))]
        public User Manager { get; set; }

        /// <summary>
        /// Gets or sets the members in the project
        /// </summary>
        [InverseProperty("Project")]
        public ICollection<User> Members { get; set; }

        /// <summary>
        /// Gets or sets the tasks in the project.
        /// </summary>
        [InverseProperty("Project")]
        public ICollection<Task> Tasks { get; set; }

        /// <summary>
        /// Gets or sets the timestamp/rowversion.
        /// </summary>
        /// <value>
        /// The timestamp.
        /// </value>
        [Timestamp]
        public byte[] Timestamp { get; set; }

        /// <summary>
        /// Updates this project with the data from another project
        /// </summary>
        /// <param name="project">The project from where to update.</param>
        public void UpdateWith(Project project)
        {
            Summary = project.Summary;
            Description = project.Description;
            StartDate = project.StartDate;
            EndDate = project.EndDate;
            Priority = project.Priority;
            Status = project.Status;
            ManagerId = project.ManagerId;
        }
    }
}
