using Mongeral.Provisao.V2.Adapters;
using Mongeral.Provisao.V2.Adapters.Containers;
using Mongeral.Provisao.V2.DTO;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Conteiners;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Entitdades;
using Moq;
using NUnit.Framework;

namespace Mongeral.Provisao.V2.Testes.Integracao.Adapters
{
    [TestFixture]
    public class ProdutoAdaptersTest: IntegrationTestBase
    {
        private ProdutoContainer _produtoConteiner;
        private ChaveProduto _chave;
        private Mock<IProdutoAdapter> _produtoAdapter;
        private DadosProduto _dadosProduto;

        [OneTimeSetUp]
        public new void FixtureSetUp()
        {
            _chave  = ChaveProdutoBuilder.UmaChave().Build();
            
            _dadosProduto = DadosProdutoBuilder.Um().Padrao().Build();

            _produtoAdapter = new Mock<IProdutoAdapter>();
            _produtoAdapter.Setup(p => p.ObterDadosProduto(It.IsAny<ChaveProduto>())).Returns(_dadosProduto);

            _produtoConteiner = new ProdutoContainer(_produtoAdapter.Object);

            _produtoConteiner.GetValue(_chave);
        }

        [Test]
        public void Dado_uma_nova_ChaveProduto_deve_buscar_dados_no_servico()
        {
            _produtoAdapter.Verify(m => m.ObterDadosProduto(_chave));
        }

        [Test]
        public void Dado_uma_ChaveProduto_existente_deve_buscar_dados_no_cache()
        {
            _produtoConteiner.GetValue(_chave);

            _produtoAdapter.Verify(m => m.ObterDadosProduto(_chave));
        }
    }
}
