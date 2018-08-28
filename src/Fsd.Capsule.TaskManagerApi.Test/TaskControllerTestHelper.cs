// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaskControllerTestHelper.cs" company="Somnath Mukherjee">
//    Copyright (c) Somnath Mukherjee. All rights reserved.
// </copyright>
// <summary>
//   The sample test
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fsd.Capsule.TaskManagerApi.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Fsd.Capsule.TaskManagerApi.Controllers;
    using Fsd.Capsule.TaskManagerApi.Models;
    using Microsoft.EntityFrameworkCore;
    using Moq;

    /// <summary>
    /// Helper class for <see cref="TasksController"/> tests
    /// </summary>
    public static class TaskControllerTestHelper
    {
        /// <summary>
        /// Creates the mock task DbSet.
        /// </summary>
        /// <param name="tasks">The tasks.</param>
        /// <returns>The Mocked DbSet</returns>
        public static Mock<DbSet<Task>> CreateMockTaskDbSet(List<Task> tasks)
        {
            var data = tasks.AsQueryable();
            var mockDbSet = new Mock<DbSet<Task>>();
            mockDbSet.As<IQueryable<Task>>().Setup(m => m.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Task>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Task>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Task>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockDbSet.Setup(ds => ds.Find(It.IsAny<object[]>())).Returns((object[] key) => tasks.FirstOrDefault(task => task.TaskID == (int)key.First()));
            mockDbSet.Setup(x => x.Add(It.IsAny<Task>())).Callback<Task>(task => tasks.Add(task));
            mockDbSet.Setup(x => x.Update(It.IsAny<Task>())).Callback<Task>(
                task =>
                    {
                        int index = tasks.FindIndex(t => t.TaskID == task.TaskID);
                        tasks[index] = task;
                    });
            mockDbSet.Setup(x => x.Remove(It.IsAny<Task>())).Callback<Task>(task => tasks.Remove(task));

            return mockDbSet;
        }

        /// <summary>
        /// Creates the mock task context.
        /// </summary>
        /// <param name="databaseSet">The DbSet</param>
        /// <returns>The Mocked Task Context</returns>
        public static Mock<TaskContext> CreateMockTaskContext(DbSet<Task> databaseSet)
        {
            var mockContext = new Mock<TaskContext>();

            mockContext.Setup(c => c.Tasks).Returns(databaseSet);

            return mockContext;
        }

        /// <summary>
        /// Creates a sample task list.
        /// </summary>
        /// <returns>The List of tasks</returns>
        public static List<Task> CreateTaskList()
        {
            return new List<Task>
                {
                    new Task
                        {
                            TaskID = 1,
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
                            TaskID = 2,
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
                            TaskID = 3,
                            Summary = "Implement Front End",
                            Description =
                                "Implement Front End for the feature which will invoke the Front End Service",
                            StartDate = DateTime.Parse("2018-07-16"),
                            EndDate = DateTime.Parse("2018-07-20"),
                            Priority = Priority.High,
                            Status = Status.NotStarted
                        }
                };
        }
    }
}
