// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TasksController.cs" company="Somnath Mukherjee">
// Copyright (c) Somnath Mukherjee. All rights reserved.
// </copyright>
// <summary>
// Web Api for Task Management
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
    /// Tasks Controller 
    /// </summary>
    [Produces("application/json")]
    [Route("/[controller]")]
    [ApiController]
    public class TasksController : Controller
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
        /// Initializes a new instance of the <see cref="TasksController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public TasksController(ProjectManagerContext context)
        {
            _context = context;
        }

        /// <summary>
        /// The GET method for retrieving all tasks
        /// </summary>
        /// <returns>
        /// The json serialized list of task.
        /// </returns>
        /// <remarks>GET: /Tasks</remarks>
        [HttpGet(Name = "GetAllTasks")]
        public ActionResult<IList<Task>> Get()
        {
            var headerInformation = new HeaderInformation(Request.Headers);

            return _context.Tasks.ToList();
        }

        /// <summary>
        /// The GET method for retrieving a tasks
        /// </summary>
        /// <param name="id">Id of the Task</param>
        /// <returns>
        /// The json serialized task.
        /// </returns>
        /// <remarks>GET: /Tasks/5</remarks>
        [HttpGet("{id}", Name = "GetTaskById")]
        public ActionResult<Task> Get(int id)
        {
            var headerInformation = new HeaderInformation(Request.Headers);

            var item = _context.Tasks.Find(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        /// <summary>
        /// The POST method for creating a task
        /// </summary>
        /// <param name="task">Task payload</param>
        /// <returns>The <see cref="Task"/> object created</returns>
        /// <remarks>POST: /Tasks</remarks>
        [HttpPost(Name = "CreateTask")]
        public IActionResult Post([FromBody]Task task)
        {
            if (!ModelState.IsValid)
            {
                throw new ApiException(ModelStateError, 500, ModelState);
            }

            var headerInformation = new HeaderInformation(Request.Headers);

            _context.Tasks.Add(task);
            _context.SaveChanges();

            IActionResult result = CreatedAtRoute("GetTaskById", new { id = task.TaskId }, task);

            return result;
        }

        /// <summary>
        /// The POST method for updating a task
        /// </summary>
        /// <param name="id">Id of the Task</param>
        /// <param name="task">Task payload</param>
        /// <returns>The updated <see cref="Task"/> object</returns>
        /// <remarks>PUT: /Tasks/5</remarks>
        [HttpPut("{id}", Name = "UpdateTask")]
        public IActionResult Put(int id, [FromBody]Task task)
        {
            if (!ModelState.IsValid)
            {
                throw new ApiException(ModelStateError, 500, ModelState);
            }

            var headerInformation = new HeaderInformation(Request.Headers);

            var taskToUpdate = _context.Tasks.Find(id);
            if (taskToUpdate == null)
            {
                return NotFound();
            }

            taskToUpdate.UpdateWith(task);

            _context.Tasks.Update(taskToUpdate);
            _context.SaveChanges();

            IActionResult result = CreatedAtRoute("GetTaskById", new { id = taskToUpdate.TaskId }, task);

            return result;
        }

        /// <summary>
        /// The POST method for deleting a task
        /// </summary>
        /// <param name="id">Id of the Task</param>
        /// <returns>No Content</returns>
        /// <remarks>DELETE: /Tasks/5</remarks>
        [HttpDelete("{id}", Name = "DeleteTask")]
        public IActionResult Delete(int id)
        {
            var headerInformation = new HeaderInformation(Request.Headers);

            var taskToDelete = _context.Tasks.Find(id);
            if (taskToDelete == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(taskToDelete);
            _context.SaveChanges();

            return NoContent();
        }
    }
}