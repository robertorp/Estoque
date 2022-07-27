namespace Estoque.Core.Data;

public interface IUnitOfWork
{
    public Task<bool> SaveEntitiesAsync();
}