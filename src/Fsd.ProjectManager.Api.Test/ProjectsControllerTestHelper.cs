// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectControllerTestHelper.cs" company="Somnath Mukherjee">
//    Copyright (c) Somnath Mukherjee. All rights reserved.
// </copyright>
// <summary>
//   Helper for Projects Controller Tests
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fsd.ProjectManager.Api.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Fsd.ProjectManager.Api.Controllers;
    using Fsd.ProjectManager.Api.Models;
    using Microsoft.EntityFrameworkCore;
    using Moq;

    /// <summary>
    /// Helper class for <see cref="ProjectsController"/> tests
    /// </summary>
    public static class ProjectsControllerTestHelper
    {
        /// <summary>
        /// Creates the mock project DbSet.
        /// </summary>
        /// <param name="projects">The projects.</param>
        /// <returns>The Mocked DbSet</returns>
        public static Mock<DbSet<Project>> CreateMockProjectDbSet(List<Project> projects)
        {
            var data = projects.AsQueryable();
            var mockDbSet = new Mock<DbSet<Project>>();
            mockDbSet.As<IQueryable<Project>>().Setup(m => m.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Project>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Project>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Project>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockDbSet.Setup(ds => ds.Find(It.IsAny<object[]>())).Returns((object[] key) => projects.FirstOrDefault(project => project.ProjectId == (int)key.First()));
            mockDbSet.Setup(x => x.Add(It.IsAny<Project>())).Callback<Project>(project => projects.Add(project));
            mockDbSet.Setup(x => x.Update(It.IsAny<Project>())).Callback<Project>(
                project =>
                    {
                        int index = projects.FindIndex(t => t.ProjectId == project.ProjectId);
                        projects[index] = project;
                    });
            mockDbSet.Setup(x => x.Remove(It.IsAny<Project>())).Callback<Project>(project => projects.Remove(project));

            return mockDbSet;
        }

        /// <summary>
        /// Creates the mock project context.
        /// </summary>
        /// <param name="databaseSet">The DbSet</param>
        /// <returns>The Mocked Project Context</returns>
        public static Mock<ProjectManagerContext> CreateMockProjectContext(DbSet<Project> databaseSet)
        {
            var mockContext = new Mock<ProjectManagerContext>();

            mockContext.Setup(c => c.Projects).Returns(databaseSet);

            return mockContext;
        }

        /// <summary>
        /// Creates a sample project list.
        /// </summary>
        /// <returns>The List of projects</returns>
        public static List<Project> CreateProjectList()
        {
            return new List<Project>
                {
                    new Project
                        {
                            ProjectId = 1,
                            Summary = "The Machine",
                            Description = "To implement a Turning Machine that can pass the Turing Test",
                            StartDate = DateTime.Parse("2018-01-01"),
                            EndDate = DateTime.Parse("2018-12-31"),
                            Priority = Priority.High,
                            Status = Status.InProgress,
                            ManagerId = 14
                        },
                    new Project
                        {
                            ProjectId = 2,
                            Summary = "The Ultimate Machine",
                            Description = "To implement a Turning Machine that can pass the Turing Test",
                            StartDate = DateTime.Parse("2019-01-01"),
                            EndDate = DateTime.Parse("2019-12-31"),
                            Priority = Priority.Low,
                            Status = Status.NotStarted,
                            ManagerId = 15
                        }
                };
        }
    }
}
