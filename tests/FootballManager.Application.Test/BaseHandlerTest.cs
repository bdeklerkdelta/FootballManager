using AutoMapper;
using FootballManager.Application.Infrastructure.AutoMapper;
using FootballManager.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace FootballManager.Application.Test
{
    public class BaseHandlerTest
    {
        protected virtual FootballManagerDbContext GetDbContext(string dbName) =>
            new FootballManagerDbContext(
                new DbContextOptionsBuilder<FootballManagerDbContext>().UseInMemoryDatabase(dbName).Options);

        protected virtual IMapper GetMapper() => new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>()));
    }
}
