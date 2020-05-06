namespace Andead.SmartHome.Presentation.API.Models
{
    public class WorkflowStepDto
    {
        public string StepName { get; set; }
        public int WorkflowId { get; set; }
        public int WorkflowLogicId { get; set; }
        public int? ParentStepId { get; set; }
        public bool IsFirstStep { get; set; }
    }
}
