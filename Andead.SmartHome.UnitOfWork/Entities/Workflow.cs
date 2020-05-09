using System.Collections.Generic;

namespace Andead.SmartHome.UnitOfWork.Entities
{
    public class Workflow : Entity
    {
        public string WorkflowName { get; set; }

        public int DeviceId { get; set; }

        public int UserId { get; set; }

        public bool IsActive { get; set; }

        public virtual IList<WorkflowStep> Steps { get; set; }

        public virtual WorkflowAction Action { get; set; }
    }
}
