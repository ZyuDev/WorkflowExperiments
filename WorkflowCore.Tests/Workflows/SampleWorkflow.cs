using WorkflowCore.Interface;
using WorkflowCore.Tests.Steps;

namespace WorkflowCore.Tests.Workflows
{
    public class SampleWorkflow: IWorkflow
    {
        public string Id => "SampleWorkflow";
        public int Version => 1;

        public void Build(IWorkflowBuilder<object> builder)
        {
            builder
                .StartWith<MessageStep>()
                    .Input(step => step.Message, data => "Hello from WorkflowCore!")
                .Then<MessageStep>()
                    .Input(step => step.Message, data => "Working...")
                .Then<MessageStep>()
                    .Input(step => step.Message, data => "Workflow completed.");
        }
    }
}
