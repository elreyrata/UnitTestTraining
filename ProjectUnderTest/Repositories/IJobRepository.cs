using System.Collections.Generic;
using ProjectUnderTest.Models;

namespace ProjectUnderTest.Repositories
{
    public interface IJobRepository
    {
        public IEnumerable<JobOffer> GetOffers(Person person);
    }
}