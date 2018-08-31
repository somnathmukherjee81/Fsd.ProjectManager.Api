// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Task.cs" company="Somnath Mukherjee">
//   Copyright (c) Somnath Mukherjee. All rights reserved.
// </copyright>
// <summary>
//  Task Model Class
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
    /// The model class for a Task
    /// </summary>
    public class Task
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [Key]
        [Required]
        [Display(Name = "Task Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskId { get; set; }

        /// <summary>
        /// Gets or sets the task summary.
        /// </summary>
        [Required]
        [StringLength(255, ErrorMessage = "Summary cannot be longer than 255 characters.")]
        [DataType(DataType.Text)]
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the task description.
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
        /// Gets or sets the task priotity
        /// </summary>
        [Column(TypeName = "nvarchar(6)")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Priority? Priority { get; set; }

        /// <summary>
        /// Gets or sets the task status.
        /// </summary>
        [Column(TypeName = "nvarchar(10)")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Status? Status { get; set; }

        /// <summary>
        /// Gets or sets the project id the task belongs to.
        /// </summary>
        [Required]
        [Display(Name = "Project Id")]
        [DisplayFormat(NullDisplayText = "-")]
        public int ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the project the task belongs to.
        /// </summary>
        [JsonIgnore]
        [ForeignKey(nameof(ProjectId))]
        public Project Project { get; set; }

        /// <summary>
        /// Gets or sets the parent identifier.
        /// </summary>        
        [Display(Name = "Parent Id")]
        [DisplayFormat(NullDisplayText = "-")]
        public int? ParentId { get; set; }

        /// <summary>
        /// Gets or sets the parent task.
        /// </summary>
        [JsonIgnore]
        [ForeignKey(nameof(ParentId))]
        public Task Parent { get; set; }

        /// <summary>
        /// Gets or sets the child tasks.
        /// </summary>
        [JsonIgnore]
        [InverseProperty("Parent")]
        public ICollection<Task> ChildTasks { get; set; }

        /// <summary>
        /// Gets or sets the user id the task is assigned to.
        /// </summary>        
        [Display(Name = "User Id")]
        [DisplayFormat(NullDisplayText = "-")]
        public int? UserId { get; set; }

        /// <summary>
        /// Gets or sets the user the task is assigned to.
        /// </summary>
        [JsonIgnore]
        [ForeignKey(nameof(UserId))]
        public User AssignedTo { get; set; }

        /// <summary>
        /// Gets or sets the timestamp/rowversion.
        /// </summary>
        /// <value>
        /// The timestamp.
        /// </value>
        [Timestamp]
        public byte[] Timestamp { get; set; }

        /// <summary>
        /// Updates this task with the data from another task
        /// </summary>
        /// <param name="task">The task from where to update.</param>
        public void UpdateWith(Task task)
        {            
            Summary = task.Summary;
            Description = task.Description;
            StartDate = task.StartDate;
            EndDate = task.EndDate;
            Priority = task.Priority;
            Status = task.Status;
            ProjectId = task.ProjectId;
            ParentId = task.ParentId;
            UserId = task.UserId;
        }
    }
}
