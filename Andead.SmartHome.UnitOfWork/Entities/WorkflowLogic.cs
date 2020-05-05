using System.Collections.Generic;

namespace Andead.SmartHome.UnitOfWork.Entities
{
    public class WorkflowLogic : Entity
    {
        public string ClassName { get; set; }

        public virtual IList<WorkflowStep> Steps { get; set; }
    }
}
