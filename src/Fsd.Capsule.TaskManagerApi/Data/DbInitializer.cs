//-----------------------------------------------------------------------
// <copyright file="DbInitializer.cs" company="Somnath Mukherjee">
//     Copyright (c) Somnath Mukherjee. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Fsd.Capsule.TaskManagerApi.Data
{
    using System;
    using System.Linq;
    using Fsd.Capsule.TaskManagerApi.Models;


    /// <summary>
    /// Database Initializer
    /// </summary>
    public static class DbInitializer
    {
        /// <summary>
        /// Initializes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public static void Initialize(TaskContext context)
        {
            context.Database.EnsureCreated();

            // Look for any task.
            if (context.Tasks.Any())
            {
                // DB has been seeded
                return;
            }

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
                                        Status = Status.NotStarted
                                    },
                                new Task
                                    {
                                        Summary = "Implement Front End Service",
                                        Description =
                                            "Implement Front End Service for the feature which will invoke the Channel Service",
                                        StartDate = DateTime.Parse("2018-07-09"),
                                        EndDate = DateTime.Parse("2018-07-13"),
                                        Priority = Priority.High,
                                        Status = Status.NotStarted
                                    },
                                new Task
                                    {
                                        Summary = "Implement Front End",
                                        Description =
                                            "Implement Front End for the feature which will invoke the Front End Service",
                                        StartDate = DateTime.Parse("2018-07-16"),
                                        EndDate = DateTime.Parse("2018-07-20"),
                                        Priority = Priority.High,
                                        Status = Status.NotStarted
                                    }
                            };

            foreach (Task t in parentTasks)
            {
                context.Tasks.Add(t);
            }

            context.SaveChanges();

            var childTasks = new[]
                                  {
                                      new Task
                                          {
                                              ParentID = context.Tasks.First(t => t.Summary == "Implement Channel Service").TaskID,
                                              Summary = "Create Channel Service Repository"                                              
                                          },
                                      new Task
                                          {
                                              ParentID = context.Tasks.First(t => t.Summary == "Implement Channel Service").TaskID,
                                              Summary = "Create ASP.Net Core WebApi for Channel Service",
                                          },
                                      new Task
                                          {
                                              ParentID = context.Tasks.First(t => t.Summary == "Implement Channel Service").TaskID,
                                              Summary = "Create CI/CD pipeline for Channel Service",
                                          },
                                      new Task
                                          {
                                              ParentID = context.Tasks.First(t => t.Summary == "Implement Front End Service").TaskID,
                                              Summary = "Create Front End Service Repository"
                                          },
                                      new Task
                                          {
                                              ParentID = context.Tasks.First(t => t.Summary == "Implement Front End Service").TaskID,
                                              Summary = "Create Node.js service for Front End Service",
                                          },
                                      new Task
                                          {
                                              ParentID = context.Tasks.First(t => t.Summary == "Implement Front End Service").TaskID,
                                              Summary = "Create CI/CD pipeline for Front End Service",
                                          },
                                      new Task
                                          {
                                              ParentID = context.Tasks.First(t => t.Summary == "Implement Front End").TaskID,
                                              Summary = "Create Front End Repository"
                                          },
                                      new Task
                                          {
                                              ParentID = context.Tasks.First(t => t.Summary == "Implement Front End").TaskID,
                                              Summary = "Create React/Redux Implementation of the Front End",
                                          },
                                      new Task
                                          {
                                              ParentID = context.Tasks.First(t => t.Summary == "Implement Front End").TaskID,
                                              Summary = "Create CI/CD pipeline for Front End",
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