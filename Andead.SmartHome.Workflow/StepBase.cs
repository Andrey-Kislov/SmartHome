using System.Collections.Generic;
using Andead.SmartHome.Workflow.Interfaces;

namespace Andead.SmartHome.Workflow
{
    public abstract class StepBase : IStep
    {
        private readonly object _lock = new object();
        private bool _started = false;
        private bool _isSuccessed = false;
        private int _timeToLive = 10;

        public IList<IStep> NextSteps { get; } = new List<IStep>();

        public StepBase()
        { }

        public StepBase(IList<IStep> nextSteps)
        {
            NextSteps = nextSteps;
        }

        public bool IsStarted()
        {
            lock (_lock)
            {
                return _started;
            }
        }

        public void SetStarted()
        {
            lock (_lock)
            {
                _started = true;
            }
        }

        public abstract bool Run();

        public bool Next()
        {
            if (_timeToLive == 0)
                return false;

            if (NextSteps.Count == 0)
                return true;

            foreach (var nextStep in NextSteps)
            {
                if (!nextStep.IsStarted())
                {
                    nextStep.SetStarted();

                    if (nextStep.Run())
                    {
                        if (nextStep.Next())
                        {
                            nextStep.SetSuccessed();
                            return true;
                        }
                    }

                    continue;
                }

                if (nextStep.IsSuccessed())
                {
                    _timeToLive--;
                    return nextStep.Next();
                }
            }

            return false;
        }

        public bool IsSuccessed()
        {
            return _isSuccessed;
        }

        public void SetSuccessed()
        {
            _isSuccessed = true;
        }
    }
}
