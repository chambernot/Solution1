using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Service.Premio.Validacao;
using Mongeral.Provisao.V2.Testes.Infra.Builders;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio;
using NUnit.Framework;

namespace Mongeral.Provisao.V2.Testes.Unitarios.Validacao
{
    [TestFixture]
    public class PremioExtensionsTest: UnitTesBase
    {
        private IParcela _parcela;

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            _parcela = ParcelaBuilder.UmBuilder()                
                .Com(ParcelaIdBuilder.UmBuilder().ComNumeroParcela(-1)
                    .ComIdentificadorExternoCobertura(IdentificadoresPadrao.ItemCertificadoApoliceId.ToString()))
                .Com(VigenciaBuilder.UmBuilder())
                .Com(ValorBuilder.UmBuilder())
                .Build();
        }

        [Test]
        public void ValidarIdentificadorExterno()
        {
            var parcela = ParcelaBuilder.UmBuilder().Com(ParcelaIdBuilder.UmBuilder()).Com(VigenciaBuilder.UmBuilder()).Com(ValorBuilder.UmBuilder()).Build();

            Assert.That(() => parcela.Validar(), GeraErro($"A ParcelaId: { parcela.ParcelaId.ParcelaId}, com vigência com Identificador Externo inválido."));
        }

        [Test]
        public void ValidarParcelaId()
        {
            var parcela = ParcelaBuilder.UmBuilder().Build();

            Assert.That(() => parcela.Validar(), GeraErro("A ParcelaId não foi informada."));
        }

        [Test]
        public void ValidarVigencia()
        {
            var parcela = ParcelaBuilder.UmBuilder().Com(ParcelaIdBuilder.UmBuilder()).Com(ValorBuilder.UmBuilder()).Build();

            Assert.That(() => parcela.Validar(), GeraErro($"A ParcelaId: { parcela.ParcelaId.ParcelaId}, com vigência inválida."));
        }

        [Test]
        public void ValidarValores()
        {
            var parcela = ParcelaBuilder.UmBuilder().Com(ParcelaIdBuilder.UmBuilder()).Build();

            Assert.That(() => parcela.Validar(), GeraErro($"A ParcelaId: { parcela.ParcelaId.ParcelaId}, com valores inválidos."));
        }

        [Test]
        public void ValidarInicioVigencia()
        {
            Assert.That(() => _parcela.Validar(), GeraErro($"IdentificadorExterno: { _parcela.ParcelaId.IdentificadorExternoCobertura }, com Inicio da Vigência inválido."));
        }

        [Test]
        public void ValidarFimVigencia()
        {
            Assert.That(() => _parcela.Validar(), GeraErro($"IdentificadorExterno: { _parcela.ParcelaId.IdentificadorExternoCobertura }, com Fim da Vigência inválido."));
        }

        [Test]
        public void ValidarCompetencia()
        {
            Assert.That(() => _parcela.Validar(), GeraErro($"IdentificadorExterno: { _parcela.ParcelaId.IdentificadorExternoCobertura }, com Competência inválido."));
        }        

        [Test]
        public void ValidarNumeroParcela()
        {
            Assert.That(() => _parcela.Validar(), GeraErro($"IdentificadorExterno: { _parcela.ParcelaId.IdentificadorExternoCobertura }, com Numero da Parcela inválido."));
        }

        [Test]
        public void ValidarValorPremio()
        {
            Assert.That(() => _parcela.Validar(), GeraErro($"IdentificadorExterno: { _parcela.ParcelaId.IdentificadorExternoCobertura }, o Valor de Contribuição não foi informado."));
        }

        [Test]
        public void ValidarValorBeneficio()
        {
            Assert.That(() => _parcela.Validar(), GeraErro($"IdentificadorExterno: { _parcela.ParcelaId.IdentificadorExternoCobertura }, o Valor de Benfício não foi informado."));
        }
    }
}
