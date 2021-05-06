using Moq;
using ProjectUnderTest.Repositories;
using ProjectUnderTest.Services;
using Xunit;

namespace TestingProject
{
    public class JobServiceTests
    {
        private readonly IJobService _service;
        
        private Mock<IJobRepository> _jobRepository;
        private Mock<IApplicationRepository> _applicationRepository;

        public JobServiceTests()
        {
            _jobRepository = new Mock<IJobRepository>();
            _applicationRepository = new Mock<IApplicationRepository>();
        }

        [Fact]
        public void Apply_Test()
        {
            // Arrange
            
            
            // Act
            

            // Assert


        } 
    }
}