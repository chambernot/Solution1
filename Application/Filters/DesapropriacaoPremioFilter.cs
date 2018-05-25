using Automatonymous;
using GreenPipes;
using Mongeral.Infrastructure.Assertions;
using Mongeral.Provisao.V2.Application.Context;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Entidades;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.Domain.StateMachine;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Application.Filters
{
    public class DesapropriacaoPremioFilter : IFilter<DesapropriacaoPremioContext>    
    {
        private readonly ICoberturas _coberturas;
        private readonly IPremios _premios;
        private readonly CoberturaStateMachine _stateMachine;

        public DesapropriacaoPremioFilter(ICoberturas coberturas, IPremios premios, CoberturaStateMachine stateMachine)
        {
            _coberturas = coberturas;
            _premios = premios;
            _stateMachine = stateMachine;
        }

        public async Task Send(DesapropriacaoPremioContext context, IPipe<DesapropriacaoPremioContext> next)
        {
            foreach (var premio in context.Premios)
            {
                if (!await _premios.VerificarUltimoPremio(premio.ItemCertificadoApoliceId, context.Evento.MovimentosPermitidos, premio.Numero))
                    Assertion.Fail($"A Desapropriação não pode ser realizado. Não foi encontrada nenhuma apropropriação com o ItemCertificadoApoliceId {premio.ItemCertificadoApoliceId}, com o Número da Parcela { premio.Numero }.");

                var instance = (await _coberturas.ObterPorItemCertificado(premio.ItemCertificadoApoliceId));

                await _stateMachine.RaiseEvent(instance, _stateMachine.DesapropriarPremio, new NotificacaoEvento<EventoDesapropriacaoPremio, PremioDesapropriado>(context.Evento, premio));
            }
            await next.Send(context);
        }

        public void Probe(ProbeContext context)
        {
            context.CreateScope("DesapropriacaoPremio");
        }
    }

    //public class PremioBaseFilters<T> : IFilter<T> where T : ProvisaoContext
    //{
    //    private readonly ICoberturas _coberturas;
    //    private readonly IPremios _premios;
    //    private readonly CoberturaStateMachine _stateMachine;

    //    public PremioBaseFilters(ICoberturas coberturas, IPremios premios, CoberturaStateMachine stateMachine)
    //    {
    //        _coberturas = coberturas;
    //        _premios = premios;
    //        _stateMachine = stateMachine;
    //    }

    //    public async Task Send(T context, IPipe<T> next)
    //    {
    //        //foreach (var premio in context.Premios)
    //        //{
    //        //    //if (!await _premios.VerificarPremioEmitido(premio.ItemCertificadoApoliceId, (short)TipoMovimentoEnum.Emissao, premio.Numero))
    //        //    //    Assertion.Fail($"A apropriação não pode ser realizado, pois não foi encontrada nenhuma emissão com o ItemCertificadoApoliceId {premio.ItemCertificadoApoliceId}, com o Número da Parcela { premio.Numero }.");

    //        //    var instance = (await _coberturas.ObterPorItemCertificado(premio.ItemCertificadoApoliceId));

    //        //    //await _stateMachine.RaiseEvent(instance, _stateMachine.ApropriarPremio, new NotificacaoEvento<EventoApropriacaoPremio, PremioApropriado>(context.Evento as EventoApropriacaoPremio, premio));
    //        //}
    //        await next.Send(context);
    //    }

    //    public void Probe(ProbeContext context)
    //    {
    //        context.CreateScope("Premio");
    //    }
    //}
}
