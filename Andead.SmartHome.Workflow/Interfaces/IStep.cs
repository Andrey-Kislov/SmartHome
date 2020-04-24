namespace Andead.SmartHome.Workflow.Interfaces
{
    public interface IStep
    {
        bool Run();

        bool Next();

        bool IsStarted();

        void SetStarted();

        bool IsSuccessed();

        void SetSuccessed();
    }
}
