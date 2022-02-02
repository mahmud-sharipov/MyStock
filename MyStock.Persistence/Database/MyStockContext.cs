namespace MyStock.Persistence.Database;

public class MyStockContext : DbContext, IContext
{
    public MyStockContext() : this(new DbContextOptions<MyStockContext>()) { }

    public MyStockContext(DbContextOptions<MyStockContext> dbContextOptions) : base(dbContextOptions)
    {
        Database.Migrate();
    }

    public Guid Id { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<EntityBase>();
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = "data source=localhost; initial catalog=MyStock; integrated security=true; persist security info=true;";
        optionsBuilder.UseLazyLoadingProxies();
        optionsBuilder.UseSqlServer(connectionString);
    }

    public new IQueryable<T> Set<T>() where T : class, IEntity =>
         base.Set<T>();

    public T Get<T>(Guid guid) where T : class, IEntity =>
        Find<T>(guid);

    public void AddToContext<T>(T entity) where T : IEntity =>
        Entry(entity as EntityBase).State = EntityState.Added;

    public void Update<T>(Guid id, T entity) where T : class, IEntity
    {
        var oldEntity = Find<T>(id);
        Entry(oldEntity).CurrentValues.SetValues(entity);
    }
    
    public void MarkDelete<T>(T entity) where T : IEntity
    {
        var entityBase = entity as EntityBase;
        if (ChangeTracker.Entries().FirstOrDefault(e =>
        {
            var serverEntityBase = e.Entity as EntityBase;
            return serverEntityBase != null && serverEntityBase.Guid == entityBase.Guid;
        })?.State == EntityState.Deleted)
        {
            throw new Exception("BaseStatus of serverEntity already is deleted");
        }
        Entry(entityBase).State = EntityState.Deleted;
    }

    public void Delete<T>(T entity) where T : class, IEntity =>
        Remove(entity);

    public bool HasChange() =>
        ChangeTracker.HasChanges();

    public void UndoChanges()
    {
        var changedEntries = ChangeTracker.Entries()
            .Where(x => x.State != EntityState.Unchanged).ToList();

        foreach (var entry in changedEntries)
        {
            switch (entry.State)
            {
                case EntityState.Modified:
                    entry.CurrentValues.SetValues(entry.OriginalValues);
                    entry.State = EntityState.Unchanged;
                    break;
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Unchanged;
                    break;
            }
        }
    }
}
