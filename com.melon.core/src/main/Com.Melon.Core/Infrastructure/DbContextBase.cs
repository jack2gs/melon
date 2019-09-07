using Microsoft.EntityFrameworkCore;

namespace Com.Melon.Core.Infrastructure
{
    public class DbContextBase: DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        protected void OnModelCreating<T>(ModelBuilder modelBuilder)
        {

        }
    }
}
