using System.Threading.Tasks;
using GreenPipes;
using Mongeral.Provisao.V2.Application.Context;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.Domain.StateMachine;

namespace Mongeral.Provisao.V2.Application.Filters
{
    public class PremioFilters: IFilter<ProvisaoContext>
    {
        private readonly ICoberturas _coberturas;
        private readonly CoberturaStateMachine _stateMachine;

        public PremioFilters(ICoberturas coberturas, CoberturaStateMachine stateMachine)
        {
            _coberturas = coberturas;
            _stateMachine = stateMachine;            
        }
        
        public async Task Send(ProvisaoContext context, IPipe<ProvisaoContext> next)
        {
            //foreach (var premio in context.Premios)
            //{
            //    var instance = (await _coberturas.ObterPorItemCertificado(premio.ItemCertificadoApoliceId));
                //var stateMachine = _stateMachines[instance.RegimeFinanceiroId]

                //await _stateMachine.RaiseEvent(instance, _stateMachine.EmitirPremio, new NotificacaoEvento<EventoEmissaoPremio, Premio>(context.Evento as EventoEmissaoPremio, premio));
            //}

            await next.Send(context);
        }

        public void Probe(ProbeContext context)
        {
            context.CreateScope("Provisao");
        }
    }
}
