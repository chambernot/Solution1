using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreenPipes;
using Mongeral.Provisao.V2.Application.Context;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Interfaces;

namespace Mongeral.Provisao.V2.Application.Filters
{
    public class SalvarEventoFilter<TContexto,TEvento> : IFilter<TContexto> 
        where TEvento : EventoOperacional
        where TContexto : ProvisaoContext
    {
        private readonly IEventosBase<TEvento> _eventos;

        public SalvarEventoFilter(IEventosBase<TEvento> eventos)
        {
            _eventos = eventos;
        }

        public async Task Send(TContexto context, IPipe<TContexto> next)
        {
            await _eventos.Adicionar(context.Evento as TEvento);
            await next.Send(context);
        }

        public void Probe(ProbeContext context)
        {
        }
    }
}