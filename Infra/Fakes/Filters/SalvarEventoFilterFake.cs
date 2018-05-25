using System;
using GreenPipes;
using Mongeral.Provisao.V2.Application.Context;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Interfaces;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Testes.Infra.Fake.Filters
{
    public class SalvarEventoFilterFake<TContexto, TEvento> : IFilter<TContexto>
         where TEvento : EventoOperacional
         where TContexto : ProvisaoContext
    {
        private readonly IEventosBase<TEvento> _eventos;

        public SalvarEventoFilterFake(IEventosBase<TEvento> eventos)
        {
            _eventos = eventos;
        }


        public Task Send(TContexto context, IPipe<TContexto> next)
        {
            throw new NotImplementedException();
        }

        public void Probe(ProbeContext context)
        {
        }
    }
}
