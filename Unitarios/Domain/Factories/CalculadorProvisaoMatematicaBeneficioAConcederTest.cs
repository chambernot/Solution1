using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.Provisao.V2.Domain.Factories;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Entitdades;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Provisao;


using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.DTO;
using System;

namespace Mongeral.Provisao.V2.Testes.Unitarios.Domain.Factories
{
    [TestFixture]
    public class CalculadorProvisaoMatematicaBeneficioAConcederTest: UnitTesBase
    {
        private Mock<ICalculoFacade> _facade;
        private Premio _premio;
        private CalculadorProvisaoMatematicaBeneficioAConceder _calculadorPmbac;
        private ProvisaoMatematicaBeneficioAConceder _resultado;
        private IEnumerable<ProvisaoDto> _listaProvisao;

        [OneTimeSetUp]
        public void SetUpFixture()
        {
            _premio = PremioBuilder.Um().Padrao()
                    .Com(CoberturaContratadaBuilder.Uma().ComItemProdutoId((int)TipoItemProdutoEnum.VidaIndividual)
                        .Com(HistoricoCoberturaContratadaBuilder.UmHistorico().ComDadosPadroes()))
                .Build();

            _premio.InformaEvento(EventoEmissaoPremioBuilder.UmEvento().Padrao().Build());

            _resultado = PmbacBuilder.UmBuilder().Padrao().Build();
                       
            _facade = new Mock<ICalculoFacade>();
            _facade.Setup(x => x.CalcularPMBAC(It.IsAny<CoberturaContratada>(), It.IsAny<DateTime>(), It.IsAny<decimal>())).Returns(_resultado);
            
            _calculadorPmbac = new CalculadorProvisaoMatematicaBeneficioAConceder(_facade.Object);
        }

        [Test]
        public void AoCalcularAProvisaoDeUmProdutoSemInformarOTipoDeveRetornarOsDadosDeProvisaoPreenchidos()
        {
            var result = _calculadorPmbac.CalculaProvisao(_premio, 0).First();

            Assert.That(result.Valor, Is.Not.Null);
            Assert.That(result.DataMovimentacao, Is.Not.Null);
        }

        [Test]
        public void AoCalcularUmaProvisaoDeUmProdutoVidaIndividualDeveRetornarUmaListaDeProvisoes()
        {
            _premio = PremioBuilder.Um().Padrao()                
                .Com(CoberturaContratadaBuilder.Uma().ComTipoItemProdutoId((int)TipoItemProdutoEnum.VidaIndividual)
                    .Com(HistoricoCoberturaContratadaBuilder.UmHistorico()))
              .Build();

            _premio.InformaEvento(EventoEmissaoPremioBuilder.UmEvento().Padrao().Build());

            var result = _calculadorPmbac.CalculaProvisao(_premio, default(decimal)).First();

            Assert.That(result.Valor, Is.Not.EqualTo(default(decimal)));
        }

        [Test]
        public void AoCalcularUmaProvisaoDeUmProdutoVGBLDeveRetornarUmaListaVazia()
        {
            _premio = PremioBuilder.Um().Padrao()                
                .Com(CoberturaContratadaBuilder.Uma().ComTipoItemProdutoId((int)TipoItemProdutoEnum.VGBL)
                    .Com(HistoricoCoberturaContratadaBuilder.UmHistorico()))
                .Build();

            _premio.InformaEvento(EventoEmissaoPremioBuilder.UmEvento().Padrao().Build());

            _listaProvisao = _calculadorPmbac.CalculaProvisao(_premio, default(decimal));

            Assert.That(_listaProvisao.Count, Is.EqualTo(0));
        }

        [Test]
        public void AoCalcularUmaProvisaoDeUmProdutoPGBLDeveRetornarUmaListaVazia()
        {
            _premio = PremioBuilder.Um().Padrao()                
                .Com(CoberturaContratadaBuilder.Uma().ComTipoItemProdutoId((int)TipoItemProdutoEnum.PGBL)
                    .Com(HistoricoCoberturaContratadaBuilder.UmHistorico()))
                .Build();

            _premio.InformaEvento(EventoEmissaoPremioBuilder.UmEvento().Padrao().Build());

            _listaProvisao = _calculadorPmbac.CalculaProvisao(_premio, default(decimal));

            Assert.That(_listaProvisao.Count, Is.EqualTo(0));
        }
    }
}
