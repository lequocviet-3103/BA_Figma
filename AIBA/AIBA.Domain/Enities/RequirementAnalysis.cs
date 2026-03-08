using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIBA.Domain.Enities
{
    public class RequirementAnalysis
    {
        public Guid Id { get; private set; }
        public string Idea { get; private set; }

        public string UserStories { get; private set; }
        public string UseCases { get; private set; }
        public string FunctionalRequirements { get; private set; }
        public string DatabaseSchema { get; private set; }
        public string ApiSuggestions { get; private set; }

        public DateTime CreatedAt { get; private set; }

        private RequirementAnalysis() { }

        public RequirementAnalysis(
            string idea,
            string userStories,
            string useCases,
            string functional,
            string db,
            string api)
        {
            Id = Guid.NewGuid();
            Idea = idea;
            UserStories = userStories;
            UseCases = useCases;
            FunctionalRequirements = functional;
            DatabaseSchema = db;
            ApiSuggestions = api;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
