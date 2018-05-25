using System.Threading.Tasks;
using Automatonymous;
using GreenPipes;
using Mongeral.Provisao.V2.Application.Context;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.Domain.StateMachine;

namespace Mongeral.Provisao.V2.Application.Filters
{
    public class InclusaoVgFilter: IFilter<InclusaoVgContext>
    {
        private readonly ICoberturas _coberturas;
        private readonly CoberturaStateMachine _stateMachine;

        public InclusaoVgFilter(ICoberturas coberturas, CoberturaStateMachine stateMachine)
        {
            _coberturas = coberturas;
            _stateMachine = stateMachine;
        }

        public async Task Send(InclusaoVgContext context, IPipe<InclusaoVgContext> next)
        {
            foreach (var coberturaContratada in context.Coberturas)
            {
                var instance = await _coberturas.ObterPorItemCertificado(coberturaContratada.ItemCertificadoApoliceId) ?? coberturaContratada;

                await _stateMachine.RaiseEvent(instance, _stateMachine.InclusaoVG, new NotificacaoEvento<EventoInclusaoVg, CoberturaContratada>(context.Evento, coberturaContratada));
            }

            await next.Send(context);
        }

        public void Probe(ProbeContext context)
        {
            context.CreateScope("InclusaoVG");
        }
    }
}
