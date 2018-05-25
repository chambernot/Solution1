using Mongeral.Integrador.Contratos;
using Mongeral.Provisao.V2.Service.Premio.Validacao;
using Mongeral.Provisao.V2.Testes.Infra.Builders;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Implantacao;
using NUnit.Framework;

namespace Mongeral.Provisao.V2.Testes.Unitarios.Validacao
{
    [TestFixture]
    public class ProponenteExtensionsTest: UnitTesBase
    {
        private IProponente _proponente;
        private long _numeroProposta = IdentificadoresPadrao.NumeroProposta;

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            _proponente = ProponenteBuider.UmProponente().Build();
        }

        [Test]
        public void ValidarProponente()
        {
            IProponente proponente = null;
            Assert.That(() => proponente.Validar(_numeroProposta).Validate(), GeraErro($"O proponente da proposta {_numeroProposta} não foi informado"));
        }

        [Test]
        public void ValidarProponenteMatricula()
        {
            Assert.That(() => _proponente.Validar(_numeroProposta).Validate(), GeraErro($"Matrícula do proponente inválido. Número da Proposta: {_numeroProposta}."));
        }

        [Test]
        public void ValidarDataNascimento()
        {
            Assert.That(() => _proponente.Validar(_numeroProposta).Validate(), GeraErro($"Data Nascimento do proponente inválido. Número da Proposta: {_numeroProposta}."));
        }
    }
}
