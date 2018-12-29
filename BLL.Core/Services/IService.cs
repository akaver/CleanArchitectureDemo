using DAL.Core.Repositories;
using Domain.Core;

namespace BLL.Core.Services
{

    public interface IService
    {
        
    }

    public interface IService<TEntity> : IRepository<TEntity> where TEntity: BaseEntity
    {
        
    }
    
    
}