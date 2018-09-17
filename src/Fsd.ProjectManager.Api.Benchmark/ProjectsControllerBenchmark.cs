// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectsControllerBenchmark.cs" company="Somnath Mukherjee">
//    Copyright (c) Somnath Mukherjee. All rights reserved.
// </copyright>
// <summary>
//   The performance benchmark tests for Projects Controller
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fsd.ProjectManager.Api.Benchmark
{
    using System;
    using Fsd.ProjectManager.Api.Controllers;
    using Fsd.ProjectManager.Api.Models;
    using Fsd.ProjectManager.Api.Test;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;  
    using NBench;

    /// <summary>
    /// The validator helper tests.
    /// </summary>
    public class ProjectsControllerBenchmark
    {
        /// <summary>
        /// The projects controller
        /// </summary>
        private ProjectsController _projectsController;

        /// <summary>
        /// Setups the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            var projects = ProjectsControllerTestHelper.CreateProjectList();

            var mockDbSet = ProjectsControllerTestHelper.CreateMockProjectDbSet(projects);
            var mockContext = ProjectsControllerTestHelper.CreateMockProjectContext(mockDbSet.Object);

            _projectsController = new ProjectsController(mockContext.Object);
            _projectsController.ControllerContext = new ControllerContext();
            _projectsController.ControllerContext.HttpContext = new DefaultHttpContext();
            _projectsController.ControllerContext.HttpContext.Request.Headers["test-header"] = "test-header-value";
        }

        #region Elapsed Time Tests

        /// <summary>
        /// Test method used to benchmark GetAllProjects
        /// </summary>
        [PerfBenchmark(
            NumberOfIterations = 1, 
            RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, 
            SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        public void Benchmark_Performance_GetAllProjectsElaspedTime()
        {
            _projectsController.Get();
        }

        /// <summary>
        /// Test method used to benchmark GetProjectById
        /// </summary>
        [PerfBenchmark(
            NumberOfIterations = 1,
            RunMode = RunMode.Throughput,
            TestMode = TestMode.Test,
            SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        public void Benchmark_Performance_GetProjectByIdElaspedTime()
        {
            _projectsController.Get(2);
        }

        /// <summary>
        /// Test method used to benchmark CreateProject
        /// </summary>
        [PerfBenchmark(
            NumberOfIterations = 1,
            RunMode = RunMode.Throughput,
            TestMode = TestMode.Test,
            SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        public void Benchmark_Performance_CreateProjectElaspedTime()
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
            _projectsController.Post(newProject);
        }

        /// <summary>
        /// Test method used to benchmark UpdateProject
        /// </summary>
        [PerfBenchmark(
            NumberOfIterations = 1,
            RunMode = RunMode.Throughput,
            TestMode = TestMode.Test,
            SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        public void Benchmark_Performance_UpdateProjectElaspedTime()
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
            _projectsController.Put(2, updatedProject);
        }

        /// <summary>
        /// Test method used to benchmark DeleteProject
        /// </summary>
        [PerfBenchmark(
            NumberOfIterations = 1,
            RunMode = RunMode.Throughput,
            TestMode = TestMode.Test,
            SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        public void Benchmark_Performance_DeleteProjectElaspedTime()
        {
            _projectsController.Delete(2);
        }

        #endregion Elapsed Time Tests

        #region Garbage Collection

        /// <summary>
        /// Test method used to benchmark GetAllProjects
        /// </summary>
        [PerfBenchmark(
            RunMode = RunMode.Iterations, 
            TestMode = TestMode.Measurement)]
        [GcMeasurement(
            GcMetric.TotalCollections, 
            GcGeneration.AllGc)]
        public void Benchmark_Performance_GetAllProjectsGC()
        {
            _projectsController.Get();
        }

        /// <summary>
        /// Test method used to benchmark GetProjectById
        /// </summary>
        [PerfBenchmark(
            RunMode = RunMode.Iterations,
            TestMode = TestMode.Measurement)]
        [GcMeasurement(
            GcMetric.TotalCollections,
            GcGeneration.AllGc)]
        public void Benchmark_Performance_GetProjectByIdGC()
        {
            _projectsController.Get(2);
        }

        /// <summary>
        /// Test method used to benchmark CreateProject
        /// </summary>
        [PerfBenchmark(
            RunMode = RunMode.Iterations,
            TestMode = TestMode.Measurement)]
        [GcMeasurement(
            GcMetric.TotalCollections,
            GcGeneration.AllGc)]
        public void Benchmark_Performance_CreateProjectGC()
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
            _projectsController.Post(newProject);
        }

        /// <summary>
        /// Test method used to benchmark UpdateProject
        /// </summary>
        [PerfBenchmark(
            RunMode = RunMode.Iterations,
            TestMode = TestMode.Measurement)]
        [GcMeasurement(
            GcMetric.TotalCollections,
            GcGeneration.AllGc)]
        public void Benchmark_Performance_UpdateProjectGC()
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
            _projectsController.Put(2, updatedProject);
        }

        /// <summary>
        /// Test method used to benchmark DeleteProject
        /// </summary>
        [PerfBenchmark(
            RunMode = RunMode.Iterations,
            TestMode = TestMode.Measurement)]
        [GcMeasurement(
            GcMetric.TotalCollections,
            GcGeneration.AllGc)]
        public void Benchmark_Performance_DeleteProjectGC()
        {
            _projectsController.Delete(2);
        }

        #endregion Garbage Collection

        #region Memory

        /// <summary>
        /// Test method used to benchmark GetAllProjects
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
        public void Benchmark_Performance_GetAllProjectsMemory()
        {
            _projectsController.Get();
        }

        /// <summary>
        /// Test method used to benchmark GetProjectById
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
        public void Benchmark_Performance_GetProjectByIdMemory()
        {
            _projectsController.Get(2);
        }

        /// <summary>
        /// Test method used to benchmark CreateProject
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
        public void Benchmark_Performance_CreateProjectMemory()
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
            _projectsController.Post(newProject);
        }

        /// <summary>
        /// Test method used to benchmark UpdateProject
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
        public void Benchmark_Performance_UpdateProjectMemory()
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
            _projectsController.Put(2, updatedProject);
        }

        /// <summary>
        /// Test method used to benchmark DeleteProject
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
        public void Benchmark_Performance_DeleteProjectMemory()
        {
            _projectsController.Delete(2);
        }

        #endregion Memory
    }
}
