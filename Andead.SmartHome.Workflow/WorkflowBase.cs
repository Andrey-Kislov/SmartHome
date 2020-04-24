using System;
using System.Collections.Generic;
using Andead.SmartHome.Workflow.Interfaces;

namespace Andead.SmartHome.Workflow
{
    public abstract class WorkflowBase : IWorkflow
    {
        private readonly IList<IStep> _steps = new List<IStep>();

        public WorkflowBase()
        {
        }

        public WorkflowBase(IList<IStep> steps)
        {
            _steps = steps ?? throw new ArgumentException(nameof(steps));
        }

        public abstract bool Action();

        public void AddStep(IStep step)
        {
            _steps.Add(step);
        }

        private bool CheckSteps()
        {
            if (_steps.Count > 0)
            {
                _steps[0].SetStarted();
                var stepResult = _steps[0].Run();

                if (stepResult)
                {
                    _steps[0].SetSuccessed();

                    if (_steps.Count == 1)
                        return true;

                    return _steps[0].Next();
                }
            }

            return false;
        }

        public bool Start()
        {
            if (CheckSteps())
            {
                return Action();
            }

            return false;
        }
    }
}
