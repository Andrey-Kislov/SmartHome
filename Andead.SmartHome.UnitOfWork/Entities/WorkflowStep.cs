namespace Andead.SmartHome.UnitOfWork.Entities
{
    public class WorkflowStep : Entity
    {
        public string StepName { get; set; }

        public Workflow Workflow { get; set; }

        public int WorkflowId { get; set; }

        public string ClassName { get; set; }

        public bool IsFirstStep { get; set; }
    }
}
