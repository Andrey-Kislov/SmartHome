using System.Collections.Generic;

namespace Andead.SmartHome.Presentation.API.Models
{
    public class WorkflowDto
    {
        public int Id { get; set; }

        public string WorkflowName { get; set; }

        public IList<WorkflowStepDto> Steps { get; set; }
    }
}
