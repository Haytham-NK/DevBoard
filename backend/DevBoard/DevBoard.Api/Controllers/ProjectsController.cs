using DevBoard.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace DevBoard.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private static readonly List<Project> _projects = new List<Project>
        {
            new Project
            {
                Id = Guid.NewGuid(),
                Name = "First project",
                Description = "Description",
                CreatedAt = DateTime.UtcNow,
            },
            new Project
            {
                Id = Guid.NewGuid(),
                Name = "Second project",
                Description = "abababababa",
                CreatedAt = DateTime.UtcNow,
            }
        };

        [HttpGet]
        public List<Project> GetAllProjects()
        {
            return _projects;
        }

        [HttpGet("{id}")]
        public ActionResult<Project> GetProject(Guid id)
        {
            var project = _projects.FirstOrDefault(project => project.Id == id);

            if (project is not null)
            {
                return project;
            }
           
            return NotFound("Project not found");  
        }

        [HttpPost]
        public ActionResult<Project> CreateProject(string name, string description)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Project needs a name");
            }
            return new Project
            {
                Id= Guid.NewGuid(),
                Name = name,
                Description = description,
                CreatedAt = DateTime.UtcNow,
            };
        }
    }
}
