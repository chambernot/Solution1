using System;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Domain.Interfaces
{
    public interface IEventosBase<TEvento>
    {        
        Task<bool> ExisteEvento(Guid identificador);
        Task Compensate(Guid identificador);
        Task Salvar(TEvento evento);
    }
}

