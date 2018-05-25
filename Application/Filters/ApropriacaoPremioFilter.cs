using Automatonymous;
using System.Threading.Tasks;
using GreenPipes;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Application.Context;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.Domain.StateMachine;
using Mongeral.Provisao.V2.Domain.Entidades;
using Mongeral.Infrastructure.Assertions;

namespace Mongeral.Provisao.V2.Application.Filters
{
    public class ApropriacaoPremioFilter: IFilter<ApropriacaoPremioContext>
    {
        private readonly ICoberturas _coberturas;
        private readonly IPremios _premios;
        private readonly CoberturaStateMachine _stateMachine;        

        public ApropriacaoPremioFilter(ICoberturas coberturas, IPremios premios, CoberturaStateMachine stateMachine)
        {
            _coberturas = coberturas;
            _premios = premios;
            _stateMachine = stateMachine;
        }
        
        public async Task Send(ApropriacaoPremioContext context, IPipe<ApropriacaoPremioContext> next)
        {
            foreach (var premio in context.Premios)
            {
                if (!await _premios.VerificarUltimoPremio(premio.ItemCertificadoApoliceId, context.Evento.MovimentosPermitidos, premio.Numero))
                    Assertion.Fail($"A apropriação não pode ser realizado, para o ItemCertificadoApoliceId {premio.ItemCertificadoApoliceId}, com o Número da Parcela { premio.Numero }." +
                                   $"O movimento anterior deve estar entre uma das opções permitidas: Emissão, Reemissão ou Desapropriação.");

                var instance = (await _coberturas.ObterPorItemCertificado(premio.ItemCertificadoApoliceId));                

                await _stateMachine.RaiseEvent(instance, _stateMachine.ApropriarPremio, new NotificacaoEvento<EventoApropriacaoPremio, PremioApropriado>(context.Evento, premio));
            }
            await next.Send(context);
        }

        public void Probe(ProbeContext context)
        {
            context.CreateScope("ApropriacaoPremio");
        }
    }
}
