using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Interfaces;

namespace Mongeral.Provisao.V2.DAL
{
    public interface IEventosDaoFactory<TEvento> where TEvento : EventoOperacional
    {
        IEventosBase<TEvento> Criar();
    }
}
