using System;

namespace ProjectUnderTest.Exceptions
{
    public class AlreadyAppliedOfferException : Exception
    {
        private new const string Message = "Cannot applied to an already applied offer";

        public AlreadyAppliedOfferException() : base(Message)
        {
        }

        public AlreadyAppliedOfferException(string message) : base(message)
        {
        }
    }
}