using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICoberturaContratadaRepository : IRepository<CoberturaContratada>
    {
        Task<IEnumerable<CoberturaContratada>> ObterPorMatricula(Int64 matricula);
    }
}