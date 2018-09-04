// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UsersControllerTestHelper.cs" company="Somnath Mukherjee">
//    Copyright (c) Somnath Mukherjee. All rights reserved.
// </copyright>
// <summary>
//   Helper for Users Controller Tests
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
    /// Helper class for <see cref="UsersController"/> tests
    /// </summary>
    public static class UsersControllerTestHelper
    {
        /// <summary>
        /// Creates the mock user DbSet.
        /// </summary>
        /// <param name="users">The users.</param>
        /// <returns>The Mocked DbSet</returns>
        public static Mock<DbSet<User>> CreateMockUserDbSet(List<User> users)
        {
            var data = users.AsQueryable();
            var mockDbSet = new Mock<DbSet<User>>();
            mockDbSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockDbSet.Setup(ds => ds.Find(It.IsAny<object[]>())).Returns((object[] key) => users.FirstOrDefault(user => user.UserId == (int)key.First()));
            mockDbSet.Setup(x => x.Add(It.IsAny<User>())).Callback<User>(user => users.Add(user));
            mockDbSet.Setup(x => x.Update(It.IsAny<User>())).Callback<User>(
                user =>
                    {
                        int index = users.FindIndex(t => t.UserId == user.UserId);
                        users[index] = user;
                    });
            mockDbSet.Setup(x => x.Remove(It.IsAny<User>())).Callback<User>(user => users.Remove(user));

            return mockDbSet;
        }

        /// <summary>
        /// Creates the mock user context.
        /// </summary>
        /// <param name="databaseSet">The DbSet</param>
        /// <returns>The Mocked User Context</returns>
        public static Mock<ProjectManagerContext> CreateMockUserContext(DbSet<User> databaseSet)
        {
            var mockContext = new Mock<ProjectManagerContext>();

            mockContext.Setup(c => c.Users).Returns(databaseSet);

            return mockContext;
        }

        /// <summary>
        /// Creates a sample user list.
        /// </summary>
        /// <returns>The List of user</returns>
        public static List<User> CreateUserList()
        {
            return new List<User>
                {
                    new User
                        {
                            UserId = 1,
                            FirstName = "Alan",
                            LastName = "Turiing",
                            EmployeeId = "100000",
                            ProjectId = 1
                        },
                    new User
                        {
                            UserId = 2,
                            FirstName = "Donald",
                            LastName = "Knuth",
                            EmployeeId = "100001",
                            ProjectId = 1
                        },
                    new User
                        {
                            UserId = 3,
                            FirstName = "Ken",
                            LastName = "Thompson",
                            EmployeeId = "100002",
                            ProjectId = 1
                        },
                    new User
                        {
                            UserId = 4,
                            FirstName = "Larry",
                            LastName = "Page",
                            EmployeeId = "100003",
                            ProjectId = 1
                        },
                    new User
                        {
                            UserId = 5,
                            FirstName = "Niklaus",
                            LastName = "Wirth",
                            EmployeeId = "100004",
                            ProjectId = 1
                        },
                    new User
                        {
                            UserId = 6,
                            FirstName = "Dennis",
                            LastName = "Ritchie",
                            EmployeeId = "100005",
                            ProjectId = 1
                        },
                    new User
                        {
                            UserId = 7,
                            FirstName = "Edsger",
                            LastName = "Dijkstra",
                            EmployeeId = "100006",
                            ProjectId = 1
                        },
                    new User
                        {
                            UserId = 8,
                            FirstName = "Claude",
                            LastName = "Shannon",
                            EmployeeId = "100007"
                        },
                    new User
                        {
                            UserId = 9,
                            FirstName = "John von",
                            LastName = "Newmann",
                            EmployeeId = "100008",
                            ProjectId = 1
                        },
                    new User
                        {
                            UserId = 10,
                            FirstName = "Brian",
                            LastName = "Kernighan",
                            EmployeeId = "100009",
                            ProjectId = 1
                        },
                    new User
                        {
                            UserId = 11,
                            FirstName = "Bjarne",
                            LastName = "Stroustrup",
                            EmployeeId = "100010",
                            ProjectId = 1
                        },
                    new User
                        {
                            UserId = 12,
                            FirstName = "Larry",
                            LastName = "Wall",
                            EmployeeId = "100011",
                            ProjectId = 1
                        },
                    new User
                        {
                            UserId = 13,
                            FirstName = "Mark",
                            LastName = "Zuckerberg",
                            EmployeeId = "100012",
                            ProjectId = 1
                        },
                    new User
                        {
                            UserId = 14,
                            FirstName = "Jeff",
                            LastName = "Sutherland",
                            EmployeeId = "100013",
                            ProjectId = 1,
                            ManagedProjectId = 1
                        }
                };
        }
    }
}
