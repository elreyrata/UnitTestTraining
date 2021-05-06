using System.Collections.Generic;
using ProjectUnderTest.Models;

namespace ProjectUnderTest.Repositories
{
    public interface IApplicationRepository
    {
        public IEnumerable<Application> GetPersonApplications(Person person);
    }
}