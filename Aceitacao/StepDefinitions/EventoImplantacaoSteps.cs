using TechTalk.SpecFlow;
using Mongeral.Integrador.Contratos;
using NUnit.Framework;
using System;
using Mongeral.Infrastructure.Ioc;
using Mongeral.Provisao.V2.Service.Premio;
using TechTalk.SpecFlow.Assist;
using Mongeral.Provisao.V2.Testes.Aceitacao.Util;
using Mongeral.Infrastructure.Assertions;
using System.Linq;
using Mongeral.Integrador.Contratos.Enum;
using System.Collections.Generic;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Implantacao;
using Mongeral.Provisao.V2.Testes.Infra.Persisters;
using Mongeral.Provisao.V2.Service.Premio.Eventos;
using Mongeral.Provisao.V2.Testes.Infra.Builders;

namespace Mongeral.Provisao.V2.Testes.Aceitacao.StepDefinitions
{
    [Binding]
    public class EventoImplantacaoSteps
    {
        private IEventosBase<EventoImplantacao> _eventos;
        private ICoberturas _coberturas;

        private CoberturaContratada _coberturaDto;

        private IProposta _proposta;
        private ImplantacaoPropostaService _service;        
        private long _itemCertificadoApoliceId;
        private Guid _coberturaContratadaId = Guid.NewGuid();
        private ImplantacaoParam _param;
        private List<IProposta> _listaProposta = new List<IProposta>();
        private readonly ScenarioContext _scenarioContext;

        public EventoImplantacaoSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        #region "Cenário: Implantacao de uma proposta"

        [Given(@"que há uma proposta com os seguintes dados")]
        public void DadoQueHaUmaPropostaComOsSeguintesDados(Table proposta)
        {
            _param = proposta.CreateSet<ImplantacaoParam>().First();

            ObterPeriodicidade(_param.Periodicidade);

            CriarProposta(_param.Identificador);
                        
            _itemCertificadoApoliceId = _param.IdExterno;
        }

        [When(@"processar o evento de implantacao")]
        public void QuandoProcessarOEventoDeImplantacao()
        {
            _service = InstanceFactory.Resolve<ImplantacaoPropostaService>();

            _service.Execute(_proposta).Wait();
        }

        [Then(@"deve ser criado um evento implantado com os seguintes dados")]
        public void EntaoDeveSerCriadoUmEventoImplantadoComOsSeguintesDados(Table param)
        {
            var saida = param.CreateSet<ImplantacaoParam>().First();

            _eventos = InstanceFactory.Resolve<IEventosBase<EventoImplantacao>>();
            
            Assert.That(_eventos.ExisteEvento(saida.Identificador).Result, Is.EqualTo(true));
        }

        [Then(@"uma cobertura contratada deve conter")]
        public void EntaoUmaCoberturaContratadaDeveConter(Table param)
        {
            var saida = param.CreateSet<ImplantacaoParam>().First();

            _coberturas = InstanceFactory.Resolve<ICoberturas>();

            _coberturaDto = _coberturas.ObterPorItemCertificado(_itemCertificadoApoliceId).Result;            

            Assert.That(_param.IdExterno, Is.EqualTo(_coberturaDto.ItemCertificadoApoliceId));
            Assert.That(_param.ItemProdutoId, Is.EqualTo(_coberturaDto.ItemProdutoId));
            Assert.That(_param.DataInicioVigencia, Is.EqualTo(_coberturaDto.DataInicioVigencia));
            Assert.That(_param.ClasseRiscoId, Is.EqualTo(_coberturaDto.ClasseRiscoId));
            Assert.That(_param.TipoFormaContratacaoId, Is.EqualTo(_coberturaDto.TipoFormaContratacaoId));
            Assert.That(_param.TipoRendaId, Is.EqualTo(_coberturaDto.TipoRendaId));
            Assert.That(_param.InscricaoId, Is.EqualTo(_coberturaDto.InscricaoId));
            Assert.That(_param.Matricula, Is.EqualTo(_coberturaDto.Matricula));
        }

        [Then(@"um historico de cobertura com")]
        public void EntaoUmHistoricoDeCoberturaCom(Table param)
        {
            var saida = param.CreateSet<ImplantacaoParam>().First();

            var historicoDto = _coberturaDto.Historico;

            Assert.That(_param.Periodicidade, Is.EqualTo(historicoDto.PeriodicidadeId));
            Assert.That(_param.DataNascimento, Is.EqualTo(historicoDto.DataNascimentoBeneficiario));
            Assert.That(_param.Sexo, Is.EqualTo(historicoDto.SexoBeneficiario));
        }

        #endregion "Cenário: Implantacao de uma proposta"        

        #region "Cenário: Implantacao duas proposta com o mesmo identificador"

        [Given(@"que há uma implantacao com")]
        public void DadoQueHaUmaImplantacaoCom(Table param)
        {
            _param = param.CreateSet<ImplantacaoParam>().First();

            _itemCertificadoApoliceId = _param.IdExterno;
            CriarProposta(_param.Identificador);

            _listaProposta.Add(_proposta);            
        }

        [Given(@"uma outra implantacao com o mesmo IdExterno")]
        public void DadoUmaOutraImplantacaoComOMesmoIdExterno(Table param)
        {
            _param = param.CreateSet<ImplantacaoParam>().First();

            CriarProposta(_param.Identificador);

            _listaProposta.Add(_proposta);
        }

        [When(@"Processar os evento")]
        public void QuandoProcessarOsEvento()
        {
            var service = InstanceFactory.Resolve<ImplantacaoPropostaService>();

            foreach (var row in _listaProposta)
            {
                service.Execute(row).Wait();
            }
        }

        [Then(@"deve criar apenas um evento com uma cobertura")]
        public void EntaoDeveCriarApenasUmEventoComUmaCobertura()
        {
            var helper = InstanceFactory.Resolve<CoberturaContratadaHelper>();
                        
            var qtdCobertura = helper.ListarCoberturas(_itemCertificadoApoliceId).Result;

            Assert.That(qtdCobertura, Is.EqualTo(1));
        }

        #endregion "Cenário: Implantacao duas proposta com o mesmo identificador"
        
        #region "Cenário: Implantacao de uma proposta com cobertura existente"

        [Given(@"que há uma cobertura com os seguintes dados")]
        public void DadoQueHaUmaCoberturaComOsSeguintesDados(Table param)
        {
            _param = param.CreateSet<ImplantacaoParam>().First();

            CriarProposta(_param.Identificador);
        }

        [When(@"Processar o evento")]
        public void QuandoProcessarOEvento()
        {
            try
            {
                var service = InstanceFactory.Resolve<ImplantacaoPropostaService>();

                //Salva a primeira vez, para em seguida testar a validação
                service.Execute(_proposta).Wait();

                CriarProposta(Guid.NewGuid());

                service.Execute(_proposta).Wait();
            }
            catch (Exception e)
            {
                _scenarioContext.Set(e.InnerException.Message, "Valida");
            }
        }

        [Then(@"ocorre um erro: ""(.*)""")]
        public void EntaoOcorreUmErro(string message)
        {
            var msg = _scenarioContext.Get<string>("Valida");

            Assertion.NotNull(message, msg);
        }

        #endregion "Cenário: Implantacao de uma proposta com cobertura existente"
                
        #region "Genéricos"
        private void CriarProposta(Guid identificador)
        {
            _proposta = PropostaBuilder.UmaProposta().Padrao().Padrao()
                .ComIdentificador(identificador)
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

        #endregion        
    }    
}