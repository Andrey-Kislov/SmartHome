namespace Andead.SmartHome.UnitOfWork.Entities
{
    public class WorkflowNextStep : Entity
    {
        public long PreviousStepId { get; set; }

        public long NextStepId { get; set; }
    }
}
