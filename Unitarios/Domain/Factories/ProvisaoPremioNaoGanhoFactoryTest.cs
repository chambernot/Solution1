using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.Provisao.V2.Domain.Factories;
using Mongeral.Provisao.V2.Testes.Infra.Builders;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Entitdades;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Testes.Unitarios.Domain.Factories
{
    [TestFixture]
    public class ProvisaoPremioNaoGanhoFactoryTest : UnitTesBase
    {
        private CalculadorProvisaoPremioNaoGanhoPremio _calculadorPPNG;
        private Premio _premio;
        private DateTime _competencia = new DateTime(2017, 01, 01);

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            _calculadorPPNG = GetInstance<CalculadorProvisaoPremioNaoGanhoPremio>();
        }

        [Test]
        public void DadoUmPremioDeveSeCalcularPPNGRetornandoUmaListaDeProvisao()
        {
            _premio = PremioBuilder.Um().Padrao().Build();

            _premio.InformaEvento(EventoEmissaoPremioBuilder.UmEvento().Padrao().Build());

            var listaProvisao = _calculadorPPNG.CalcularProvisao(_premio);

            var qtdCompetencias = CalcularMeses(_premio.FimVigencia, _premio.EventoOperacional.DataExecucaoEvento) + 1;

            Assert.That(listaProvisao.ToList().Count, Is.EqualTo(qtdCompetencias));
        }

        [Test]
        public void AoCalcularUmaProvisaoComMesDevigenciaIgualAoMesFimVigenciaDeveRetornarUmaProvisao()
        {
            _premio = PremioBuilder.Um().Padrao()
                .Com(CoberturaContratadaBuilder.Uma().Com(HistoricoCoberturaContratadaBuilder.UmHistorico()))
                .ComDataCompetencia(new DateTime(2017, 01, 01))
                .ComFimVigencia(new DateTime(2017, 01, 31))
                .Build();

            _premio.InformaEvento(EventoEmissaoPremioBuilder.UmEvento().Padrao().Build());

            var listaProvisao = _calculadorPPNG.CalcularProvisao(_premio);

            var qtdCompetencias = 1;

            Assert.That(qtdCompetencias, Is.EqualTo(listaProvisao.ToList().Count));
        }
    }
}
