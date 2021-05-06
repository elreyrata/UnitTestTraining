using System;

namespace ProjectUnderTest.Models
{
    public class Job
    {
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Position { get; set; }
        public Sector Sector { get; set; }
    }
}