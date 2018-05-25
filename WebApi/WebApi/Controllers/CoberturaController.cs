using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class CoberturaController : ApiController
    {
        readonly ICoberturaContratadaRepository _repository;
        
        public CoberturaController(ICoberturaContratadaRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<CoberturaContratada> Obter(int id)
        {
            return await _repository.Obter(id);
        }

        [HttpGet]
        public async Task<IEnumerable<CoberturaContratada>> ObterTodos()
        {
            return await _repository.ObterTodos();
        }

        [HttpGet]
        public async Task<IEnumerable<CoberturaContratada>> ObterPorMatricula(Int64 matricula)
        {
            return await _repository.ObterPorMatricula(matricula);
        }
    }
}
