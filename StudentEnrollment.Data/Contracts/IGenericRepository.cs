namespace StudentEnrollment.Data.Contracts;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity> GetAsync(int? id);
    Task<TEntity> AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task<bool> DeleteAsync(int id);
    Task<bool> Exists(int id);
}
