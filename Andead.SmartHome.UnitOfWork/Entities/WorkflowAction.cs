namespace Andead.SmartHome.UnitOfWork.Entities
{
    public class WorkflowAction : Entity
    {
        public string ActionName { get; set; }

        public long WorkflowId { get; set; }

        public string ClassName { get; set; }
    }
}
