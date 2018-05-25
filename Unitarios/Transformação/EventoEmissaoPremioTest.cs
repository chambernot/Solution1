using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.Provisao.V2.Domain.Factories;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.Service.Premio.Factory;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Entitdades;
using Moq;
using Ninject;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Testes.Unitarios.Transformação
{
    [TestFixture]
    public class EventoEmissaoPremioTest: UnitTesBase
    {
        private EventoPremio _evento;
        private IParcelaEmitida _parcelaEmitida;
        private List<MovimentoProvisao> _retornoMovimento;

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            _parcelaEmitida = ParcelaEmitidaBuilder.UmBuilder().Padrao().Build();
            var premioAnterior = PremioBuilder.Um().Padrao().ComTipoMovimento((short)TipoMovimentoEnum.Inexistente).Build();

            var retornoCobertura = CoberturaContratadaBuilder.Uma().Padrao();
            var movimento = MovimentoProvisaoBuilder.UmBuilder().Padrao();
            _retornoMovimento = new List<MovimentoProvisao>() { movimento.Build() };

            var retornoPremio = PremioBuilder.Um().Padrao()
                .Com(movimento)
                .Com(retornoCobertura)
                .Build();

            MockingKernel.GetMock<IPremioService>()
                .Setup(x => x.CriarPremio(It.IsAny<Premio>(), It.IsAny<EventoPremio>())).Returns(Task.FromResult(retornoPremio));

            MockingKernel.GetMock<ICoberturas>()
                .Setup(x => x.ObterPorItemCertificado(It.IsAny<long>())).Returns(Task.FromResult(retornoCobertura.Build()));

            MockingKernel.GetMock<ICalculadorProvisaoPremio>()
                .Setup(x => x.CriarProvisao(It.IsAny<Premio>())).Returns(_retornoMovimento.AsEnumerable());

            MockingKernel.GetMock<IPremios>()
                .Setup(x => x.ObterPremioAnterior(It.IsAny<long>(), It.IsAny<int>())).Returns(Task.FromResult(premioAnterior));

            _evento = MockingKernel.Get<EventoPremioFactory>().Fabricar(_parcelaEmitida).Result;
        }

        [Test]
        public void MapearEventoEmissao()
        {
            Assert.That(_evento.GetType(), Is.EqualTo(typeof(EventoEmissaoPremio)));

            Assert.That(_parcelaEmitida.Identificador, Is.EqualTo(_evento.Identificador));
            Assert.That(_parcelaEmitida.IdentificadorNegocio, Is.EqualTo(_evento.IdentificadorNegocio));
            Assert.That(_parcelaEmitida.IdentificadorCorrelacao, Is.EqualTo(_evento.IdentificadorCorrelacao));
            Assert.That(_parcelaEmitida.DataExecucaoEvento, Is.EqualTo(_evento.DataExecucaoEvento));
        }

        [Test]
        public void MapearPremioEmitido()
        {
            var parcela = _parcelaEmitida.Parcelas.First();
            var premio = _evento.Premios.First();

            Assert.That(parcela.ParcelaId.IdentificadorExternoCobertura, Is.EqualTo(premio.ItemCertificadoApoliceId.ToString()));            
            Assert.That(parcela.ParcelaId.NumeroParcela, Is.EqualTo(premio.Numero));
            Assert.That(parcela.Vigencia.Competencia, Is.EqualTo(premio.Competencia));
            Assert.That(parcela.Vigencia.Inicio, Is.EqualTo(premio.InicioVigencia));
            Assert.That(parcela.Vigencia.Fim, Is.EqualTo(premio.FimVigencia));
            Assert.That(parcela.Valores.Beneficio, Is.EqualTo(premio.ValorBeneficio));
            Assert.That(parcela.Valores.CapitalSegurado, Is.EqualTo(premio.ValorCapitalSegurado));
            Assert.That(parcela.Valores.Carregamento, Is.EqualTo(premio.ValorCarregamento));            
            Assert.That(parcela.Valores.Contribuicao, Is.EqualTo(premio.ValorPremio));            
        }

        [Test]
        public void MapearMovimentoProvisaoEmissao()
        {
            var movimento = _evento.Premios.First().MovimentosProvisao.First();
            var provisao = _retornoMovimento.First();

            Assert.That(provisao.DataMovimentacao, Is.EqualTo(movimento.DataMovimentacao));
            Assert.That(provisao.ValorJuros, Is.EqualTo(movimento.ValorJuros));
            Assert.That(provisao.PercentualTaxaJuros, Is.EqualTo(movimento.PercentualTaxaJuros));
            Assert.That(provisao.PercentualCarregamento, Is.EqualTo(movimento.PercentualCarregamento));
            Assert.That(provisao.ValorAtualizacao, Is.EqualTo(movimento.ValorAtualizacao));
            Assert.That(provisao.DataUltimaAtualizacaoContribuicao, Is.EqualTo(movimento.DataUltimaAtualizacaoContribuicao));
            Assert.That(provisao.Fator, Is.EqualTo(movimento.Fator));

            Assert.That(provisao.ValorProvisao, Is.EqualTo(movimento.ValorProvisao));
            Assert.That(provisao.ValorBeneficioCorrigido, Is.EqualTo(movimento.ValorBeneficioCorrigido));
            Assert.That(provisao.ValorDesvio, Is.EqualTo(movimento.ValorDesvio));
            Assert.That(provisao.ValorFIF, Is.EqualTo(movimento.ValorFIF));
            Assert.That(provisao.ValorSobrevivencia, Is.EqualTo(movimento.ValorSobrevivencia));
            Assert.That(provisao.ValorUltimaContribuicao, Is.EqualTo(movimento.ValorUltimaContribuicao));
        }
    }
}
