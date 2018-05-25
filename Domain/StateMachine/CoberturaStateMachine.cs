using Automatonymous;
using Mongeral.Infrastructure.Cache;
using Mongeral.Provisao.V2.Domain.Entidades;
using Mongeral.Provisao.V2.Domain.Entidades.EventosOperacional;
using Mongeral.Provisao.V2.Domain.Entidades.Premios;
using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.Provisao.V2.Domain.Factories;
using Mongeral.Provisao.V2.DTO;

namespace Mongeral.Provisao.V2.Domain.StateMachine
{
    public class CoberturaStateMachine : AutomatonymousStateMachine<CoberturaContratada>
    {
        public CoberturaStateMachine(
            IndexedCachedContainer<ChaveProduto, DadosProduto> produtoContainer,
            ICalculadorProvisaoPremio calculadorProvisao)
        {
            InstanceState(x => x.Status, Ativa, Cancelada, Beneficio, Saldada, Remida);

            Event(() => Implantar);
            Event(() => Alterar);
            Event(() => EmitirPremio);
            Event(() => ApropriarPremio);
            Event(() => DesapropriarPremio);
            Event(() => AjustePremio);
            Event(() => PortabilidadePremio);
            Event(() => AportePremio);
            Event(() => InclusaoVG);

            Initially(
                When(Implantar)
                    .TransitionTo(Ativa)
                    .Then(x => x.Instance.ComDadosProduto(produtoContainer.GetValue(x.Instance.ChaveProduto())))
                    .Then(x => x.Data.EventoOperacional.Adicionar(x.Instance))
            );

            During(Ativa,
                When(Alterar)
                    .Then(x =>
                    {
                        x.Data.Mensagem.Cobertura.Id = x.Instance.Id;
                        x.Data.EventoOperacional.AdicionaHistorico(x.Data.Mensagem);
                    }),
                When(EmitirPremio)
                    .Then(x =>
                    {
                        x.Data.Mensagem.AdicionarCobertura(x.Instance);
                        x.Data.Mensagem.AdicionarMovimentoProvisao(
                            calculadorProvisao.CriarProvisao(x.Data.Mensagem, TipoEventoEnum.EmissaoPremio, x.Data.EventoOperacional.DataExecucaoEvento));
                        x.Data.EventoOperacional.AdicionarPremiosEmitidos(x.Data.Mensagem);
                    }),
                When(ApropriarPremio)
                    .Then(x =>
                    {
                        x.Data.Mensagem.AdicionarCobertura(x.Instance);
                        x.Data.Mensagem.AdicionarMovimentoProvisao(
                            calculadorProvisao.CriarProvisao(x.Data.Mensagem, TipoEventoEnum.ApropriacaoPremio, x.Data.EventoOperacional.DataExecucaoEvento));
                        x.Data.EventoOperacional.AdicionarPremiosApropriados(x.Data.Mensagem);
                    }),
                When(DesapropriarPremio)
                    .Then(x =>
                    {
                        x.Data.Mensagem.AdicionarCobertura(x.Instance);
                        x.Data.Mensagem.AdicionarMovimentoProvisao(
                            calculadorProvisao.CriarProvisao(x.Data.Mensagem, TipoEventoEnum.DesapropriacaoPremio, x.Data.EventoOperacional.DataExecucaoEvento));
                        x.Data.EventoOperacional.AdicionarPremio(x.Data.Mensagem);
                    }),
                When(AjustePremio)
                    .Then(x =>
                    {
                        x.Data.Mensagem.AdicionarCobertura(x.Instance);
                        x.Data.Mensagem.AdicionarMovimentoProvisao(
                            calculadorProvisao.CriarProvisao(x.Data.Mensagem, TipoEventoEnum.AjustePremio, x.Data.EventoOperacional.DataExecucaoEvento));
                        x.Data.EventoOperacional.AdicionarPremio(x.Data.Mensagem);
                    }),
                When(PortabilidadePremio)
                    .Then(x =>
                    {
                        x.Data.Mensagem.AdicionarCobertura(x.Instance);
                        x.Data.Mensagem.AdicionarMovimentoProvisao(
                            calculadorProvisao.CriarProvisao(x.Data.Mensagem, TipoEventoEnum.PortabilidadePremio, x.Data.EventoOperacional.DataExecucaoEvento));
                        x.Data.EventoOperacional.AdicionarPremio(x.Data.Mensagem);
                    }),
                When(AportePremio)
                    .Then(x =>
                    {
                        x.Data.Mensagem.AdicionarCobertura(x.Instance);
                        x.Data.Mensagem.AdicionarMovimentoProvisao(
                            calculadorProvisao.CriarProvisao(x.Data.Mensagem, TipoEventoEnum.AportePremio, x.Data.EventoOperacional.DataExecucaoEvento));
                        x.Data.EventoOperacional.AdicionarPremio(x.Data.Mensagem);
                    })
                );

            Initially(
                When(InclusaoVG)
                    .TransitionTo(Ativa)
                    .Then(x => x.Instance.ComDadosProduto(produtoContainer.GetValue(x.Instance.ChaveProduto())))
                    .Then(x => x.Data.EventoOperacional.Adicionar(x.Instance))
            );
        }

        public State Cancelada { get; set; }
        public State Beneficio { get; set; }
        public State Ativa { get; set; }
        public State Saldada { get; set; }
        public State Remida { get; set; }

        public Event<NotificacaoEvento<EventoImplantacao, CoberturaContratada>> Implantar { get; set; }
        public Event<NotificacaoEvento<EventoAlteracao, HistoricoCoberturaContratada>> Alterar { get; set; }
        public Event<NotificacaoEvento<EventoEmissaoPremio, Premio>> EmitirPremio { get; set; }
        public Event<NotificacaoEvento<EventoApropriacaoPremio, PremioApropriado>> ApropriarPremio { get; set; }
        public Event<NotificacaoEvento<EventoDesapropriacaoPremio, PremioDesapropriado>> DesapropriarPremio { get; set; }
        public Event<NotificacaoEvento<EventoAjustePremio, Premio>> AjustePremio { get; set; }
        public Event<NotificacaoEvento<EventoPortabilidadePremio, PremioPortabilidade>> PortabilidadePremio { get; set; }
        public Event<NotificacaoEvento<EventoAportePremio, PremioAporte>> AportePremio { get; set; }
        public Event<NotificacaoEvento<EventoInclusaoVg, CoberturaContratada>> InclusaoVG { get; set; }
    }    

    public class NotificacaoEvento<TEvento, TData> where TEvento : EventoOperacional where TData : class
    {
        public TEvento EventoOperacional { get; }
        public TData Mensagem { get; }

        public NotificacaoEvento(TEvento evento, TData data)
        {
            EventoOperacional = evento;
            Mensagem = data;
        }
    }

    public static class CoberturaContratadaExtensions
    {
        public static ChaveProduto ChaveProduto(this CoberturaContratada cob)
        {
            return new ChaveProduto(cob.ItemProdutoId, cob.TipoFormaContratacaoId, cob.TipoRendaId, cob.DataAssinatura);
        }
    }
}
