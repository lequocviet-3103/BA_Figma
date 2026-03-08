using System;

namespace AIBA.Application.DTO
{
    public class ProjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<RequirementAnalysisDto> Analyses { get; set; }
    }

    public class RequirementAnalysisDto
    {
        public Guid Id { get; set; }
        public string Idea { get; set; }
        public string UserStories { get; set; }
        public string UseCases { get; set; }
        public string FunctionalRequirements { get; set; }
        public string DatabaseSchema { get; set; }
        public string ApiSuggestions { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class AnalyzeResponseDto
    {
        public Guid ProjectId { get; set; }
        public string Message { get; set; }
    }
}
