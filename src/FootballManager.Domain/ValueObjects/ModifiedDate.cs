using System;
using System.Collections.Generic;
using FootballManager.Domain.Exceptions;
using FootballManager.Domain.Infrastructure;

namespace FootballManager.Domain.ValueObjects
{
    public sealed class ModifiedDate : ValueObject
    {
        private readonly DateTimeOffset? _value;

        private ModifiedDate() { }

        public ModifiedDate(DateTimeOffset? modifiedDate = null)
        {
            if (modifiedDate != null && modifiedDate >= DateTimeOffset.UtcNow)
                throw new DomainValidationException("Invalid value. Modified date cannot be in the future.", "ModifiedDate");

            if (modifiedDate != null && modifiedDate <= DateTimeOffset.MinValue)
                throw new DomainValidationException("Invalid value.", "ModifiedDate");

            if (modifiedDate != null && modifiedDate > DateTimeOffset.MaxValue)
                throw new DomainValidationException("Invalid value.", "ModifiedDate");

            this._value = modifiedDate;
        }

        public static implicit operator ModifiedDate(DateTimeOffset? value) => new ModifiedDate(value);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _value;
        }

        public DateTimeOffset? Value
        {
            get { return _value; }
        }

        public static bool operator <(ModifiedDate left, ModifiedDate right)
        {
            return left.Value < right.Value;
        }

        public static bool operator >(ModifiedDate left, ModifiedDate right)
        {
            return left.Value > right.Value;
        }

        public static bool operator <=(ModifiedDate left, ModifiedDate right)
        {
            return (left < right || left == right);
        }

        public static bool operator >=(ModifiedDate left, ModifiedDate right)
        {
            return (left > right || left == right);
        }


    }
}
