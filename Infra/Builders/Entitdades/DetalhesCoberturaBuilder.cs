using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.DTO;
using Mongeral.Provisao.V2.Testes.Infra.Builders;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Entitdades
{
    public class DadosProdutoBuilder : BaseBuilder<DadosProduto>
    {
        public DadosProdutoBuilder()
        {
            Instance = new DadosProduto();
        }
        
        public static DadosProdutoBuilder Um()
        {
            return new DadosProdutoBuilder();
        }

        public DadosProdutoBuilder Padrao()
        {   
            Instance.IndiceBeneficioId = IdentificadoresPadrao.IndiceBeneficioId;
            Instance.IndiceContribuicaoId = IdentificadoresPadrao.IndiceContribuicaoId;
            Instance.ItemProdutoId = IdentificadoresPadrao.ItemProdutoId;
            Instance.ModalidadeCoberturaId = IdentificadoresPadrao.ModalidadeCoberturaId;
            Instance.ProdutoId = IdentificadoresPadrao.ProdutoId;
            Instance.RegimeFinanceiroId = IdentificadoresPadrao.RegimeFinanceiroId;
            Instance.TipoItemProdutoId = IdentificadoresPadrao.TipoItemProdutoId;
            Instance.NomeProduto = IdentificadoresPadrao.NomeProduto;
            Instance.NumeroProcessoSusep = IdentificadoresPadrao.NumeroProcessoSusep;
            Instance.PlanoFipSusep = IdentificadoresPadrao.PlanoFipSusep;
            Instance.ProvisoesPossiveis = IdentificadoresPadrao.TipoProvisoes;
            Instance.PermiteResgateParcial = IdentificadoresPadrao.PermiteResgateParcial;
            Instance.TipoFormaContratacaoId = IdentificadoresPadrao.TipoFormaContratacaoId;
            Instance.DataInicioVigencia = IdentificadoresPadrao.DataInicioVigencia.AddMonths(-6);
            Instance.DataFimVigencia = IdentificadoresPadrao.DataInicioVigencia.AddMonths(6);
            Instance.DataVigenciaCobertura = IdentificadoresPadrao.DataInicioVigencia;

            return this;
        }
        public DadosProdutoBuilder ComItemProdutoId(int itemProdutoId)
        {
            Instance.ItemProdutoId = itemProdutoId;
            return this;
        }

        public DadosProdutoBuilder ComChaveProduto(ChaveProduto key)
        {
            Instance.ItemProdutoId = key.ItemProdutoId;
            Instance.TipoFormaContratacaoId = key.TipoFormaContratacaoId;
            Instance.DataVigenciaCobertura = key.DataVigencia;

            return this;
        }
    }
}
