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
    public class EmissaoPremioFilter : IFilter<EmissaoPremioContext>
    {
        private readonly ICoberturas _coberturas;
        private readonly IPremios _premios;
        private readonly CoberturaStateMachine _stateMachine;

        public EmissaoPremioFilter(ICoberturas coberturas, IPremios premios, CoberturaStateMachine stateMachine)
        {
            _coberturas = coberturas;
            _premios = premios;
            _stateMachine = stateMachine;
        }

        public async Task Send(EmissaoPremioContext context, IPipe<EmissaoPremioContext> next)
        {
            foreach (var premio in context.Premios)
            {
                var existeEmissao = await _premios.Contem(premio.ItemCertificadoApoliceId);

                if (existeEmissao)
                    if (!await _premios.VerificarUltimoPremio(premio.ItemCertificadoApoliceId,
                        context.Evento.MovimentosPermitidos, premio.Numero))
                        Assertion.Fail($"Já existe Emissão do Prêmio com o ItemCertificadoApoliceId: {premio.ItemCertificadoApoliceId} e com o Número da Parcela: {premio.Numero}. " +
                                $"O movimento anterior deve estar entre uma das opções permitidas: Cancelamento Emissão, Ajuste de Premio ou Cancelamento Ajuste.")
                            .Validate();

                var instance = await _coberturas.ObterPorItemCertificado(premio.ItemCertificadoApoliceId);

                await _stateMachine.RaiseEvent(instance, _stateMachine.EmitirPremio, new NotificacaoEvento<EventoEmissaoPremio, Premio>(context.Evento as EventoEmissaoPremio, premio));
            }
            await next.Send(context);
        }

        public void Probe(ProbeContext context)
        {
            context.CreateScope("EmissaoPremio");
        }
    }
}
