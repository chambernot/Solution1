using Mongeral.Integrador.Contratos;
using Mongeral.Provisao.V2.Service.Premio.Validacao;
using Mongeral.Provisao.V2.Testes.Infra.Builders;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Implantacao;
using NUnit.Framework;

namespace Mongeral.Provisao.V2.Testes.Unitarios.Validacao
{
    [TestFixture]
    public class ProdutoExtensionsTest: UnitTesBase
    {                
        [Test]
        public void ValidarInscricaoCertificado()
        {
            var produto = ProdutoBuilder.UmProduto().Build();
            Assert.That(() => produto.Validar().Validate(), GeraErro("Não foi informado o numero de certificado / inscrição"));
        }

        [Test]
        public void ValidarCodigoProduto()
        {
            var produto = ProdutoBuilder.UmProduto().ComInscricao(IdentificadoresPadrao.InscricaoId).Build();
            Assert.That(() => produto.Validar().Validate(), GeraErro($"Não foi informado o produtoId para a inscricao { produto.InscricaoCertificado }"));
        }

        [Test]
        public void ValidarMatricula()
        {
            var produto = ProdutoBuilder.UmProduto().ComInscricao(IdentificadoresPadrao.InscricaoId).Build();
            Assert.That(() => produto.Validar().Validate(), GeraErro($"Não foi informada a matricula da inscrição { produto.InscricaoCertificado }"));
        }
    }
}
