using AIBA.Domain.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIBA.Domain.Interfaces
{
    public interface IProjectRepository
    {
        Task AddAsync(Project project);
        Task<Project?> GetByIdAsync(Guid id);
        Task SaveChangesAsync();
    }
}
