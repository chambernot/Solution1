using Mongeral.Integrador.Contratos;
using Mongeral.Provisao.V2.Testes.TestHelpers.Builder.Entitdades;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Testes.TestHelpers.Builder.Contratos.Implantacao;
using NUnit.Framework;

namespace Mongeral.Provisao.V2.Testes.Unitarios.Service.Extensions
{
    [TestFixture]
    public class PropostaExtensionTest
    {
        [Test]
        public void ObterBeneficiarioMaisNovo_quando_item_produto_contem_um_unico_beneficiario()
        {
            var _proposta = PropostaBuilder.UmaProposta()
                .Com(ProdutoBuilder.UmProduto()
                    .Com(BeneficiarioBuilder.UmBeneficiario())
                    .ComCodigo(10))
                .Build();

            var coberturaContratada = CoberturaContratadaBuilder.Uma().Build();

        }
    }
}
