using System;
using System.Collections.Generic;
using FootballManager.Domain.Exceptions;
using FootballManager.Domain.Infrastructure;

namespace FootballManager.Domain.ValueObjects
{
    public sealed class CreatedDate : ValueObject
    {
        private readonly DateTimeOffset _value;


        private CreatedDate() { 
        }

        public CreatedDate(DateTimeOffset? createdDate = null)
        {
            if (createdDate != null && createdDate > DateTimeOffset.UtcNow)
                throw new DomainValidationException("Invalid value. Date cannot be set in the future", "CreatedDate");

            if (createdDate != null && createdDate <= DateTimeOffset.MinValue)
                throw new DomainValidationException("Invalid value.", "CreatedDate");

            if (createdDate != null && createdDate >= DateTimeOffset.MaxValue)
                throw new DomainValidationException("Invalid value.", "CreatedDate");

            this._value = createdDate ?? DateTimeOffset.UtcNow;
        }

        public static implicit operator CreatedDate(DateTimeOffset? value) => new CreatedDate(value);

        public DateTimeOffset Value
        {
            get { return _value; }
        }

        public static bool operator <(CreatedDate left, CreatedDate right)
        {
            return left.Value < right.Value;
        }

        public static bool operator >(CreatedDate left, CreatedDate right)
        {
            return left.Value > right.Value;
        }

        public static bool operator <=(CreatedDate left, CreatedDate right)
        {
            return (left < right || left == right);
        }

        public static bool operator >=(CreatedDate left, CreatedDate right)
        {
            return (left > right || left == right);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _value;
        }
    }
}
