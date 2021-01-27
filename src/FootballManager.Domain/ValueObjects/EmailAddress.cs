using System.Collections.Generic;
using FootballManager.Domain.Exceptions;
using FootballManager.Domain.Infrastructure;

namespace FootballManager.Domain.ValueObjects
{
    public sealed class EmailAddress : ValueObject
    {
        private readonly string _value;

        private EmailAddress() { }

        public EmailAddress(string emailAddress)
        {
            if (emailAddress == null)
                throw new DomainValidationException("Invalid value. Cannot be null.", "EmailAddress");

            if (!emailAddress.Contains('@'))
                throw new DomainValidationException("Invalid value.", "EmailAddress");

            if (!emailAddress.Contains('.'))
                throw new DomainValidationException("Invalid value.", "EmailAddress");

            this._value = emailAddress;
        }

        public static implicit operator EmailAddress(string value) => new EmailAddress(value);
        public static implicit operator string(EmailAddress emailAddress) => emailAddress.Value;

        public string Value
        {
            get { return _value; }
        }
          
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _value.ToLowerInvariant();
        }
    }
}
