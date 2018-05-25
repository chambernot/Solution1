using System.Threading.Tasks;
using Automatonymous;
using GreenPipes;
using Mongeral.Provisao.V2.Application.Context;
using Mongeral.Provisao.V2.Domain.Entidades.EventosOperacional;
using Mongeral.Provisao.V2.Domain.Entidades.Premios;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.Domain.StateMachine;

namespace Mongeral.Provisao.V2.Application.Filters
{
    public class AportePremioFilter: IFilter<AportePremioContext>
    {
        private readonly ICoberturas _coberturas;
        private readonly CoberturaStateMachine _stateMachine;

        public AportePremioFilter(ICoberturas coberturas, CoberturaStateMachine stateMachine)
        {
            _coberturas = coberturas;
            _stateMachine = stateMachine;
        }

        public async Task Send(AportePremioContext context, IPipe<AportePremioContext> next)
        {
            foreach (var premio in context.Premios)
            {
                var instance = await _coberturas.ObterPorItemCertificado(premio.ItemCertificadoApoliceId);

                await _stateMachine.RaiseEvent(instance, _stateMachine.AportePremio, new NotificacaoEvento<EventoAportePremio, PremioAporte>(context.Evento, premio));
            }

            await next.Send(context);
        }

        public void Probe(ProbeContext context)
        {
            context.CreateScope("AportePremio");
        }
    }
}
