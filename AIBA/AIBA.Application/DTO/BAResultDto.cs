using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIBA.Application.DTO
{
    public class BAResultDto
    {
        public string UserStories { get; set; }
        public string UseCases { get; set; }
        public string FunctionalRequirements { get; set; }
        public string DatabaseSchema { get; set; }
        public string ApiSuggestions { get; set; }
    }
}
