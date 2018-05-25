using System;
using Domain.Enum;

namespace Domain.Entities
{
    public class CoberturaContratada
    {
        public int CoberturaContratadaId { get; set; }
        public int EventoImplantadoId { get; set; }
        public long InscricaoId { get; set; }
        public long ItemCertificadoApoliceId { get; set; }
        public int? CodigoProduto { get; set; }
        public int ItemProdutoId { get; set; }
        public DateTime DataVigencia { get; set; }
        public int? ClasseRiscoId { get; set; }
        public TipoFormaContratacaoEnum? TipoFormaContratacaoId { get; set; }        
        public TipoDeRendaEnum? TipoRendaId { get; set; }
        public long Matricula { get; set; }
        public string Sexo { get; set; }
        public DateTime DataNascimento { get; set; }        
        public decimal? ValorCapital { get; set; }
        public decimal? ValorContribuicao { get;  set; }
        public decimal? ValorBeneficio { get; set; }        
        public string IdentificadorProvisao { get; set; }                
        public short? FormaContratacaoId { get; set; }
        public int? IndiceBeneficioId { get; set; }
        public int? IndiceContribuicaoId { get; set; }        
        public int ModalidadeCoberturaId { get; set; }
        public int ProdutoId { get; set; }
        public short? RegimeFinanceiroId { get; set; }
        public short TipoItemProdutoId { get; set; }
        public string NomeProduto { get; set; }
        public short? NumeroBeneficioSusep { get; set; }
        public string NumeroProcessoSusep { get; set; }
        public int PlanoFipSusep { get; set; }
        public int TipoProvisoes { get; set; }
        public bool PermiteResgateParcial { get; set; }
    }
}
