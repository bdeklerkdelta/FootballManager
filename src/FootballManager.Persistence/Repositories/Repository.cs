﻿using FootballManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FootballManager.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly FootballManagerDbContext FootballManagerContext;

        public Repository(FootballManagerDbContext footballManagerDbContext)
        {
            FootballManagerContext = footballManagerDbContext;
        }

        public IQueryable<TEntity> GetAll()
        {
            try
            {
                FootballManagerContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                return FootballManagerContext.Set<TEntity>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                await FootballManagerContext.AddAsync(entity);
                await FootballManagerContext.SaveChangesAsync(true,default);

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be saved: {ex.Message}");
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                FootballManagerContext.Update(entity);
                await FootballManagerContext.SaveChangesAsync(true, default);

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}");
            }
        }
    }
}
