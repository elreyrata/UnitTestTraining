using System;

namespace ProjectUnderTest.Exceptions
{
    public class ApplicationNotApplicableException : Exception
    {
        private new const string Message = "Cannot apply when not applicable";

        public ApplicationNotApplicableException() : base(Message)
        {
        }

        public ApplicationNotApplicableException(string message) : base(message)
        {
        }
    }
}