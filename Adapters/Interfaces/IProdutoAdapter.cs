using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.DTO;

namespace Mongeral.Provisao.V2.Adapters
{
    public interface IProdutoAdapter
    {
        DadosProduto ObterDadosProduto(ChaveProduto key);
    }
}
