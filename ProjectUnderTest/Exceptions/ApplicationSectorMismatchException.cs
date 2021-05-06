using System;

namespace ProjectUnderTest.Exceptions
{
    public class ApplicationSectorMismatchException : Exception
    {
        private new const string Message = "Applicant sector does not match offer";

        public ApplicationSectorMismatchException() : base(Message)
        {
        }
        
        public ApplicationSectorMismatchException(string message) : base(message)
        {
        }
    }
}