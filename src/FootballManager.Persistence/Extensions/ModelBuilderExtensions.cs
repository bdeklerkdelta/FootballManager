using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace FootballManager.Persistence.Extensions
{
    public static class ModelBuilderExtensions
    {
        static readonly MethodInfo SetQueryFilterMethod = typeof(ModelBuilderExtensions)
            .GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
            .Single(t => t.IsGenericMethod && t.Name == nameof(SetQueryFilter));


        static void SetEntityQueryFilter<TEntityInterface>(
           this ModelBuilder builder,
           Type entityType,
           Expression<Func<TEntityInterface, bool>> filterExpression)
        {
            SetQueryFilterMethod.MakeGenericMethod(entityType, typeof(TEntityInterface))
              .Invoke(null, new object[] { builder, filterExpression });
        }

        static void SetQueryFilter<TEntity, TEntityInterface>(
            this ModelBuilder builder,
            Expression<Func<TEntityInterface, bool>> filterExpression)
              where TEntityInterface : class
              where TEntity : class, TEntityInterface
        {
            var concreteExpression = filterExpression.Convert<TEntityInterface, TEntity>();
            builder.Entity<TEntity>().HasQueryFilter(concreteExpression);
        }

        public static void SetQueryFilterOnAllEntities<TEntityInterface>(
            this ModelBuilder builder,
            Expression<Func<TEntityInterface, bool>> filterExpression)
        {
            foreach (var type in builder.Model.GetEntityTypes()
              .Where(t => t.BaseType == null)
              .Select(t => t.ClrType)
              .Where(t => typeof(TEntityInterface).IsAssignableFrom(t)))
            {
                builder.SetEntityQueryFilter<TEntityInterface>(
                  type,
                  filterExpression);
            }
        }
    }
}
