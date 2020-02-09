using Source.DataServices.EFCore.DataContext;

namespace Source.EFCore.Setup
{
    public static class TestDbContextFactory
    {
        public static AppDbContext CreateDbContext()
        {
            return new InMemoryDbContext(true);
        }
    }
}