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
    public class CompensarEventoFilterSpecification<TContexto,TEventoOperacional> : IPipeSpecification<TContexto> 
        where TContexto : ProvisaoContext
        where TEventoOperacional : EventoOperacional
    {
        private readonly Func<IEventosBase<TEventoOperacional>> _eventosFactory;

        public CompensarEventoFilterSpecification(Func<IEventosBase<TEventoOperacional>> eventosFactory)
        {
            _eventosFactory = eventosFactory;
        }

        public void Apply(IPipeBuilder<TContexto> builder)
        {
            builder.AddFilter(new CompensarEventoFilter<TContexto,TEventoOperacional>(_eventosFactory));
        }

        public IEnumerable<ValidationResult> Validate()
        {
            return Enumerable.Empty<ValidationResult>();
        }
    }

    public class CompensarEventoFilter<TContexto,TEventoOperacional> : IFilter<TContexto> 
        where TContexto : ProvisaoContext
        where TEventoOperacional : EventoOperacional
    {
        private readonly IEventosBase<TEventoOperacional> _eventos;

        public CompensarEventoFilter(Func<IEventosBase<TEventoOperacional>> eventosFactory)
        {
            _eventos = eventosFactory.Invoke();
        }

        public async Task Send(TContexto context, IPipe<TContexto> next)
        {
            await _eventos.Remover(context.Request.Identificador);
            await next.Send(context);
        }

        public void Probe(ProbeContext context)
        {
        }
    }

}
