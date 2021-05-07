using System;
using Moq;
using ProjectUnderTest.Exceptions;
using ProjectUnderTest.Models;
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

            _service = new JobService(_jobRepository.Object, _applicationRepository.Object);
        }

        [Fact]
        public void Apply_Test_ExpectedBehaviour()
        {
            // Arrange
            
            // Act

            // Assert
            
        }
        
        [Fact]
        public void Apply_Test_Throws_ApplicationNotApplicableException()
        {
            // Arrange
            
            // Act

            // Assert
            
        }
        
        [Fact]
        public void Apply_Test_Throws_ApplicationSectorMismatchException()
        {
            // Arrange
            
            // Act

            // Assert
            
        }
    }
}