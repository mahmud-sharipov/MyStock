namespace MyStock.Persistence.Database;

public class MyStockContextFactory : IDesignTimeDbContextFactory<MyStockContext>
{
    public MyStockContext CreateDbContext(string[] args) =>
        new MyStockContext(new DbContextOptionsBuilder<MyStockContext>().Options);
}
