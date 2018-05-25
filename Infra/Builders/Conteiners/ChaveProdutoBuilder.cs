using Mongeral.Provisao.V2.DTO;
using System;
using Mongeral.Provisao.V2.Testes.Infra.Builders;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Conteiners
{
    public class ChaveProdutoBuilder: BaseBuilder<ChaveProduto>
    {
        private ChaveProdutoBuilder(int itemProdutoId, int tipoFormaContratacaoId, int? tipoRendaId, DateTime dataInicioVigencia)
        {
            Instance = new ChaveProduto(itemProdutoId, tipoFormaContratacaoId, tipoRendaId, dataInicioVigencia);
        }

        public static ChaveProdutoBuilder UmaChave()
        {
            return new ChaveProdutoBuilder(
                        IdentificadoresPadrao.ItemProdutoId, 
                        IdentificadoresPadrao.TipoFormaContratacaoId, 
                        IdentificadoresPadrao.TipoRendaId,
                        IdentificadoresPadrao.DataInicioVigencia);
        }

        public static ChaveProdutoBuilder UmaPersonalizada(int itemProdutoId, int tipoFormaContratacaoId, int? tipoRendaId, DateTime dataInicioVigencia)
        {
            return new ChaveProdutoBuilder(itemProdutoId, tipoFormaContratacaoId, tipoRendaId, dataInicioVigencia);
        }
    }
}
