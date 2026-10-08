using DevBoard.Api.Dtos;
using DevBoard.Api.DTOs;
using DevBoard.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevBoard.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        // Temporary in-memory storage until EF Core persistence is introduced
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
        public ActionResult<List<ProjectDto>> GetAllProjects()
        {
            var projectsDtos = _projects
                .Select(projects => new ProjectDto
                {
                    Id = projects.Id,
                    Name = projects.Name,
                    Description = projects.Description,
                    CreatedAt = projects.CreatedAt
                })
                .ToList();

            return projectsDtos;
        }

        [HttpGet("{id}")]
        public ActionResult<ProjectDto> GetProject(Guid id)
        {
            var project = _projects.FirstOrDefault(project => project.Id == id);

            if (project is not null)
            {
                return new ProjectDto
                {
                    Id = project.Id,
                    Name = project.Name,
                    Description = project.Description,
                    CreatedAt = project.CreatedAt
                };
            }

            return NotFound("Project not found");
        }

        [HttpPost]
        public ActionResult<ProjectDto> CreateProject([FromBody] CreateProjectDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return BadRequest("Project needs a name");
            }

            var project = new Project
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                CreatedAt = DateTime.UtcNow,
            };

            _projects.Add(project);

            var projectDto = new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                CreatedAt = project.CreatedAt,
            };

            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, projectDto);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProject(Guid id)
        {
            var project = _projects.FirstOrDefault(x => x.Id == id);

            if (project is null)
            {
                return NotFound("Project not found");
            }

            _projects.Remove(project);

            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateProject(Guid id, [FromBody] UpdateProjectDto dto)
        {
            var project = _projects.FirstOrDefault(x => x.Id == id);

            if (project is null)
            {
                return NotFound("Project not found");
            }

            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return BadRequest("Name can't be empty");
            }

            project.Name = dto.Name;
            project.Description = dto.Description;

            return NoContent();
        }
    }
}