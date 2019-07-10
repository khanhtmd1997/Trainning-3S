using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using TaskTranning.Models;

namespace TestsXUnit
{
    public static class TestHelpers
    {
        internal static Mock<DbSet<T>> GetMockDbSet<T>(ICollection<T> entities) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(entities.AsQueryable().Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(entities.AsQueryable().Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(entities.AsQueryable().ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(entities.AsQueryable().GetEnumerator());
            mockSet.Setup(m => m.Add(It.IsAny<T>())).Callback<T>(entities.Add);
            return mockSet;
        }

        /// <summary>
        /// Get app db context
        /// </summary>
        /// <returns></returns>
        public static CodeFirstDataContext GetDataContext()
        {
            var options = new DbContextOptionsBuilder<CodeFirstDataContext>().UseInMemoryDatabase(Guid.NewGuid().ToString())
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .EnableSensitiveDataLogging()
                .Options;
            return new CodeFirstDataContext(options);
        }

        /// <summary>
        /// Get app db context
        /// </summary>
        /// <returns></returns>
        public static DbContextOptionsBuilder GetDbContextOptionsBuilderOptions()
        {
            var builder = new DbContextOptionsBuilder<CodeFirstDataContext>().UseInMemoryDatabase(Guid.NewGuid().ToString())
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .EnableSensitiveDataLogging();
            return builder;
        }
    }
}
