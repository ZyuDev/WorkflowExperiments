using Microsoft.Extensions.DependencyInjection;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCore.Tests.Workflows;

namespace WorkflowCore.Tests
{
    [TestFixture]
    public class WorkflowTests
    {

        private IWorkflowHost? _host;

        [SetUp]
        public void Setup()
        {
            _host = CreateWorkflowHost();
            _host.RegisterWorkflow<HelloWorldWorkflow>();
            _host.RegisterWorkflow<SampleWorkflow>();
            _host.RegisterWorkflow<SleepWorkflow>();

            _host?.Start();
           
        }

        [TearDown]
        public void TearDown()
        {
            _host?.Stop();

            if (_host is IDisposable disposableHost)
            {
                disposableHost.Dispose();
            }

            _host = null;
        }

        [Test]
        public void CheckHost()
        {
            Assert.IsNotNull(_host);
        }

        [Test]
        public async Task HellowWorldWorkflow_Run()
        {
            var workflowId =  await _host.StartWorkflow("HelloWorld", null, Guid.NewGuid().ToString());

            WaitForWorkflowToComplete(workflowId);

            Assert.Pass();
        }

        [Test]
        public async Task SampleWorkflow_Run()
        {
            
           var workflowId =  await _host.StartWorkflow("SampleWorkflow", null, Guid.NewGuid().ToString());
            WaitForWorkflowToComplete(workflowId);

            Assert.Pass();
        }

        [Test]
        public async Task SleepWorkflow_Run()
        {

            var workflowId = await _host.StartWorkflow("SleepWorkflow", null, Guid.NewGuid().ToString());
            WaitForWorkflowToComplete(workflowId);

            Assert.Pass();
        }

        private IWorkflowHost? CreateWorkflowHost()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging();
            serviceCollection.AddWorkflow();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider.GetService<IWorkflowHost>();
        }

        private void WaitForWorkflowToComplete(string workflowId, int timeoutSeconds = 10)
        {
            var startTime = DateTime.Now;
            while ((DateTime.Now - startTime).TotalSeconds < timeoutSeconds)
            {
                var status = _host.PersistenceStore.GetWorkflowInstance(workflowId).Result;

                if (status.Status == WorkflowStatus.Complete || status.Status == WorkflowStatus.Terminated)
                    break;

                Thread.Sleep(500);  // Check every 0.5 seconds
            }
        }
    }
}
