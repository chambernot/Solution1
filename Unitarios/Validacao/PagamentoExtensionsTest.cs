using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Service.Premio.Validacao;
using Mongeral.Provisao.V2.Testes.Infra.Builders;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio;
using NUnit.Framework;

namespace Mongeral.Provisao.V2.Testes.Unitarios.Validacao
{
    [TestFixture]
    public class PagamentoExtensionsTest: UnitTesBase
    {
        private IPagamento _pagamento;
        private string _identificadorExterno = IdentificadoresPadrao.ItemCertificadoApoliceId.ToString();

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            _pagamento = PagamentoBuilder.UmBuilder().Build();
        }

        [Test]
        public void ValidarDataPagamento()
        {
            Assert.That(() => _pagamento.Validar(_identificadorExterno), GeraErro($"A Data de Pagamento para o ItemCertificadoApolice: { _identificadorExterno }, não foi informada."));
        }

        [Test]
        public void ValidarDataApropriacao()
        {
            Assert.That(() => _pagamento.Validar(_identificadorExterno), GeraErro($"A Data de Apropriação para o ItemCertificadoApolice: { _identificadorExterno },  não foi informada."));
        }

        [Test]
        public void ValidarValorPago()
        {
            Assert.That(() => _pagamento.Validar(_identificadorExterno), GeraErro($"O Valor Pago para o ItemCertificadoApolice: { _identificadorExterno }, não foi informado."));
        }
    }
}
