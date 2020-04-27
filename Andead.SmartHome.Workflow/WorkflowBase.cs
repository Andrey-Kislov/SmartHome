using System;
using Andead.SmartHome.Workflow.Interfaces;

namespace Andead.SmartHome.Workflow
{
    public abstract class WorkflowBase : IWorkflow
    {
        private readonly IStep _fistStep;
        private readonly IWorkflowAction _action;

        public WorkflowBase()
        {
        }

        public WorkflowBase(IStep fistStep, IWorkflowAction action)
        {
            _fistStep = fistStep ?? throw new ArgumentException(nameof(fistStep));
            _action = action ?? throw new ArgumentException(nameof(action));
        }

        public bool Action()
        {
            return _action.RunAction();
        }

        private bool CheckSteps()
        {
            if (_fistStep != null)
            {
                _fistStep.SetStarted();
                var stepResult = _fistStep.Run();

                if (stepResult)
                {
                    _fistStep.SetSuccessed();

                    if (_fistStep.NextSteps.Count == 0)
                        return true;

                    return _fistStep.Next();
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
