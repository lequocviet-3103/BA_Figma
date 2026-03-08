using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIBA.Domain.Enities
{
    public class Project
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private readonly List<RequirementAnalysis> _analyses = new();
        public IReadOnlyCollection<RequirementAnalysis> Analyses => _analyses;

        private Project() { }

        public Project(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            CreatedAt = DateTime.UtcNow;
        }

        public void AddAnalysis(RequirementAnalysis analysis)
        {
            _analyses.Add(analysis);
        }
    }
}
