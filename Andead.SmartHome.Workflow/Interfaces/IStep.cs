using System.Collections.Generic;

namespace Andead.SmartHome.Workflow.Interfaces
{
    public interface IStep
    {
        long Id { get; set; }

        string Name { get; set; }

        IList<IStep> NextSteps { get; }

        void SetNextSteps(IList<IStep> nextSteps);

        bool Run();

        bool Next();

        bool IsStarted();

        void SetStarted();

        bool IsSuccessed();

        void SetSuccessed();
    }
}
