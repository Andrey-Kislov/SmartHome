namespace Andead.SmartHome.UnitOfWork.Entities
{
    public class WorkflowAction : Entity
    {
        public string ActionName { get; set; }

        public virtual Workflow Workflow { get; set; }

        public int WorkflowId { get; set; }

        public virtual WorkflowLogic WorkflowLogic { get; set; }

        public int WorkflowLogicId { get; set; }
    }
}
