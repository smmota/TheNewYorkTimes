using Microsoft.EntityFrameworkCore;
using TheNewYorkTimes.Data;

namespace TheNewYorkTimes.Tests.Helpers
{
    public class MockUsuarioDb : IDbContextFactory<UsuarioDbContext>
    {
        public UsuarioDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<UsuarioDbContext>()
                .UseInMemoryDatabase($"InMemoryTestDb-{DateTime.Now.ToFileTimeUtc()}")
                .Options;

            return new UsuarioDbContext(options);
        }
    }
}
