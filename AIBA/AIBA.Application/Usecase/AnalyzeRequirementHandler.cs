using AIBA.Application.DTO;
using AIBA.Application.IService;
using AIBA.Domain.Enities;
using AIBA.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIBA.Application.Usecase
{
    public class AnalyzeRequirementHandler
    {
        private readonly IGeminiService _geminiService;
        private readonly IProjectRepository _projectRepository;

        public AnalyzeRequirementHandler(
            IGeminiService geminiService,
            IProjectRepository projectRepository)
        {
            _geminiService = geminiService;
            _projectRepository = projectRepository;
        }

        public async Task<Guid> HandleAsync(AnalyzeRequirementRequest request)
        {
            var project = new Project(request.ProjectName);

            var aiResult = await _geminiService.GenerateAnalysisAsync(request.Idea);

            var analysis = new RequirementAnalysis(
                request.Idea,
                aiResult.UserStories,
                aiResult.UseCases,
                aiResult.FunctionalRequirements,
                aiResult.DatabaseSchema,
                aiResult.ApiSuggestions
            );

            project.AddAnalysis(analysis);

            await _projectRepository.AddAsync(project);
            await _projectRepository.SaveChangesAsync();

            return project.Id;
        }
    }
}
