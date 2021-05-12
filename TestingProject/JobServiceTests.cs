using System;
using System.Collections.Generic;
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
        
        private readonly Mock<IJobRepository> _jobRepository;
        private readonly Mock<IApplicationRepository> _applicationRepository;

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
        
        [Fact]
        public void Apply_Test_Throws_AlreadyAppliedOfferException()
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

            _applicationRepository.Setup(x => x.GetPersonApplications(applicant))
                .Returns(new List<Application>()
                {
                    new Application()
                    {
                        Applicant = applicant,
                        Offer = offer
                    }
                });
            
            // Act
            var act = new Func<Application>(() => _service.Apply(offer, applicant));

            // Assert
            var exception = Assert.Throws<AlreadyAppliedOfferException>(act);
            Assert.NotNull(exception);
            Assert.Equal("Cannot applied to an already applied offer", exception.Message);
        }

        [Fact]
        public void Hire_WithoutPastJob_Test_ExpectedBehaviour()
        {
            // Arrange
            var offer = new JobOffer()
            {
                Applicable = true,
                TargetJob = new Job()
                {
                    Position = "DummyPosition"
                }
            };
            
            var person = new Person()
            {
                ActualJob = null
            };

            // Act
            var result = _service.Hire(offer, person);

            // Assert
            Assert.NotNull(result);
            Assert.False(offer.Applicable);
            Assert.NotNull(person.ActualJob);
            Assert.Equal(offer.TargetJob, person.ActualJob);
        }
        
        [Fact]
        public void Hire_WithPastJob_Test_ExpectedBehaviour()
        {
            // Arrange
            var offer = new JobOffer()
            {
                Applicable = true,
                TargetJob = new Job()
                {
                    Position = "DummyPosition"
                }
            };
            
            var toBePastJob = new Job()
            {
                Position = "DummyJob"
            };
            
            var person = new Person()
            {
                ActualJob = toBePastJob
            };

            // Act
            var result = _service.Hire(offer, person);

            // Assert
            Assert.NotNull(result);
            Assert.False(offer.Applicable);
            Assert.NotNull(person.ActualJob);
            Assert.Equal(offer.TargetJob, person.ActualJob);
            Assert.NotEmpty(person.PastJobs);
            Assert.Contains(toBePastJob, person.PastJobs);
        }
    }
}