namespace Andead.SmartHome.UnitOfWork.Entities
{
    public class WorkflowNextStep : Entity
    {
        public int PreviousStepId { get; set; }

        public int NextStepId { get; set; }
    }
}
