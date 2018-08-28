// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TasksControllerTests.cs" company="Somnath Mukherjee">
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

    using Fsd.Capsule.TaskManagerApi.Controllers;
    using Fsd.Capsule.TaskManagerApi.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using Moq;
    using Xunit;

    /// <summary>
    /// The validator helper tests.
    /// </summary>
    public class TasksControllerTests
    {
        /// <summary>
        /// The tasks controller
        /// </summary>
        private TasksController _tasksController;

        /// <summary>
        /// The tasks
        /// </summary>
        private List<Task> _tasks;

        /// <summary>
        /// The mock database set
        /// </summary>
        private Mock<DbSet<Task>> _mockDbSet;

        /// <summary>
        /// The mock context
        /// </summary>
        private Mock<TaskContext> _mockContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TasksControllerTests"/> class.
        /// </summary>
        public TasksControllerTests()
        {
            _tasks = TaskControllerTestHelper.CreateTaskList();

            _mockDbSet = TaskControllerTestHelper.CreateMockTaskDbSet(_tasks);
            _mockContext = TaskControllerTestHelper.CreateMockTaskContext(_mockDbSet.Object);

            _tasksController = new TasksController(_mockContext.Object);
            _tasksController.ControllerContext = new ControllerContext();
            _tasksController.ControllerContext.HttpContext = new DefaultHttpContext();
            _tasksController.ControllerContext.HttpContext.Request.Headers["test-header"] = "test-header-value";
        }

        /// <summary>
        /// Test method used to validate that GetAllTasks returns all the tasks
        /// </summary>
        [Fact]
        public void When_GetAllTasksIsCalled_AllTasksAreReturned()
        {
            // Act
            var results = _tasksController.Get();

            // Assert
            Assert.Equal(_tasks.Count, results.Value.Count);
        }

        /// <summary>
        /// Test method used to validate that GetTaskById retirns the correct task
        /// </summary>
        [Fact]
        public void When_GetTaskByIdIsCalled_TheMatchingTaskIsReturned()
        {
            // Act
            var result = _tasksController.Get(2);

            // Assert
            Assert.NotNull(result.Value);
            Assert.Equal(2, result.Value.TaskID);
            Assert.Equal("Implement Front End Service", result.Value.Summary);
        }

        /// <summary>
        /// Test method used to validate that CreateTask adds and saves a task
        /// </summary>
        [Fact]
        public void When_CreateTaskIsCalled_TaskIsAddedAndSaved()
        {
            // Arrange 
            Task newTask = new Task
                {
                    TaskID = 4,
                    Summary = "Implement Plugin",
                    Description =
                        "Implement Plugin for the feature which will invoke the Front End Service",
                    StartDate = DateTime.Parse("2018-07-16"),
                    EndDate = DateTime.Parse("2018-07-20"),
                    Priority = Priority.High,
                    Status = Status.NotStarted
                };

            // Act
            var result = _tasksController.Post(newTask);

            // Assert
            _mockDbSet.Verify(m => m.Add(It.IsAny<Task>()), Times.Once());
            _mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Test method used to validate that UpdateTask updates and saves a task
        /// </summary>
        [Fact]
        public void When_UpdateTaskIsCalled_TaskIsUpdatedAndSaved()
        {
            // Arrange 
            Task updatedTask = new Task
                {
                    TaskID = 2,
                    Summary = "Implement Front End Service - Updated",
                    Description =
                        "Implement Front End Service for the feature which will invoke the Channel Service",
                    StartDate = DateTime.Parse("2018-07-09"),
                    EndDate = DateTime.Parse("2018-07-13"),
                    Priority = Priority.High,
                    Status = Status.NotStarted
                };

            // Act
            var result = _tasksController.Put(2, updatedTask);

            // Assert
            _mockDbSet.Verify(m => m.Find(It.IsAny<int>()), Times.Once());
            _mockDbSet.Verify(m => m.Update(It.IsAny<Task>()), Times.Once());
            _mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Test method used to validate that DeleteTask deletes and saves a task
        /// </summary>
        [Fact]
        public void When_DeleteTaskIsCalled_TaskIsDeletedAndSaved()
        {
            // Act
            var result = _tasksController.Delete(2);

            // Assert
            _mockDbSet.Verify(m => m.Find(It.IsAny<int>()), Times.Once());
            _mockDbSet.Verify(m => m.Remove(It.IsAny<Task>()), Times.Once());
            _mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }        
    }
}
