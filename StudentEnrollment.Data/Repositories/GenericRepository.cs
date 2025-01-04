using Microsoft.EntityFrameworkCore;
using StudentEnrollment.Data.Contracts;

namespace StudentEnrollment.Data.Repositories;

public class GenericRepository<TEntity>(StudentEnrollmentDbContext context) : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly StudentEnrollmentDbContext context = context;

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await context.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity> GetAsync(int? id)
    {
        return await context.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await context.AddAsync(entity);
        await context.SaveChangesAsync();

        return entity;
    }

    public async Task UpdateAsync(TEntity entity)
    {
        context.Update(entity);
        await context.SaveChangesAsync();
    }
    public async Task<bool> DeleteAsync(int id)
    {
        TEntity entity = await GetAsync(id);

        context.Set<TEntity>().Remove(entity);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Exists(int id)
    {
        return await context.Set<TEntity>().AnyAsync(entity => entity.Id == id);
    }
}