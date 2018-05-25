using Mongeral.Infrastructure.Cache;
using Mongeral.Provisao.V2.DTO;

namespace Mongeral.Provisao.V2.Adapters.Containers
{
    public class ProdutoContainer : IndexedCachedContainer<ChaveProduto, DadosProduto>
    {
        protected override int TimeToDie => OneHour * 12;

        private readonly IProdutoAdapter _adapter;
        public ProdutoContainer(IProdutoAdapter adapter)
        {
            _adapter = adapter;
        }

        protected override DadosProduto GetNewValue(ChaveProduto key)
        {            
            var produto = _adapter.ObterDadosProduto(key);
            key.SetVigencia(produto.DataInicioVigencia, produto.DataFimVigencia);

            return produto;
        }
    }
}
