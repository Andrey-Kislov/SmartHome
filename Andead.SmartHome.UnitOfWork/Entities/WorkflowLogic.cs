using System.Collections.Generic;

namespace Andead.SmartHome.UnitOfWork.Entities
{
    public class WorkflowLogic : Entity
    {
        public string LogicName { get; set; }

        public string ClassName { get; set; }

        public virtual IList<WorkflowStep> Steps { get; set; }

        public virtual IList<WorkflowAction> Actions { get; set; }
    }
}
