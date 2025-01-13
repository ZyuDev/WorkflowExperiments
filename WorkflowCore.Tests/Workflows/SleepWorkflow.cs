using WorkflowCore.Interface;
using WorkflowCore.Tests.Steps;

namespace WorkflowCore.Tests.Workflows
{
    public class SleepWorkflow: IWorkflow
    {
        public string Id => "SleepWorkflow";
        public int Version => 1;

        public void Build(IWorkflowBuilder<object> builder)
        {
            builder
                .StartWith<MessageStep>()
                    .Input(step => step.Message, data => $"Start workflow at {DateTime.Now}")
                .Then<SleepStep>()
                    .Input(step => step.Delay, data => 1000)
                .Then<MessageStep>()
                    .Input(step => step.Message, data => $"Workflow completed at {DateTime.Now}");
        }
    }
}
