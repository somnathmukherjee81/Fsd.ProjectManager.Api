// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UsersController.cs" company="Somnath Mukherjee">
// Copyright (c) Somnath Mukherjee. All rights reserved.
// </copyright>
// <summary>
// Web Api for User Management
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fsd.ProjectManager.Api.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using Exception;
    using Fsd.ProjectManager.Api.Helpers;
    using Fsd.ProjectManager.Api.Models;

    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Users Controller 
    /// </summary>
    [Produces("application/json")]
    [Route("/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        /// <summary>
        /// The model state error
        /// </summary>
        private const string ModelStateError = "Unable to create model. Please see error details for more information";

        /// <summary>
        /// The project manager database context
        /// </summary>
        private readonly ProjectManagerContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UsersController(ProjectManagerContext context)
        {
            _context = context;
        }

        /// <summary>
        /// The GET method for retrieving all users
        /// </summary>
        /// <returns>
        /// The json serialized list of users.
        /// </returns>
        /// <remarks>GET: /Users</remarks>
        [HttpGet(Name = "GetAllUsers")]
        public ActionResult<IList<User>> Get()
        {
            var headerInformation = new HeaderInformation(Request.Headers);

            return _context.Users.ToList();
        }

        /// <summary>
        /// The GET method for retrieving a users
        /// </summary>
        /// <param name="id">Id of the User</param>
        /// <returns>
        /// The json serialized user.
        /// </returns>
        /// <remarks>GET: /Users/5</remarks>
        [HttpGet("{id}", Name = "GetUserById")]
        public ActionResult<User> Get(int id)
        {
            var headerInformation = new HeaderInformation(Request.Headers);

            var user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        /// <summary>
        /// The POST method for creating a user
        /// </summary>
        /// <param name="user">User payload</param>
        /// <returns>The <see cref="User"/> object created</returns>
        /// <remarks>POST: /Users</remarks>
        [HttpPost(Name = "CreateUser")]
        public IActionResult Post([FromBody]User user)
        {
            if (!ModelState.IsValid)
            {
                throw new ApiException(ModelStateError, 500, ModelState);
            }

            var headerInformation = new HeaderInformation(Request.Headers);

            _context.Users.Add(user);
            _context.SaveChanges();

            IActionResult result = CreatedAtRoute("GetUserById", new { id = user.UserId }, user);

            return result;
        }

        /// <summary>
        /// The POST method for updating a user
        /// </summary>
        /// <param name="id">Id of the User</param>
        /// <param name="user">User payload</param>
        /// <returns>The updated <see cref="User"/> object</returns>
        /// <remarks>PUT: /Users/5</remarks>
        [HttpPut("{id}", Name = "UpdateUser")]
        public IActionResult Put(int id, [FromBody]User user)
        {
            if (!ModelState.IsValid)
            {
                throw new ApiException(ModelStateError, 500, ModelState);
            }

            var headerInformation = new HeaderInformation(Request.Headers);

            var userToUpdate = _context.Users.Find(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }

            userToUpdate.UpdateWith(user);

            _context.Users.Update(userToUpdate);
            _context.SaveChanges();

            IActionResult result = CreatedAtRoute("GetUserById", new { id = userToUpdate.UserId }, user);

            return result;
        }

        /// <summary>
        /// The POST method for deleting a user
        /// </summary>
        /// <param name="id">Id of the User</param>
        /// <returns>No Content</returns>
        /// <remarks>DELETE: /Users/5</remarks>
        [HttpDelete("{id}", Name = "DeleteUser")]
        public IActionResult Delete(int id)
        {
            var headerInformation = new HeaderInformation(Request.Headers);

            var userToDelete = _context.Users.Find(id);
            if (userToDelete == null)
            {
                return NotFound();
            }

            _context.Users.Remove(userToDelete);
            _context.SaveChanges();

            return NoContent();
        }
    }
}