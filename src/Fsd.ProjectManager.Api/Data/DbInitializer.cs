//-----------------------------------------------------------------------
// <copyright file="DbInitializer.cs" company="Somnath Mukherjee">
//     Copyright (c) Somnath Mukherjee. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Fsd.ProjectManager.Api.Data
{
    using System;
    using System.Linq;
    using Fsd.ProjectManager.Api.Models;


    /// <summary>
    /// Database Initializer
    /// </summary>
    public static class DbInitializer
    {
        /// <summary>
        /// Initializes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public static void Initialize(ProjectManagerContext context)
        {
            context.Database.EnsureCreated();

            // Look for any task, user and project.
            if (context.Tasks.Any() || context.Users.Any() || context.Projects.Any())
            {
                // DB has been seeded
                return;
            }

            // Add Users
            var users = new[]
                {
                    new User
                        {
                            FirstName = "Alan",
                            LastName = "Turing",
                            EmployeeId = "100000"
                        },
                    new User
                        {
                            FirstName = "Donald",
                            LastName = "Knuth",
                            EmployeeId = "100001"
                        },
                    new User
                        {
                            FirstName = "Ken",
                            LastName = "Thompson",
                            EmployeeId = "100002"
                        },
                    new User
                        {
                            FirstName = "Larry",
                            LastName = "Page",
                            EmployeeId = "100003"
                        },
                    new User
                        {
                            FirstName = "Niklaus",
                            LastName = "Wirth",
                            EmployeeId = "100004"
                        },
                    new User
                        {
                            FirstName = "Dennis",
                            LastName = "Ritchie",
                            EmployeeId = "100005"
                        },
                    new User
                        {
                            FirstName = "Edsger",
                            LastName = "Dijkstra",
                            EmployeeId = "100006"
                        },
                    new User
                        {
                            FirstName = "Claude",
                            LastName = "Shannon",
                            EmployeeId = "100007"
                        },
                    new User
                        {
                            FirstName = "John von",
                            LastName = "Newmann",
                            EmployeeId = "100008"
                        },
                    new User
                        {
                            FirstName = "Brian",
                            LastName = "Kernighan",
                            EmployeeId = "100009"
                        },
                    new User
                        {
                            FirstName = "Bjarne",
                            LastName = "Stroustrup",
                            EmployeeId = "100010"
                        },
                    new User
                        {
                            FirstName = "Larry",
                            LastName = "Wall",
                            EmployeeId = "100011"
                        },
                    new User
                        {
                            FirstName = "Mark",
                            LastName = "Zuckerberg",
                            EmployeeId = "100012"
                        },
                    new User
                        {
                            FirstName = "Jeff",
                            LastName = "Sutherland",
                            EmployeeId = "100013"
                        }
                };

            foreach (User u in users)
            {
                context.Users.Add(u);
            }

            context.SaveChanges();

            // Add Project
            var project = new Project
                {
                    Summary = "The Machine",
                    Description = "To implement a Turning Machine that can pass the Turing Test",
                    StartDate = DateTime.Parse("2018-01-01"),
                    EndDate = DateTime.Parse("2018-12-31"),
                    Priority = Priority.High,
                    Status = Status.InProgress,
                    ManagerId = context.Users.First(u => u.EmployeeId == "100013").UserId
                };

            context.Projects.Add(project);
            context.SaveChanges();

            int projectId = context.Projects.First().ProjectId;

            // Add users to project
            var allUsers = context.Users.ToList();
            foreach (User user in allUsers)
            {
                user.ProjectId = projectId;
                context.Users.Update(user);
            }

            context.SaveChanges();

            // Add Parent Tasks
            var parentTasks = new[]
                {
                    new Task
                        {
                            Summary = "Implement Channel Service",
                            Description =
                                "Implement Channel Service for the feature which will invoke the factories",
                            StartDate = DateTime.Parse("2018-07-02"),
                            EndDate = DateTime.Parse("2018-07-06"),
                            Priority = Priority.High,
                            Status = Status.NotStarted,
                            ProjectId = projectId,
                            UserId = context.Users.First(u => u.EmployeeId == "100009").UserId
                        },
                    new Task
                        {
                            Summary = "Implement Front End Service",
                            Description =
                                "Implement Front End Service for the feature which will invoke the Channel Service",
                            StartDate = DateTime.Parse("2018-07-09"),
                            EndDate = DateTime.Parse("2018-07-13"),
                            Priority = Priority.High,
                            Status = Status.NotStarted,
                            ProjectId = projectId,
                            UserId = context.Users.First(u => u.EmployeeId == "100010").UserId
                        },
                    new Task
                        {
                            Summary = "Implement Front End",
                            Description =
                                "Implement Front End for the feature which will invoke the Front End Service",
                            StartDate = DateTime.Parse("2018-07-16"),
                            EndDate = DateTime.Parse("2018-07-20"),
                            Priority = Priority.High,
                            Status = Status.NotStarted,
                            ProjectId = projectId,
                            UserId = context.Users.First(u => u.EmployeeId == "100011").UserId
                        }
                };

            foreach (Task t in parentTasks)
            {
                context.Tasks.Add(t);
            }

            context.SaveChanges();

            // Add Child Tasks
            var childTasks = new[]
                {
                    new Task
                        {
                            ParentId = context.Tasks.First(t => t.Summary == "Implement Channel Service").TaskId,
                            Summary = "Create Channel Service Repository",
                            ProjectId = projectId,
                            UserId = context.Users.First(u => u.EmployeeId == "100000").UserId
                        },
                    new Task
                        {
                            ParentId = context.Tasks.First(t => t.Summary == "Implement Channel Service").TaskId,
                            Summary = "Create ASP.Net Core WebApi for Channel Service",
                            ProjectId = projectId,
                            UserId = context.Users.First(u => u.EmployeeId == "100001").UserId
                        },
                    new Task
                        {
                            ParentId = context.Tasks.First(t => t.Summary == "Implement Channel Service").TaskId,
                            Summary = "Create CI/CD pipeline for Channel Service",
                            ProjectId = projectId,
                            UserId = context.Users.First(u => u.EmployeeId == "100002").UserId
                        },
                    new Task
                        {
                            ParentId = context.Tasks.First(t => t.Summary == "Implement Front End Service").TaskId,
                            Summary = "Create Front End Service Repository",
                            ProjectId = projectId,
                            UserId = context.Users.First(u => u.EmployeeId == "100003").UserId
                        },
                    new Task
                        {
                            ParentId = context.Tasks.First(t => t.Summary == "Implement Front End Service").TaskId,
                            Summary = "Create Node.js service for Front End Service",
                            ProjectId = projectId,
                            UserId = context.Users.First(u => u.EmployeeId == "100004").UserId
                        },
                    new Task
                        {
                            ParentId = context.Tasks.First(t => t.Summary == "Implement Front End Service").TaskId,
                            Summary = "Create CI/CD pipeline for Front End Service",
                            ProjectId = projectId,
                            UserId = context.Users.First(u => u.EmployeeId == "100005").UserId
                        },
                    new Task
                        {
                            ParentId = context.Tasks.First(t => t.Summary == "Implement Front End").TaskId,
                            Summary = "Create Front End Repository",
                            ProjectId = projectId,
                            UserId = context.Users.First(u => u.EmployeeId == "100006").UserId
                        },
                    new Task
                        {
                            ParentId = context.Tasks.First(t => t.Summary == "Implement Front End").TaskId,
                            Summary = "Create React/Redux Implementation of the Front End",
                            ProjectId = projectId,
                            UserId = context.Users.First(u => u.EmployeeId == "100007").UserId
                        },
                    new Task
                        {
                            ParentId = context.Tasks.First(t => t.Summary == "Implement Front End").TaskId,
                            Summary = "Create CI/CD pipeline for Front End",
                            ProjectId = projectId,
                            UserId = context.Users.First(u => u.EmployeeId == "100008").UserId
                        },
                };

            foreach (Task t in childTasks)
            {
                context.Tasks.Add(t);
            }

            context.SaveChanges();
        }
    }
}