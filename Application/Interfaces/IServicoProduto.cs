using Mongeral.Provisao.V2.DTO;


namespace Mongeral.Provisao.V2.Application.Interfaces
{
    public interface IServicoProduto
    {
        DadosProduto ObterDadosProduto(ChaveProduto key);
    }
}
