using Automatonymous;
using GreenPipes;
using Mongeral.Infrastructure.Assertions;
using Mongeral.Provisao.V2.Application.Context;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.Domain.StateMachine;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Application.Filters
{
    public class AjustePremioFilter: IFilter<AjustePremioContext>
    {
        private readonly ICoberturas _coberturas;
        private readonly IPremios _premios;
        private readonly CoberturaStateMachine _stateMachine;

        public AjustePremioFilter(ICoberturas coberturas, IPremios premios, CoberturaStateMachine stateMachine)
        {
            _coberturas = coberturas;
            _stateMachine = stateMachine;
            _premios = premios;
        }

        public async Task Send(AjustePremioContext context, IPipe<AjustePremioContext> next)
        {
            foreach (var premio in context.Premios)
            {
                if (!await _premios.VerificarUltimoPremio(premio.ItemCertificadoApoliceId, context.Evento.MovimentosPermitidos, premio.Numero))
                    Assertion.Fail($"O Ajuste não pode ser realizado. Não foi encontrada nenhum prêmio com o ItemCertificadoApoliceId: {premio.ItemCertificadoApoliceId} e com o Número da Parcela: { premio.Numero }.").Validate();

                var instance = await _coberturas.ObterPorItemCertificado(premio.ItemCertificadoApoliceId);

                await _stateMachine.RaiseEvent(instance, _stateMachine.AjustePremio, new NotificacaoEvento<EventoAjustePremio, Premio>(context.Evento as EventoAjustePremio, premio));
            }
            await next.Send(context);
        }
        
        public void Probe(ProbeContext context)
        {
            context.CreateScope("AjustePremio");
        }
    }
}
