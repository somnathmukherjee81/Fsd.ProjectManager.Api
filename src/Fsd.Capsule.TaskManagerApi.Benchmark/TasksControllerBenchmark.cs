// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TasksControllerBenchmark.cs" company="Somnath Mukherjee">
//    Copyright (c) Somnath Mukherjee. All rights reserved.
// </copyright>
// <summary>
//   The sample test
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fsd.Capsule.TaskManagerApi.Benchmark
{
    using System;
    using Fsd.Capsule.TaskManagerApi.Controllers;
    using Fsd.Capsule.TaskManagerApi.Models;
    using Fsd.Capsule.TaskManagerApi.Test;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;  
    using NBench;

    /// <summary>
    /// The validator helper tests.
    /// </summary>
    public class TasksControllerBenchmark
    {
        /// <summary>
        /// The tasks controller
        /// </summary>
        private TasksController _tasksController;

        /// <summary>
        /// Setups the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            var tasks = TaskControllerTestHelper.CreateTaskList();

            var mockDbSet = TaskControllerTestHelper.CreateMockTaskDbSet(tasks);
            var mockContext = TaskControllerTestHelper.CreateMockTaskContext(mockDbSet.Object);

            _tasksController = new TasksController(mockContext.Object);
            _tasksController.ControllerContext = new ControllerContext();
            _tasksController.ControllerContext.HttpContext = new DefaultHttpContext();
            _tasksController.ControllerContext.HttpContext.Request.Headers["test-header"] = "test-header-value";
        }

        #region Elapsed Time Tests

        /// <summary>
        /// Test method used to benchmark GetAllTasks
        /// </summary>
        [PerfBenchmark(
            NumberOfIterations = 1, 
            RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, 
            SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        public void Benchmark_Performance_GetAllTasksElaspedTime()
        {
            _tasksController.Get();
        }

        /// <summary>
        /// Test method used to benchmark GetTaskById
        /// </summary>
        [PerfBenchmark(
            NumberOfIterations = 1,
            RunMode = RunMode.Throughput,
            TestMode = TestMode.Test,
            SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        public void Benchmark_Performance_GetTaskByIdElaspedTime()
        {
            _tasksController.Get(2);
        }

        /// <summary>
        /// Test method used to benchmark CreateTask
        /// </summary>
        [PerfBenchmark(
            NumberOfIterations = 1,
            RunMode = RunMode.Throughput,
            TestMode = TestMode.Test,
            SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        public void Benchmark_Performance_CreateTaskElaspedTime()
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
            _tasksController.Post(newTask);
        }

        /// <summary>
        /// Test method used to benchmark UpdateTask
        /// </summary>
        [PerfBenchmark(
            NumberOfIterations = 1,
            RunMode = RunMode.Throughput,
            TestMode = TestMode.Test,
            SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        public void Benchmark_Performance_UpdateTaskElaspedTime()
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
            _tasksController.Put(2, updatedTask);
        }

        /// <summary>
        /// Test method used to benchmark DeleteTask
        /// </summary>
        [PerfBenchmark(
            NumberOfIterations = 1,
            RunMode = RunMode.Throughput,
            TestMode = TestMode.Test,
            SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        public void Benchmark_Performance_DeleteTaskElaspedTime()
        {
            _tasksController.Delete(2);
        }

        #endregion Elapsed Time Tests

        #region Garbage Collection

        /// <summary>
        /// Test method used to benchmark GetAllTasks
        /// </summary>
        [PerfBenchmark(
            RunMode = RunMode.Iterations, 
            TestMode = TestMode.Measurement)]
        [GcMeasurement(
            GcMetric.TotalCollections, 
            GcGeneration.AllGc)]
        public void Benchmark_Performance_GetAllTasksGC()
        {
            _tasksController.Get();
        }

        /// <summary>
        /// Test method used to benchmark GetTaskById
        /// </summary>
        [PerfBenchmark(
            RunMode = RunMode.Iterations,
            TestMode = TestMode.Measurement)]
        [GcMeasurement(
            GcMetric.TotalCollections,
            GcGeneration.AllGc)]
        public void Benchmark_Performance_GetTaskByIdGC()
        {
            _tasksController.Get(2);
        }

        /// <summary>
        /// Test method used to benchmark CreateTask
        /// </summary>
        [PerfBenchmark(
            RunMode = RunMode.Iterations,
            TestMode = TestMode.Measurement)]
        [GcMeasurement(
            GcMetric.TotalCollections,
            GcGeneration.AllGc)]
        public void Benchmark_Performance_CreateTaskGC()
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
            _tasksController.Post(newTask);
        }

        /// <summary>
        /// Test method used to benchmark UpdateTask
        /// </summary>
        [PerfBenchmark(
            RunMode = RunMode.Iterations,
            TestMode = TestMode.Measurement)]
        [GcMeasurement(
            GcMetric.TotalCollections,
            GcGeneration.AllGc)]
        public void Benchmark_Performance_UpdateTaskGC()
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
            _tasksController.Put(2, updatedTask);
        }

        /// <summary>
        /// Test method used to benchmark DeleteTask
        /// </summary>
        [PerfBenchmark(
            RunMode = RunMode.Iterations,
            TestMode = TestMode.Measurement)]
        [GcMeasurement(
            GcMetric.TotalCollections,
            GcGeneration.AllGc)]
        public void Benchmark_Performance_DeleteTaskGC()
        {
            _tasksController.Delete(2);
        }

        #endregion Garbage Collection

        #region Memory

        /// <summary>
        /// Test method used to benchmark GetAllTasks
        /// </summary>
        [PerfBenchmark(
            NumberOfIterations = 5, 
            RunMode = RunMode.Throughput, 
            RunTimeMilliseconds = 2500, 
            TestMode = TestMode.Test)]
        [MemoryAssertion(
            MemoryMetric.TotalBytesAllocated, 
            MustBe.LessThanOrEqualTo, 
            ByteConstants.SixtyFourKb)]
        public void Benchmark_Performance_GetAllTasksMemory()
        {
            _tasksController.Get();
        }

        /// <summary>
        /// Test method used to benchmark GetTaskById
        /// </summary>
        [PerfBenchmark(
            NumberOfIterations = 5,
            RunMode = RunMode.Throughput,
            RunTimeMilliseconds = 2500,
            TestMode = TestMode.Test)]
        [MemoryAssertion(
            MemoryMetric.TotalBytesAllocated,
            MustBe.LessThanOrEqualTo,
            ByteConstants.SixtyFourKb)]
        public void Benchmark_Performance_GetTaskByIdMemory()
        {
            _tasksController.Get(2);
        }

        /// <summary>
        /// Test method used to benchmark CreateTask
        /// </summary>
        [PerfBenchmark(
            NumberOfIterations = 5,
            RunMode = RunMode.Throughput,
            RunTimeMilliseconds = 2500,
            TestMode = TestMode.Test)]
        [MemoryAssertion(
            MemoryMetric.TotalBytesAllocated,
            MustBe.LessThanOrEqualTo,
            ByteConstants.SixtyFourKb)]
        public void Benchmark_Performance_CreateTaskMemory()
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
            _tasksController.Post(newTask);
        }

        /// <summary>
        /// Test method used to benchmark UpdateTask
        /// </summary>
        [PerfBenchmark(
            NumberOfIterations = 5,
            RunMode = RunMode.Throughput,
            RunTimeMilliseconds = 2500,
            TestMode = TestMode.Test)]
        [MemoryAssertion(
            MemoryMetric.TotalBytesAllocated,
            MustBe.LessThanOrEqualTo,
            ByteConstants.SixtyFourKb)]
        public void Benchmark_Performance_UpdateTaskMemory()
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
            _tasksController.Put(2, updatedTask);
        }

        /// <summary>
        /// Test method used to benchmark DeleteTask
        /// </summary>
        [PerfBenchmark(
            NumberOfIterations = 5,
            RunMode = RunMode.Throughput,
            RunTimeMilliseconds = 2500,
            TestMode = TestMode.Test)]
        [MemoryAssertion(
            MemoryMetric.TotalBytesAllocated,
            MustBe.LessThanOrEqualTo,
            ByteConstants.SixtyFourKb)]
        public void Benchmark_Performance_DeleteTaskMemory()
        {
            _tasksController.Delete(2);
        }

        #endregion Memory
    }
}
