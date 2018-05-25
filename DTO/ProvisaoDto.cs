using Mongeral.Integrador.Contratos.Premio;
using System;

namespace Mongeral.Provisao.V2.DTO
{
    public class ProvisaoDto : IProvisao
    {
        public short ProvisaoId { get; set; }
        public DateTime DataMovimentacao { get; set; }
        public decimal Valor { get; set; }
        public decimal? AtualizacaoMonetaria { get; set; }
        public decimal? Juros { get; set; }
        public decimal? PercentualTaxaJuros { get; set; }
        public decimal? ValorSobrevivencia { get; set; }
        public decimal? ValorUltimaContribuicao { get; set; }
        public DateTime? DataUltimaAtualizacaoContribuicao { get; set; }
        public short? NaturezaRenda { get; set; }
        public short? TabuaId { get; set; }
        public short? IndiceId { get; set; }
        public string CodigoPortabilidadeInterna { get; set; }
    }
}
