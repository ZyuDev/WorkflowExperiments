using Microsoft.Extensions.DependencyInjection;
using WorkflowCore.Interface;
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

            _host?.Start();
        }

        [TearDown]
        public void TearDown()
        {
            _host?.Stop();
        }

        [Test]
        public void CheckHost()
        {
            Assert.IsNotNull(_host);
        }

        [Test]
        public void HellowWorldWorkflow_Run()
        {
            _host.StartWorkflow("HelloWorld", 1, null);

            Assert.Pass();
        }

        [Test]
        public void SampleWorkflow_Run()
        {
            _host.StartWorkflow("SampleWorkflow", 1, null);

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
    }
}
