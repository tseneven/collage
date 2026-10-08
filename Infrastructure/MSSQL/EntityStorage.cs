using trpo6.Models;
using Task = System.Threading.Tasks.Task;

namespace trpo6.Infrastructure.MSSQL;

public class EntityStorage(WebApiDatabaseContext dbContext)
{
    public IQueryable<T> Select<T>() where T : class
    {
        return dbContext.Set<T>();
    }

    public T? CreateEntity<T>(T entity) where T : class
    {
        try
        {
            var outputEntity = dbContext.Set<T>().Add(entity);
            var c = Commit();

            return outputEntity.Entity;
        }
        catch
        {
            return null;
        }
    }
    
    public T? UpdateEntity<T>(T entity) where T : class
    {
        try
        {
            dbContext.Set<T>().Update(entity);
            Commit();
            return entity;
        }
        catch
        {
            return null;
        }
    }
    
    public T? PatchEntity<T>(T existingEntity, T patchedEntity) where T : class
    {
        try
        {
            Commit();
            return existingEntity;
        }
        catch
        {
            return null;
        }
    }
    
    public T? DeleteEntity<T>(T entity) where T : class
    {
        try
        {
            dbContext.Set<T>().Remove(entity);
            Commit();
            return entity;
        }
        catch
        {
            return null;
        }
    }
    private int Commit() => dbContext.SaveChanges();
}