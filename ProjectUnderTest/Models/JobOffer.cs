using System;

namespace ProjectUnderTest.Models
{
    public class JobOffer
    {
        public bool Applicable { get; set; } = true;
        public DateTime? DeadLine { get; set; }
        public Job TargetJob { get; set; }
    }
}