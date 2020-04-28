namespace Andead.SmartHome.Presentation.API.Models
{
    public class AddWorkflowActionCommand
    {
        public string ActionName { get; set; }
        public int WorkflowId { get; set; }
        public string ClassName { get; set; }
    }
}
