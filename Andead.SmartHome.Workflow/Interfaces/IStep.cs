using System.Collections.Generic;

namespace Andead.SmartHome.Workflow.Interfaces
{
    public interface IStep
    {
        void SetNextSteps(IList<IStep> nextSteps);

        bool Run();

        bool Next();

        bool IsStarted();

        void SetStarted();

        bool IsSuccessed();

        void SetSuccessed();
    }
}
