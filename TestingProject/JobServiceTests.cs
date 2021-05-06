using Moq;
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
                Sector = Sector.Engineering
            };
            
            // Act
            var response = _service.Apply(offer, applicant);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(applicant, response.Applicant);
            Assert.Equal(offer, response.Offer);
        }
    }
}