using Domain.Entities;
using Domain.Enum;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IEventoImplantadoRepository: IRepository<EventoImplantado>
    {
        Task<EventoImplantado> Obter(long id, FiltroEventoEnum filtro);
    }
}
