using Mongeral.Infrastructure.Cache;
using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.Provisao.V2.DTO;
using Mongeral.Provisao.V2.Testes.Infra.Builders;

namespace Mongeral.Provisao.V2.Testes.Infra.Fake
{
    public class DadosProdutoUtilFake : IndexedCachedContainer<ChaveProduto,DadosProduto>
    {

        protected override DadosProduto GetNewValue (ChaveProduto key)
        {
            return new DadosProduto()
            {
                DataVigenciaCobertura = IdentificadoresPadrao.Competencia,
                DataInicioVigencia = IdentificadoresPadrao.DataInicioVigencia,
                DataFimVigencia = IdentificadoresPadrao.DataFimVigencia,
                IndiceBeneficioId = 3,
                IndiceContribuicaoId = 3,
                ItemProdutoId = 153417,
                ModalidadeCoberturaId = 1,
                NomeProduto = "SEGURO TOTAL MISTO 10",
                NumeroProcessoSusep = "15414.0001996/2007-61",
                PermiteResgateParcial = true,
                PlanoFipSusep = 0,
                ProdutoId = 1534,
                RegimeFinanceiroId = 1,
                TipoItemProdutoId = (int)TipoItemProdutoEnum.VidaIndividual,
                ProvisoesPossiveis = 2099
            };
        }

        protected override int TimeToDie => OneDay;
    }
}
