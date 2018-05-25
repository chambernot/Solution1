using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<TEntity> 
        where TEntity: class
    {
        Task<TEntity> Obter(int id);
        
        Task<IEnumerable<TEntity>> ObterTodos();
    }
}
