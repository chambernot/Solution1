using System;
using Mongeral.Integrador.Contratos.Enum;
using Mongeral.Provisao.V2.Adapters;
using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.Provisao.V2.DTO;
using Mongeral.Provisao.V2.Testes.Infra.Builders;

namespace Mongeral.Provisao.V2.Testes.Infra.Fakes
{
    public class ProdutoAdapterFake: IProdutoAdapter
    {
        public DadosProduto ObterDadosProduto(ChaveProduto key)
        {
            return new DadosProduto()
            {   
                ItemProdutoId = 200419,
                TipoFormaContratacaoId = (int)TipoFormaContratacaoEnum.CapitalSegurado,
                DataVigenciaCobertura = new DateTime(2017,  4, 3),
                DataInicioVigencia = DateTime.Now.AddYears(-2),
                DataFimVigencia = DateTime.Now.AddYears(1),
                ProdutoId = 1589,
                NomeProduto = "SEGURO TOTAL MISTO 10",
                NumeroProcessoSusep = "15414.0001996/2007-61",
                RegimeFinanceiroId = 1,
                TipoItemProdutoId = (int)TipoItemProdutoEnum.VidaIndividual,
                ModalidadeCoberturaId = 1,
                IndiceBeneficioId = 3,
                IndiceContribuicaoId = 3,
                PermiteResgateParcial = true,
                ProvisoesPossiveis = 2099,
                PlanoFipSusep = 0,
                NumeroBeneficioSusep = null
            };
        }
    }
}
