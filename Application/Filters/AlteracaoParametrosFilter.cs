using System;
using System.Threading.Tasks;
using GreenPipes;
using Mongeral.Provisao.V2.Application.Context;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.Domain.StateMachine;
using Automatonymous;
using Mongeral.Provisao.V2.Domain.Dominios;

namespace Mongeral.Provisao.V2.Application.Filters
{
    public class AlteracaoCoberturasFilter : IFilter<AlteracaoContext>
    {
        private readonly ICoberturas _coberturas;
        private CoberturaStateMachine _stateMachine;

        public AlteracaoCoberturasFilter(ICoberturas coberturas, CoberturaStateMachine stateMachine)
        {
            _coberturas = coberturas;
            _stateMachine = stateMachine;
        }

        public async Task Send(AlteracaoContext context, IPipe<AlteracaoContext> next)
        {
            foreach (var historico in context.Historicos)
            {
                historico.InformaStatus(StatusCobertura.StatusCoberturaEnum.Activa, DateTime.Now);

                var instance = (await _coberturas.ObterPorItemCertificado(historico.Cobertura.ItemCertificadoApoliceId)) ?? historico.Cobertura;                
                await _stateMachine.RaiseEvent(instance, _stateMachine.Alterar, new NotificacaoEvento<EventoAlteracao, HistoricoCoberturaContratada>(context.Evento, historico));                
            }

            await next.Send(context);
        }

        public void Probe(ProbeContext context)
        {
            context.CreateScope("AlteracaoCoberturas");
        }
    }
}

