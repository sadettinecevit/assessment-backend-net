namespace Report.Application.Interfaces.Repositories
{
    public interface IRepository<T> where T : class, new()
    {
        Task<List<T>> GetAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(T entity);
        Task<int> SaveChanges();
    }
}
