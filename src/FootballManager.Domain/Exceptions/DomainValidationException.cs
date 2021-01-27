using System;

namespace FootballManager.Domain.Exceptions
{
    public class DomainValidationException : ArgumentException
    {
        public DomainValidationException(string message, string paramName) : base(message, paramName)
        {
        }
    }
}
