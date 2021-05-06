using System;

namespace ProjectUnderTest.Exceptions
{
    public class ApplicationOutOfDeadlineException : Exception
    {
        private new const string Message = "Cannot apply out of deadline";

        public ApplicationOutOfDeadlineException() : base(Message)
        {
        }
        
        public ApplicationOutOfDeadlineException(string message) : base(message)
        {
        }
    }
}