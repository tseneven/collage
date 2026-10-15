using Microsoft.EntityFrameworkCore;
using trpo6.Models;

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
            Commit();
            return outputEntity.Entity;
        }
        catch
        {
            return null;
        }
    }
    
    public T? PutEntity<T>(T entity) where T : class {
        try
        {
            var entry = dbContext.Entry(entity);

            if (entry.State == EntityState.Detached)
                dbContext.Set<T>().Update(entity);

            Commit();

            return entity;
        }
        catch
        {
            return null;
        }
    }

    public void Patch() => Commit();
    
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
    private void Commit() => dbContext.SaveChanges();
}