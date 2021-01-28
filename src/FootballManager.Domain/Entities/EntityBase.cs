using System;
using System.ComponentModel.DataAnnotations;
using FootballManager.Domain.ValueObjects;
using static FootballManager.Domain.Enumerations.EnumBag;

namespace FootballManager.Domain.Entities
{
    public abstract class EntityBase
    {
        protected EntityBase() 
        {
            CreatedDate = DateTimeOffset.Now;
            ModifiedDate = DateTimeOffset.Now;
            DataState = DataState.Active;
        }

        public EntityBase(CreatedDate createdDate, 
                ModifiedDate modifiedDate, 
                DataState dataState)
        { 
            _ = createdDate ?? throw new ArgumentException("Cannot be null.", "createdDate");
            _ = modifiedDate ?? throw new ArgumentException("Cannot be null.", "modifiedDate");

            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
            DataState = dataState;
        }

        [Key]
        public long Id { get;  } 

        public virtual CreatedDate CreatedDate { get;  }

        public DataState DataState { get; set; }

        public virtual ModifiedDate ModifiedDate { get; set; }
    }
}