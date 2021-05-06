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
            var offer = new JobOffer()
            {
                Applicable = false,
                TargetJob = new Job()
                {
                    Sector = Sector.Engineering
                }
            };
            
            var applicant = new Person()
            {
                Sector = Sector.Engineering
            };
            
            // Act
            var act = new Func<Application>(() => _service.Apply(offer, applicant));

            // Assert
            var exception = Assert.Throws<ApplicationNotApplicableException>(act);
            Assert.NotNull(exception);
            Assert.Equal("Cannot apply when not applicable", exception.Message);
        }
        
        [Fact]
        public void Apply_Test_Throws_ApplicationSectorMismatchException()
        {
            // Arrange
            var offer = new JobOffer()
            {
                Applicable = true,
                TargetJob = new Job()
                {
                    Sector = Sector.Engineering
                }
            };
            
            var applicant = new Person()
            {
                Sector = Sector.Art
            };
            
            // Act
            var act = new Func<Application>(() => _service.Apply(offer, applicant));

            // Assert
            var exception = Assert.Throws<ApplicationSectorMismatchException>(act);
            Assert.NotNull(exception);
            Assert.Equal("Applicant sector does not match offer", exception.Message);
        }
    }
}