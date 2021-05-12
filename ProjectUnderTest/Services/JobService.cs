using System;
using System.Collections.Generic;
using System.Linq;
using ProjectUnderTest.Exceptions;
using ProjectUnderTest.Models;
using ProjectUnderTest.Repositories;

namespace ProjectUnderTest.Services
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;
        private readonly IApplicationRepository _applicationRepository;
        
        public JobService(IJobRepository jobRepository, IApplicationRepository applicationRepository)
        {
            _jobRepository = jobRepository;
            _applicationRepository = applicationRepository;
        }

        public Application Apply(JobOffer offer, Person applicant)
        {
            if (offer.DeadLine.HasValue && offer.DeadLine.Value < DateTime.Now)
            {
                throw new ApplicationOutOfDeadlineException($"{offer.TargetJob.Position} cannot be applied out of deadline");
            }

            if (!offer.Applicable)
            {
                throw new ApplicationNotApplicableException();
            }

            if (applicant.Sector != offer.TargetJob.Sector)
            {
                throw new ApplicationSectorMismatchException();
            }
            
            // TODO: NEW FEATURE 
            // PERSON CANNOT APPLY IF ALREADY HAD APPLIED
            var applications = _applicationRepository.GetPersonApplications(applicant);
            var offers = applications.Select(x => x.Offer);
            if (offers.Contains(offer))
            {
                throw new AlreadyAppliedOfferException();
            }
            
            return new Application()
            {
                Applicant = applicant,
                Offer = offer
            };
        }

        public JobOffer CancelOffer(JobOffer offer)
        {
            offer.Applicable = false;
            return offer;
        }

        // TODO: TEST
        public Person Hire(JobOffer offer, Person person)
        {
            offer.Applicable = false;
            var lastJob = person.ActualJob;
            
            if (lastJob != null)
            {
                person.PastJobs = person.PastJobs.Append(lastJob);
            }

            person.ActualJob = offer.TargetJob;

            return person;
        }

        public IEnumerable<JobOffer> GetOffers(Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));                
            }
            
            return _jobRepository.GetOffers(person);
        }
    }
}