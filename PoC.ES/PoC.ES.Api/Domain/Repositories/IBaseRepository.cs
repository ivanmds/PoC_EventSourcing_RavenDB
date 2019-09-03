namespace PoC.ES.Api.Domain.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        TEntity Get(string id);
    }
}
