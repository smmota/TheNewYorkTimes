using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewYorkTimes.Data;

namespace TheNewYorkTimes.Tests.Helpers
{
    public class MockNoticiaDb : IDbContextFactory<NoticiaDbContext>
    {
        public NoticiaDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<NoticiaDbContext>()
                .UseInMemoryDatabase($"InMemoryTestDb-{DateTime.Now.ToFileTimeUtc()}")
                .Options;

            return new NoticiaDbContext( options );
        }
    }
}
