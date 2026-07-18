namespace ConferenceBooking.Api.Repository.Interfaces
{
    public interface IRepository<TEntity>
    where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(Guid id);

        Task<List<TEntity>> GetAllAsync();

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        Task SaveChangesAsync();
    }
}
