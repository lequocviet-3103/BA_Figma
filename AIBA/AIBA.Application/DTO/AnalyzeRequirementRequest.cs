using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIBA.Application.DTO
{
    public class AnalyzeRequirementRequest
    {
        [Required(ErrorMessage = "Project name is required")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Project name must be between 1 and 200 characters")]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "Idea is required")]
        [StringLength(5000, MinimumLength = 10, ErrorMessage = "Idea must be between 10 and 5000 characters")]
        public string Idea { get; set; }
    }
}
