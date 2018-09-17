// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UsersControllerBenchmark.cs" company="Somnath Mukherjee">
//    Copyright (c) Somnath Mukherjee. All rights reserved.
// </copyright>
// <summary>
//   The performance benchmark tests for Users Controller
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fsd.ProjectManager.Api.Benchmark
{
    using Fsd.ProjectManager.Api.Controllers;
    using Fsd.ProjectManager.Api.Models;
    using Fsd.ProjectManager.Api.Test;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;  
    using NBench;

    /// <summary>
    /// The validator helper tests.
    /// </summary>
    public class UsersControllerBenchmark
    {
        /// <summary>
        /// The users controller
        /// </summary>
        private UsersController _usersController;

        /// <summary>
        /// Setups the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            var users = UsersControllerTestHelper.CreateUserList();

            var mockDbSet = UsersControllerTestHelper.CreateMockUserDbSet(users);
            var mockContext = UsersControllerTestHelper.CreateMockUserContext(mockDbSet.Object);

            _usersController = new UsersController(mockContext.Object);
            _usersController.ControllerContext = new ControllerContext();
            _usersController.ControllerContext.HttpContext = new DefaultHttpContext();
            _usersController.ControllerContext.HttpContext.Request.Headers["test-header"] = "test-header-value";
        }

        #region Elapsed Time Tests

        /// <summary>
        /// Test method used to benchmark GetAllUsers
        /// </summary>
        [PerfBenchmark(
            NumberOfIterations = 1, 
            RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, 
            SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        public void Benchmark_Performance_GetAllUsersElaspedTime()
        {
            _usersController.Get();
        }

        /// <summary>
        /// Test method used to benchmark GetUserById
        /// </summary>
        [PerfBenchmark(
            NumberOfIterations = 1,
            RunMode = RunMode.Throughput,
            TestMode = TestMode.Test,
            SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        public void Benchmark_Performance_GetUserByIdElaspedTime()
        {
            _usersController.Get(2);
        }

        /// <summary>
        /// Test method used to benchmark CreateUser
        /// </summary>
        [PerfBenchmark(
            NumberOfIterations = 1,
            RunMode = RunMode.Throughput,
            TestMode = TestMode.Test,
            SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        public void Benchmark_Performance_CreateUserElaspedTime()
        {
            // Arrange 
            User newUser = new User
                {
                    UserId = 15,
                    FirstName = "Somnath",
                    LastName = "Mukherjee",
                    EmployeeId = "100014",
                    ProjectId = 1
                };

            // Act
            _usersController.Post(newUser);
        }

        /// <summary>
        /// Test method used to benchmark UpdateUser
        /// </summary>
        [PerfBenchmark(
            NumberOfIterations = 1,
            RunMode = RunMode.Throughput,
            TestMode = TestMode.Test,
            SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        public void Benchmark_Performance_UpdateUserElaspedTime()
        {
            // Arrange 
            User updatedUser = new User
                {
                    UserId = 2,
                    FirstName = "Donald",
                    LastName = "Knuth",
                    EmployeeId = "100001",
                    ProjectId = 2
                };

            // Act
            _usersController.Put(2, updatedUser);
        }

        /// <summary>
        /// Test method used to benchmark DeleteUser
        /// </summary>
        [PerfBenchmark(
            NumberOfIterations = 1,
            RunMode = RunMode.Throughput,
            TestMode = TestMode.Test,
            SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        public void Benchmark_Performance_DeleteUserElaspedTime()
        {
            _usersController.Delete(2);
        }

        #endregion Elapsed Time Tests

        #region Garbage Collection

        /// <summary>
        /// Test method used to benchmark GetAllUsers
        /// </summary>
        [PerfBenchmark(
            RunMode = RunMode.Iterations, 
            TestMode = TestMode.Measurement)]
        [GcMeasurement(
            GcMetric.TotalCollections, 
            GcGeneration.AllGc)]
        public void Benchmark_Performance_GetAllUsersGC()
        {
            _usersController.Get();
        }

        /// <summary>
        /// Test method used to benchmark GetUserById
        /// </summary>
        [PerfBenchmark(
            RunMode = RunMode.Iterations,
            TestMode = TestMode.Measurement)]
        [GcMeasurement(
            GcMetric.TotalCollections,
            GcGeneration.AllGc)]
        public void Benchmark_Performance_GetUserByIdGC()
        {
            _usersController.Get(2);
        }

        /// <summary>
        /// Test method used to benchmark CreateUser
        /// </summary>
        [PerfBenchmark(
            RunMode = RunMode.Iterations,
            TestMode = TestMode.Measurement)]
        [GcMeasurement(
            GcMetric.TotalCollections,
            GcGeneration.AllGc)]
        public void Benchmark_Performance_CreateUserGC()
        {
            // Arrange 
            User newUser = new User
                {
                    UserId = 15,
                    FirstName = "Somnath",
                    LastName = "Mukherjee",
                    EmployeeId = "100014",
                    ProjectId = 1
                };

            // Act
            _usersController.Post(newUser);
        }

        /// <summary>
        /// Test method used to benchmark UpdateUser
        /// </summary>
        [PerfBenchmark(
            RunMode = RunMode.Iterations,
            TestMode = TestMode.Measurement)]
        [GcMeasurement(
            GcMetric.TotalCollections,
            GcGeneration.AllGc)]
        public void Benchmark_Performance_UpdateUserGC()
        {
            // Arrange 
            User updatedUser = new User
                {
                    UserId = 2,
                    FirstName = "Donald",
                    LastName = "Knuth",
                    EmployeeId = "100001",
                    ProjectId = 2
                };

            // Act
            _usersController.Put(2, updatedUser);
        }

        /// <summary>
        /// Test method used to benchmark DeleteUser
        /// </summary>
        [PerfBenchmark(
            RunMode = RunMode.Iterations,
            TestMode = TestMode.Measurement)]
        [GcMeasurement(
            GcMetric.TotalCollections,
            GcGeneration.AllGc)]
        public void Benchmark_Performance_DeleteUserGC()
        {
            _usersController.Delete(2);
        }

        #endregion Garbage Collection

        #region Memory

        /// <summary>
        /// Test method used to benchmark GetAllUsers
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
        public void Benchmark_Performance_GetAllUsersMemory()
        {
            _usersController.Get();
        }

        /// <summary>
        /// Test method used to benchmark GetUserById
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
        public void Benchmark_Performance_GetUserByIdMemory()
        {
            _usersController.Get(2);
        }

        /// <summary>
        /// Test method used to benchmark CreateUser
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
        public void Benchmark_Performance_CreateUserMemory()
        {
            // Arrange 
            User newUser = new User
                {
                    UserId = 15,
                    FirstName = "Somnath",
                    LastName = "Mukherjee",
                    EmployeeId = "100014",
                    ProjectId = 1
                };

            // Act
            _usersController.Post(newUser);
        }

        /// <summary>
        /// Test method used to benchmark UpdateUser
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
        public void Benchmark_Performance_UpdateUserMemory()
        {
            // Arrange 
            User updatedUser = new User
                {
                    UserId = 2,
                    FirstName = "Donald",
                    LastName = "Knuth",
                    EmployeeId = "100001",
                    ProjectId = 2
                };

            // Act
            _usersController.Put(2, updatedUser);
        }

        /// <summary>
        /// Test method used to benchmark DeleteUser
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
        public void Benchmark_Performance_DeleteUserMemory()
        {
            _usersController.Delete(2);
        }

        #endregion Memory
    }
}
