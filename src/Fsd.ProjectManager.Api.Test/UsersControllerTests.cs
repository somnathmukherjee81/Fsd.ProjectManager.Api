// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UsersControllerTests.cs" company="Somnath Mukherjee">
//    Copyright (c) Somnath Mukherjee. All rights reserved.
// </copyright>
// <summary>
//   Unit tests for the Users Controller
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
    public class UsersControllerTests
    {
        /// <summary>
        /// The users controller
        /// </summary>
        private UsersController _usersController;

        /// <summary>
        /// The users
        /// </summary>
        private List<User> _users;

        /// <summary>
        /// The mock database set
        /// </summary>
        private Mock<DbSet<User>> _mockDbSet;

        /// <summary>
        /// The mock context
        /// </summary>
        private Mock<ProjectManagerContext> _mockContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersControllerTests"/> class.
        /// </summary>
        public UsersControllerTests()
        {
            _users = UsersControllerTestHelper.CreateUserList();

            _mockDbSet = UsersControllerTestHelper.CreateMockUserDbSet(_users);
            _mockContext = UsersControllerTestHelper.CreateMockUserContext(_mockDbSet.Object);

            _usersController = new UsersController(_mockContext.Object);
            _usersController.ControllerContext = new ControllerContext();
            _usersController.ControllerContext.HttpContext = new DefaultHttpContext();
            _usersController.ControllerContext.HttpContext.Request.Headers["test-header"] = "test-header-value";
        }

        /// <summary>
        /// Test method used to validate that GetAllUsers returns all the users
        /// </summary>
        [Fact]
        public void When_GetAllUsersIsCalled_AllUsersAreReturned()
        {
            // Act
            var results = _usersController.Get();

            // Assert
            Assert.Equal(_users.Count, results.Value.Count);
        }

        /// <summary>
        /// Test method used to validate that GetAllUsers with employee id returns the matching users
        /// </summary>
        [Fact]
        public void When_GetAllUsersIsCalledWithEmployeeId_MatchingUsersAreReturned()
        {
            // Act
            var results = _usersController.Get("100001");

            // Assert
            Assert.Equal(1, results.Value.Count);
            Assert.Equal(2, results.Value[0].UserId);
            Assert.Equal("Donald", results.Value[0].FirstName);
            Assert.Equal("Knuth", results.Value[0].LastName);
            Assert.Equal("100001", results.Value[0].EmployeeId);
        }

        /// <summary>
        /// Test method used to validate that GetUserById retirns the correct user
        /// </summary>
        [Fact]
        public void When_GetUserByIdIsCalled_TheMatchingUserIsReturned()
        {
            // Act
            var result = _usersController.Get(2);

            // Assert
            Assert.NotNull(result.Value);
            Assert.Equal(2, result.Value.UserId);
            Assert.Equal("Donald", result.Value.FirstName);
            Assert.Equal("Knuth", result.Value.LastName);
            Assert.Equal("100001", result.Value.EmployeeId);
        }

        /// <summary>
        /// Test method used to validate that CreateUser adds and saves a user
        /// </summary>
        [Fact]
        public void When_CreateUserIsCalled_UserIsAddedAndSaved()
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
            var result = _usersController.Post(newUser);

            // Assert
            _mockDbSet.Verify(m => m.Add(It.IsAny<User>()), Times.Once());
            _mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Test method used to validate that UpdateUser updates and saves a user
        /// </summary>
        [Fact]
        public void When_UpdateUserIsCalled_UserIsUpdatedAndSaved()
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
            var result = _usersController.Put(2, updatedUser);

            // Assert
            _mockDbSet.Verify(m => m.Find(It.IsAny<int>()), Times.Once());
            _mockDbSet.Verify(m => m.Update(It.IsAny<User>()), Times.Once());
            _mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Test method used to validate that DeleteUser deletes and saves a user
        /// </summary>
        [Fact]
        public void When_DeleteUserIsCalled_UserIsDeletedAndSaved()
        {
            // Act
            var result = _usersController.Delete(2);

            // Assert
            _mockDbSet.Verify(m => m.Find(It.IsAny<int>()), Times.Once());
            _mockDbSet.Verify(m => m.Remove(It.IsAny<User>()), Times.Once());
            _mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }        
    }
}
