using Domain.Entities;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class HistoricoController : ApiController
    {
        readonly IHistoricoCoberturaRepository _repository;

        public HistoricoController(IHistoricoCoberturaRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<HistoricoCobertura> Obter(int id)
        {
            return await _repository.Obter(id);
        }

        [HttpGet]
        public async Task<IEnumerable<HistoricoCobertura>> ObterTodos()
        {
            return await _repository.ObterTodos();
        }
    }
}
