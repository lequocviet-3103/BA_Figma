using AIBA.Domain.Enities;
using AIBA.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIBA.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;

        public ProjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Project project)
        {
            await _context.Projects.AddAsync(project);
        }

        public async Task<Project?> GetByIdAsync(Guid id)
        {
            return await _context.Projects
                .Include(p => p.Analyses)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
