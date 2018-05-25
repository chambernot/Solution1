using Mongeral.Integrador.Contratos;
using Mongeral.Provisao.V2.Service.Premio.Validacao;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Implantacao;
using Moq;
using NUnit.Framework;

namespace Mongeral.Provisao.V2.Testes.Unitarios.Validacao
{
    [TestFixture]
    public class EventoOperacionalExtensionsTest: UnitTesBase
    {
        private IProposta _proposta;

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            _proposta = PropostaBuilder.UmaProposta().Build();
        }

        [Test]
        public void ValidarIdentificador()
        {
            Assert.That(() => _proposta.ValidarEvento().Validate(), GeraErro("O identificador do evento não foi informado."));
        }

        [Test]
        public void ValidarDataExecucao()
        {
            Assert.That(() => _proposta.ValidarEvento().Validate(), GeraErro("A data de execução do evento não foi informada."));
        }
    }
}
