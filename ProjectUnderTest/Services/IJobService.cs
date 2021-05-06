using System.Collections.Generic;
using ProjectUnderTest.Models;

namespace ProjectUnderTest.Services
{
    public interface IJobService
    {
        public Application Apply(JobOffer offer, Person applicant);
        public JobOffer CancelOffer(JobOffer offer);
        public Person Hire(JobOffer offer, Person person);
        public IEnumerable<JobOffer> GetOffers(Person person);
    }
}