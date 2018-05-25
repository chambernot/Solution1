using Mongeral.Infrastructure.Ioc;
using Mongeral.Integrador.Contratos;
using Mongeral.Provisao.V2.Service.Premio;
using NUnit.Framework;
using System;
using System.Linq;
using TechTalk.SpecFlow;
using Mongeral.Provisao.V2.Testes.Aceitacao.Util;
using TechTalk.SpecFlow.Assist;
using Mongeral.Integrador.Contratos.Enum;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Implantacao;
using Mongeral.Provisao.V2.Service.Premio.Eventos;
using Mongeral.Provisao.V2.Testes.Infra.Builders;

namespace Mongeral.Provisao.V2.Testes.Aceitacao.StepDefinitions
{
    [Binding]
    public class EventoAtualizacaoParametrosSteps
    {
        private IEventosBase<EventoAlteracao> _repository;
        private ICoberturas _coberturas;
        private IProposta _proposta;        
        private Guid _identificador = Guid.NewGuid();
        private ImplantacaoParam _param;
        private long _itemCertificadoApoliceId;
               

        [Given(@"que há um evento de atualizacao de parametros com os seguintes dados")]
        public void DadoQueHaUmEventoDeAtualizacaoDeParametrosComOsSeguintesDados(Table proposta)
        {
            _param = proposta.CreateSet<ImplantacaoParam>().First();

            ObterPeriodicidade(_param.Periodicidade);

            _itemCertificadoApoliceId = _param.IdExterno;
            
            CriarProposta(_param, _itemCertificadoApoliceId);
        }

        [Scope(Tag = "EventoAlteracaoParametros")]
        [When(@"processar o evento")]
        public void QuandoProcessarOEvento()
        {
            var implantacaoService = InstanceFactory.Resolve<ImplantacaoPropostaService>();

            implantacaoService.Execute(_proposta).Wait();

            var service = InstanceFactory.Resolve<AlteracaoPropostaService>();

            service.Execute(_proposta).Wait();
        }

        [Then(@"deve ser criado um evento com os dados abaixo")]
        public void EntaoDeveSerCriadoUmEventoComOsDadosAbaixo(Table param)
        {
            var saida = param.CreateSet<ImplantacaoParam>().First();

            _repository = InstanceFactory.Resolve<IEventosBase<EventoAlteracao>>();

            var eventoCriado = _repository.ExisteEvento(saida.Identificador).Result;

            Assert.That(eventoCriado, Is.EqualTo(true));
        }    

        [Then(@"um historico de cobertura contratada com")]
        public void EntaoUmHistoricoDeCoberturaContrataCom(Table param)
        {           
            var saida = param.CreateSet<ImplantacaoParam>().First();

            _coberturas = InstanceFactory.Resolve<ICoberturas>();

            var coberturaDto = _coberturas.ObterPorItemCertificado(_itemCertificadoApoliceId).Result;

            Assert.That(_param.IdExterno, Is.EqualTo(coberturaDto.ItemCertificadoApoliceId));            
            
            Assert.That(saida.Periodicidade, Is.EqualTo(coberturaDto.Historico.PeriodicidadeId));
            Assert.That(saida.DataNascimento, Is.EqualTo(coberturaDto.Historico.DataNascimentoBeneficiario));
            Assert.That(saida.Sexo, Is.EqualTo(coberturaDto.Historico.SexoBeneficiario));
        }

        private void CriarProposta(ImplantacaoParam param, long itemCertificadoApolice)
        {
            _proposta = PropostaBuilder.UmaProposta().Padrao().Padrao()
                .ComIdentificador(_param.Identificador)
                .ComDataAssinatura(_param.DataInicioVigencia.AddMonths(6))
                .Com(DadosPagamentoBuilder.UmPagamento()
                    .ComPeriodicidade(ObterPeriodicidade(_param.Periodicidade)))
                .Com(ProponenteBuider.UmProponente().Padrao()
                    .ComDataNascimento(_param.DataNascimento)
                    .ComMatricula(_param.Matricula)
                    .ComSexo(_param.Sexo))
                .Com(ProdutoBuilder.UmProduto().Padrao()
                    .ComMatricula(IdentificadoresPadrao.Matricula)
                    .ComCodigo(IdentificadoresPadrao.ProdutoId)
                    .ComInscricao(_param.InscricaoId)
                    .Com(BeneficiarioBuilder.UmBeneficiario().Padrao())
                    .Com(CoberturaBuilder.UmaCobertura().Padrao()
                        .ComInicioVigencia(_param.DataInicioVigencia.AddYears(-1))
                        .ComItemCertificadoApolice(_param.IdExterno)
                        .ComItemProdutoId(_param.ItemProdutoId)
                        .ComInicioVigencia(_param.DataInicioVigencia)
                        .ComClasseRisco(_param.ClasseRiscoId)
                        .Com(ContratacaoBuilder.UmaContratacao().Padrao()
                            .ComTipoRenda(ObterTipoRenda(_param.TipoRendaId))
                            .ComTipoFormaContratacao(ObterTipoFormaContratacao(_param.TipoFormaContratacaoId))
                        ))).Build();

        }


        private TipoDeRendaEnum ObterTipoRenda(int tipoRendaId)
        {
            return (TipoDeRendaEnum)Enum.ToObject(typeof(TipoDeRendaEnum), tipoRendaId);
        }

        private TipoFormaContratacaoEnum ObterTipoFormaContratacao(int tipoFormaContratacao)
        {
            return (TipoFormaContratacaoEnum)Enum.ToObject(typeof(TipoFormaContratacaoEnum), tipoFormaContratacao);
        }

        private Periodicidade ObterPeriodicidade(int periodicidade)
        {
            return (Periodicidade)Enum.ToObject(typeof(Periodicidade), periodicidade);
        }
    }
}
