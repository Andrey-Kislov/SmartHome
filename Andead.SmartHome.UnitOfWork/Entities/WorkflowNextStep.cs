namespace Andead.SmartHome.UnitOfWork.Entities
{
    public class WorkflowNextStep : Entity
    {
        public virtual WorkflowStep PreviousStep { get; set; }

        public int PreviousStepId { get; set; }

        public virtual WorkflowStep NextStep { get; set; }

        public int NextStepId { get; set; }
    }
}
