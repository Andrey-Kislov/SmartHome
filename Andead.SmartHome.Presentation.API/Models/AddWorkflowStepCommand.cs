namespace Andead.SmartHome.Presentation.API.Models
{
    public class AddWorkflowStepCommand
    {
        public string StepName { get; set; }
        public int WorkflowId { get; set; }
        public string ClassName { get; set; }
        public bool IsFirstStep { get; set; }
    }
}
