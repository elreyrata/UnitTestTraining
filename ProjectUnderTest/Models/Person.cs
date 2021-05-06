using System.Collections.Generic;

namespace ProjectUnderTest.Models
{
    public class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Age { get; set; }
        public Sector Sector { get; set; }
        public Job ActualJob { get; set; }
        public IEnumerable<Job> PastJobs { get; set; } = new List<Job>();
    }
}