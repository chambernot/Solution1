using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class EventoController : ApiController
    {
        readonly IEventoImplantadoRepository _repository;
        public EventoController(IEventoImplantadoRepository repository)
        {
            _repository = repository;
        }
        
        [HttpGet]
        [ActionName("ObterPorId")]
        public async Task<EventoImplantado> ObterPorId(long id)
        {
            return await _repository.Obter(id, FiltroEventoEnum.PorEventoImplantadoId);
        }

        [HttpGet]
        [ActionName("ObterPorItemCertificadoApoliceId")]
        public async Task<EventoImplantado> ObterPorItemCertificadoApoliceId(long id)
        {
            return await _repository.Obter(id, FiltroEventoEnum.PorItemCertificadoApoliceId);
        }

        [HttpGet]
        [ActionName("ObterPorInscricaoId")]
        public async Task<EventoImplantado> ObterPorInscricaoId(long id)
        {
            return await _repository.Obter(id, FiltroEventoEnum.PorInscricaoId);
        }

        [HttpGet]
        public async Task<IEnumerable<EventoImplantado>> ObterTodos()
        {
            return await _repository.ObterTodos();
        }
    }
}
