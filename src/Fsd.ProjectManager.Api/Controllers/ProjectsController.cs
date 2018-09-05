// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectsController.cs" company="Somnath Mukherjee">
// Copyright (c) Somnath Mukherjee. All rights reserved.
// </copyright>
// <summary>
// Web Api for Project Management
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
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Projects Controller 
    /// </summary>
    [Produces("application/json")]
    [Route("/[controller]")]
    [ApiController]
    public class ProjectsController : Controller
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
        /// Initializes a new instance of the <see cref="ProjectsController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ProjectsController(ProjectManagerContext context)
        {
            _context = context;
        }

        /// <summary>
        /// The GET method for retrieving all projects
        /// </summary>
        /// <returns>
        /// The json serialized list of project.
        /// </returns>
        /// <remarks>GET: /Projects</remarks>
        [HttpGet(Name = "GetAllProjects")]
        public ActionResult<IList<Project>> Get()
        {
            var headerInformation = new HeaderInformation(Request.Headers);

            return _context.Projects
                .Include(project => project.Tasks)
                .ToList();
        }

        /// <summary>
        /// The GET method for retrieving a projects
        /// </summary>
        /// <param name="id">Id of the Project</param>
        /// <returns>
        /// The json serialized project.
        /// </returns>
        /// <remarks>GET: /Projects/5</remarks>
        [HttpGet("{id}", Name = "GetProjectById")]
        public ActionResult<Project> Get(int id)
        {
            var headerInformation = new HeaderInformation(Request.Headers);

            var item = _context.Projects.Find(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        /// <summary>
        /// The GET method for retrieving the manager of a projects
        /// </summary>
        /// <param name="id">Id of the Project</param>
        /// <returns>
        /// The json serialized project.
        /// </returns>
        /// <remarks>GET: /Projects/5/Manager</remarks>
        [HttpGet("{id}/Manager", Name = "GetManagerByProjectId")]
        public ActionResult<User> GetManager(int id)
        {
            var headerInformation = new HeaderInformation(Request.Headers);

            var item = _context.Projects.Find(id);

            if (item == null)
            {
                return NotFound();
            }

            _context.Entry(item)
                .Reference(project => project.Manager)
                .Load();

            if (item.Manager != null)
            {
                return item.Manager;
            }

            return NoContent();
        }

        /// <summary>
        /// The GET method for retrieving the tasks of a projects
        /// </summary>
        /// <param name="id">Id of the Project</param>
        /// <returns>
        /// The json serialized project.
        /// </returns>
        /// <remarks>GET: /Projects/5/Tasks</remarks>
        [HttpGet("{id}/Tasks", Name = "GetTasksByProjectId")]
        public ActionResult<IList<Task>> GetTasks(int id)
        {
            var headerInformation = new HeaderInformation(Request.Headers);

            var item = _context.Projects.Find(id);

            if (item == null)
            {
                return NotFound();
            }

            _context.Entry(item)
                .Collection(project => project.Tasks)
                .Load();

            if (item.Tasks != null)
            {
                return item.Tasks.ToList();
            }

            return new List<Task>();
        }

        /// <summary>
        /// The GET method for retrieving the members of a projects
        /// </summary>
        /// <param name="id">Id of the Project</param>
        /// <returns>
        /// The json serialized project.
        /// </returns>
        /// <remarks>GET: /Projects/5/Members</remarks>
        [HttpGet("{id}/Members", Name = "GetMembersByProjectId")]
        public ActionResult<IList<User>> GetMembers(int id)
        {
            var headerInformation = new HeaderInformation(Request.Headers);

            var item = _context.Projects.Find(id);

            if (item == null)
            {
                return NotFound();
            }

            _context.Entry(item)
                .Collection(project => project.Members)
                .Load();

            if (item.Members != null)
            {
                return item.Members.ToList();
            }

            return new List<User>();
        }

        /// <summary>
        /// The POST method for creating a project
        /// </summary>
        /// <param name="project">Project payload</param>
        /// <returns>The <see cref="Project"/> object created</returns>
        /// <remarks>POST: /Projects</remarks>
        [HttpPost(Name = "CreateProject")]
        public IActionResult Post([FromBody]Project project)
        {
            if (!ModelState.IsValid)
            {
                throw new ApiException(ModelStateError, 500, ModelState);
            }

            var headerInformation = new HeaderInformation(Request.Headers);

            _context.Projects.Add(project);
            _context.SaveChanges();

            IActionResult result = CreatedAtRoute("GetProjectById", new { id = project.ProjectId }, project);

            return result;
        }

        /// <summary>
        /// The POST method for updating a project
        /// </summary>
        /// <param name="id">Id of the Project</param>
        /// <param name="project">Project payload</param>
        /// <returns>The updated <see cref="Project"/> object</returns>
        /// <remarks>PUT: /Projects/5</remarks>
        [HttpPut("{id}", Name = "UpdateProject")]
        public IActionResult Put(int id, [FromBody]Project project)
        {
            if (!ModelState.IsValid)
            {
                throw new ApiException(ModelStateError, 500, ModelState);
            }

            var headerInformation = new HeaderInformation(Request.Headers);

            var projectToUpdate = _context.Projects.Find(id);
            if (projectToUpdate == null)
            {
                return NotFound();
            }

            projectToUpdate.UpdateWith(project);

            _context.Projects.Update(projectToUpdate);
            _context.SaveChanges();

            IActionResult result = CreatedAtRoute("GetProjectById", new { id = projectToUpdate.ProjectId }, project);

            return result;
        }

        /// <summary>
        /// The POST method for deleting a project
        /// </summary>
        /// <param name="id">Id of the Project</param>
        /// <returns>No Content</returns>
        /// <remarks>DELETE: /Projects/5</remarks>
        [HttpDelete("{id}", Name = "DeleteProject")]
        public IActionResult Delete(int id)
        {
            var headerInformation = new HeaderInformation(Request.Headers);

            var projectToDelete = _context.Projects.Find(id);
            if (projectToDelete == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(projectToDelete);
            _context.SaveChanges();

            return NoContent();
        }
    }
}