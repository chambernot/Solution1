using NUnit.Framework;
using Mongeral.Provisao.V2.Adapters.Containers;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Conteiners;
using Moq;
using Mongeral.Provisao.V2.Adapters;
using Mongeral.Provisao.V2.DTO;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Entitdades;

namespace Mongeral.Provisao.V2.Testes.Unitarios
{
    [TestFixture]
    public class ProdutoContainerTest : UnitTesBase
    {
        private ChaveProduto _chave;        
        private Mock<IProdutoAdapter> _adapter;
        private ProdutoContainer _servicoProduto;

        private DadosProduto _detalhe;

        [OneTimeSetUp]
        protected void FixtureSetUp()
        {
            _chave = ChaveProdutoBuilder.UmaChave().Build();
            
            _detalhe = DadosProdutoBuilder.Um().Padrao().Build();

            _adapter = new Mock<IProdutoAdapter>();
            _adapter.Setup(p => p.ObterDadosProduto(_chave)).Returns(_detalhe);

            _servicoProduto = new ProdutoContainer(_adapter.Object);

            _detalhe = _servicoProduto.GetValue(_chave);
        }

        [Test]
        public void Dado_uma_ChaveProduto_deve_obter_dados_do_Produto_do_Servico()
        {
            _adapter.Verify(m => m.ObterDadosProduto(_chave));
        }

        [Test]
        public void Dado_uma_ChaveProduto_existente_deve_obter_dados_do_Produto_no_Cache()
        {
            _detalhe = _servicoProduto.GetValue(_chave);

            _adapter.Verify(m => m.ObterDadosProduto(_chave), Times.Once);
        }
    }
}