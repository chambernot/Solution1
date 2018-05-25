using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.DTO
{
    public class DadosProduto
    {
        public int TipoFormaContratacaoId { get; set; }
        public int ItemProdutoId { get; set; }
        public DateTime DataVigenciaCobertura { get; set; }
        public int ProdutoId { get; set; }
        public string NomeProduto { get; set; }
        public string NumeroProcessoSusep { get; set; }
        public int PlanoFipSusep { get; set; }
        public short? RegimeFinanceiroId { get; set; }
        public int TipoItemProdutoId { get; set; }
        public int ModalidadeCoberturaId { get; set; }
        public int? IndiceBeneficioId { get; set; }
        public int? IndiceContribuicaoId { get; set; }        
        public bool PermiteResgateParcial { get; set; }
        public int ProvisoesPossiveis { get; set; }
        public DateTime DataInicioVigencia { get; set; }
        public DateTime DataFimVigencia { get; set; }
        public short? NumeroBeneficioSusep { get; set; }
    }
}
