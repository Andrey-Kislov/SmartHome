using System.Collections.Generic;

namespace Andead.SmartHome.UnitOfWork.Entities
{
    public class WorkflowStep : Entity
    {
        public string StepName { get; set; }

        public virtual Workflow Workflow { get; set; }

        public int WorkflowId { get; set; }

        public virtual WorkflowLogic WorkflowLogic { get; set; }
        
        public int WorkflowLogicId { get; set; }

        public bool IsFirstStep { get; set; }

        public virtual WorkflowStep ParentStep { get; set; }

        public int? ParentStepId { get; set; }

        public virtual IList<WorkflowStep> NextSteps { get; set; }
    }
}
