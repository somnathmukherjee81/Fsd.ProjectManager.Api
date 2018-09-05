// --------------------------------------------------------------------------------------------------------------------
// <copyright file="User.cs" company="Somnath Mukherjee">
//   Copyright (c) Somnath Mukherjee. All rights reserved.
// </copyright>
// <summary>
//  User Model Class
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fsd.ProjectManager.Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Newtonsoft.Json;

    /// <summary>
    /// The model class for a User
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [Key]
        [Required]
        [Display(Name = "User Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the user's first name.
        /// </summary>
        [Required]
        [StringLength(500, ErrorMessage = "First Name cannot be longer than 500 characters.")]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the user's last name.
        /// </summary>
        [Required]
        [StringLength(500, ErrorMessage = "Last Name cannot be longer than 500 characters.")]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        [NotMapped]
        public string FullName => $"{LastName}, {FirstName}";

        /// <summary>
        /// Gets or sets the user's employee id.
        /// </summary>        
        [Required]
        [StringLength(6, ErrorMessage = "Employee Id cannot be longer than 6 characters.")]
        [DataType(DataType.Text)]
        [Display(Name = "Employee Id")]
        public string EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the project id of the user.
        /// </summary>        
        [Display(Name = "Project Id")]
        [DisplayFormat(NullDisplayText = "-")]
        public int? ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the project of teh user.
        /// </summary>
        [JsonIgnore]
        [ForeignKey(nameof(ProjectId))]
        public Project Project { get; set; }

        /// <summary>
        /// Gets or sets the tasks assigned to the user
        /// </summary>
        [InverseProperty("AssignedTo")]
        [JsonIgnore]
        public ICollection<Task> Tasks { get; set; }

        /// <summary>
        /// Gets or sets the project id the user manages.
        /// </summary>        
        [Display(Name = "Managed Project Id")]
        [DisplayFormat(NullDisplayText = "-")]
        public int? ManagedProjectId { get; set; }

        /// <summary>
        /// Gets or sets the project the user manages
        /// </summary>
        [JsonIgnore]
        [InverseProperty("Manager")]
        public Project ManagedProject { get; set; }

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
        /// <param name="user">The user from where to update.</param>
        public void UpdateWith(User user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            EmployeeId = user.EmployeeId;
            ProjectId = user.ProjectId;
        }
    }
}
