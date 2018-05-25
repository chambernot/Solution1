using System;
using System.Threading.Tasks;
using Mongeral.Provisao.V2.Domain;

namespace Mongeral.Provisao.V2.Application.Interfaces
{
    public interface IProcessaEvento<TEvento> where TEvento : EventoOperacional
    {
        Task<TEvento> Processar(TEvento evento);
        Task Compensar(Guid identificador);
    }
}
