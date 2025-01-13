using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowCore.Tests.Workflows
{
    public class HelloWorldWorkflow: IWorkflow
    {
        public string Id => "HelloWorld";
        public int Version => 1;

        public void Build(IWorkflowBuilder<object> builder)
        {
            builder
                .StartWith(context =>
                {
                    Console.WriteLine("Workflow: Hello world!");
                    return ExecutionResult.Next();
                })
                .Then(context =>
                {
                    Console.WriteLine("Workflow: See you soon!");
                    return ExecutionResult.Next();
                });
        }
    }
}
