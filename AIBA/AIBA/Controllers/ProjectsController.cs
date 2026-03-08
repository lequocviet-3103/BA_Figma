using AIBA.Application.DTO;
using AIBA.Application.Usecase;
using AIBA.Application.IService;
using AIBA.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AIBA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly AnalyzeRequirementHandler _handler;
        private readonly IProjectRepository _projectRepository;
        private readonly IGeminiService _geminiService;

        public ProjectsController(
            AnalyzeRequirementHandler handler,
            IProjectRepository projectRepository,
            IGeminiService geminiService)
        {
            _handler = handler;
            _projectRepository = projectRepository;
            _geminiService = geminiService;
        }

        [HttpPost("analyze")]
        public async Task<IActionResult> Analyze(AnalyzeRequirementRequest request)
        {
            try
            {
                var projectId = await _handler.HandleAsync(request);
                return Ok(new AnalyzeResponseDto
                {
                    ProjectId = projectId,
                    Message = "Analysis completed successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message, Details = ex.InnerException?.Message });
            }
        }

        [HttpPost("test-gemini")]
        public async Task<IActionResult> TestGemini([FromBody] TestGeminiRequest request)
        {
            try
            {
                var result = await _geminiService.GenerateAnalysisAsync(request.Idea);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Error = ex.Message,
                    Details = ex.InnerException?.Message,
                    StackTrace = ex.StackTrace
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var project = await _projectRepository.GetByIdAsync(id);
                if (project == null)
                    return NotFound(new { Error = "Project not found" });

                var projectDto = new ProjectDto
                {
                    Id = project.Id,
                    Name = project.Name,
                    CreatedAt = project.CreatedAt,
                    Analyses = project.Analyses.Select(a => new RequirementAnalysisDto
                    {
                        Id = a.Id,
                        Idea = a.Idea,
                        UserStories = a.UserStories,
                        UseCases = a.UseCases,
                        FunctionalRequirements = a.FunctionalRequirements,
                        DatabaseSchema = a.DatabaseSchema,
                        ApiSuggestions = a.ApiSuggestions,
                        CreatedAt = a.CreatedAt
                    }).ToList()
                };

                return Ok(projectDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }

    public class TestGeminiRequest
    {
        public string Idea { get; set; }
    }
}