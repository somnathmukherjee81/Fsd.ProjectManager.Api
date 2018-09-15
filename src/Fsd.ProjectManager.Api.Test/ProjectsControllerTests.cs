// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectsControllerTests.cs" company="Somnath Mukherjee">
//    Copyright (c) Somnath Mukherjee. All rights reserved.
// </copyright>
// <summary>
//   Unit tests for the Projects Controller
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fsd.ProjectManager.Api.Test
{
    using System;
    using System.Collections.Generic;

    using Fsd.ProjectManager.Api.Controllers;
    using Fsd.ProjectManager.Api.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using Moq;
    using Xunit;

    /// <summary>
    /// The validator helper tests.
    /// </summary>
    public class ProjectsControllerTests
    {
        /// <summary>
        /// The projects controller
        /// </summary>
        private ProjectsController _projectsController;

        /// <summary>
        /// The projects
        /// </summary>
        private List<Project> _projects;

        /// <summary>
        /// The mock database set
        /// </summary>
        private Mock<DbSet<Project>> _mockDbSet;

        /// <summary>
        /// The mock context
        /// </summary>
        private Mock<ProjectManagerContext> _mockContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectsControllerTests"/> class.
        /// </summary>
        public ProjectsControllerTests()
        {
            _projects = ProjectsControllerTestHelper.CreateProjectList();

            _mockDbSet = ProjectsControllerTestHelper.CreateMockProjectDbSet(_projects);
            _mockContext = ProjectsControllerTestHelper.CreateMockProjectContext(_mockDbSet.Object);

            _projectsController = new ProjectsController(_mockContext.Object);
            _projectsController.ControllerContext = new ControllerContext();
            _projectsController.ControllerContext.HttpContext = new DefaultHttpContext();
            _projectsController.ControllerContext.HttpContext.Request.Headers["test-header"] = "test-header-value";
        }

        /// <summary>
        /// Test method used to validate that GetAllProjects returns all the projects
        /// </summary>
        [Fact]
        public void When_GetAllProjectsIsCalled_AllProjectsAreReturned()
        {
            // Act
            var results = _projectsController.Get();

            // Assert
            Assert.Equal(_projects.Count, results.Value.Count);
        }

        ///// <summary>
        ///// Test method used to validate that GetProjectById retirns the correct project
        ///// </summary>
        //[Fact]
        //public void When_GetProjectByIdIsCalled_TheMatchingProjectIsReturned()
        //{
        //    // Act
        //    var result = _projectsController.Get(2);

        //    // Assert
        //    Assert.NotNull(result.Value);
        //    Assert.Equal(2, result.Value.ProjectId);
        //    Assert.Equal("The Ultimate Machine", result.Value.Summary);
        //}

        /// <summary>
        /// Test method used to validate that CreateProject adds and saves a project
        /// </summary>
        [Fact]
        public void When_CreateProjectIsCalled_ProjectIsAddedAndSaved()
        {
            // Arrange 
            Project newProject = new Project
                {
                    ProjectId = 3,
                    Summary = "The Super Ultimate Machine",
                    Description = "To implement a Turning Machine that can pass the Turing Test",
                    StartDate = DateTime.Parse("2020-01-01"),
                    EndDate = DateTime.Parse("2020-12-31"),
                    Priority = Priority.Low,
                    Status = Status.NotStarted,
                    ManagerId = 15
                };

            // Act
            var result = _projectsController.Post(newProject);

            // Assert
            _mockDbSet.Verify(m => m.Add(It.IsAny<Project>()), Times.Once());
            _mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Test method used to validate that UpdateProject updates and saves a project
        /// </summary>
        [Fact]
        public void When_UpdateProjectIsCalled_ProjectIsUpdatedAndSaved()
        {
            // Arrange 
            Project updatedProject = new Project
                {
                    ProjectId = 2,
                    Summary = "The Ultimate Machine",
                    Description = "To implement a Turning Machine that can pass the Turing Test",
                    StartDate = DateTime.Parse("2019-01-01"),
                    EndDate = DateTime.Parse("2019-12-31"),
                    Priority = Priority.Low,
                    Status = Status.NotStarted,
                    ManagerId = 14
                };

            // Act
            var result = _projectsController.Put(2, updatedProject);

            // Assert
            _mockDbSet.Verify(m => m.Find(It.IsAny<int>()), Times.Once());
            _mockDbSet.Verify(m => m.Update(It.IsAny<Project>()), Times.Once());
            _mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Test method used to validate that DeleteProject deletes and saves a project
        /// </summary>
        [Fact]
        public void When_DeleteProjectIsCalled_ProjectIsDeletedAndSaved()
        {
            // Act
            var result = _projectsController.Delete(2);

            // Assert
            _mockDbSet.Verify(m => m.Find(It.IsAny<int>()), Times.Once());
            _mockDbSet.Verify(m => m.Remove(It.IsAny<Project>()), Times.Once());
            _mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }        
    }
}
