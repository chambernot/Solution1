using Mongeral.Provisao.V2.Domain;
using System;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Application.Interfaces
{
    public interface IExecucaoEvento
    {
        Task Executar(EventoOperacional eventoOperacional);
        Task Compensate(Guid identificador);
        Task<bool> ExisteEvento(Guid identificador);
    }
}
