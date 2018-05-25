using System;
using Mongeral.Provisao.V2.DTO;

namespace Mongeral.Provisao.V2.Application.Interfaces
{
    public interface IDadosProduto
    {
        DadosProduto Obter(int itemProdutoId, DateTime vigencia);
    }
}