using Mongeral.Provisao.V2.Service.Premio.Validacao;
using Mongeral.Provisao.V2.Testes.Infra.Builders;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Implantacao;
using NUnit.Framework;

namespace Mongeral.Provisao.V2.Testes.Unitarios.Validacao
{
    [TestFixture]
    public class CoberturaContratadaExtensionsTest: UnitTesBase
    {
        private long _inscricaoCertificado = IdentificadoresPadrao.InscricaoId;
        [Test]
        public void ValidarItemCertificadoApolice()
        {
            var cobertura = CoberturaBuilder.UmaCobertura().Build();

            Assert.That(() => cobertura.Validar(_inscricaoCertificado).Validate(), GeraErro($"Cobertura com identificador externo inválido. Inscrição Certificado: {_inscricaoCertificado}."));
        }

        [Test]
        public void ValidarItemProduto()
        {
            var cobertura = CoberturaBuilder.UmaCobertura().ComItemCertificadoApolice(IdentificadoresPadrao.ItemCertificadoApoliceId).Build();

            Assert.That(() => cobertura.Validar(_inscricaoCertificado).Validate(), GeraErro($"Cobertura com ItemProduto inválido. Identificador Externo: {cobertura.IdentificadorExterno}."));
        }

        [Test]
        public void ValidarInicioVigencia()
        {
            var cobertura = CoberturaBuilder.UmaCobertura().ComItemCertificadoApolice(IdentificadoresPadrao.ItemCertificadoApoliceId).Build();

            Assert.That(() => cobertura.Validar(_inscricaoCertificado).Validate(), GeraErro($"Cobertura com data de inicio de vigencia inválida: Identificador Externo: {cobertura.IdentificadorExterno}."));
        }
    }
}
