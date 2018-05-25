using Mongeral.Infrastructure.Domain.Model;
using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.Provisao.V2.DTO;
using Newtonsoft.Json;
using System;

namespace Mongeral.Provisao.V2.Domain
{
    public class MovimentoProvisao : Entidade<Guid>
    {
        public MovimentoProvisao() { }

        public Guid? EventoId => Premio?.EventoId == null ? EventoIdCobertura  : Premio.EventoId;
        public Guid? EventoIdCobertura { get; set; }
        public ProvisaoCobertura _provisaoCobertura { get; protected set; }
        public Guid? ProvisaoCoberturaId => _provisaoCobertura?.Id;
        public Premio Premio { get; protected set; }
        public Guid? PremioId => Premio?.Id;
        public DateTime DataMovimentacao { get; set; }
        public int QuantidadeContribuicoes { get; set; }
        public decimal Fator { get; set; }
        public decimal PercentualCarregamento { get; set; }
        public decimal ValorBeneficioCorrigido { get; set; }
        public decimal? ValorJuros { get; set; }
        public decimal? ValorAtualizacao { get; set; }
        public decimal? ValorSobrevivencia { get; set; }
        public decimal ValorProvisao { get; set; }
        public decimal ValorDesvio { get; set; }
        public decimal ValorFIF { get; set; }
        

        public short ProvisaoId { get; set; }
        public string CodigoPortabilidadeInterna { get; set; }
        public short? NaturezaRenda { get; set; }
        public short? TabuaId { get; set; }
        public decimal? PercentualTaxaJuros { get; set; }
        public DateTime? DataUltimaAtualizacaoContribuicao { get; set; }
        public short? IndiceId { get; set; }
        public decimal? ValorUltimaContribuicao { get; set; }

        public MovimentoProvisao ComProvisao(ProvisaoDto provisao)
        {
            ProvisaoId = provisao.ProvisaoId;
            DataMovimentacao = provisao.DataMovimentacao;
            ValorProvisao = provisao.Valor;
            ValorAtualizacao = provisao.AtualizacaoMonetaria;
            ValorJuros = provisao.Juros;
            PercentualTaxaJuros = provisao.PercentualTaxaJuros;
            ValorUltimaContribuicao = provisao.ValorUltimaContribuicao;
            DataUltimaAtualizacaoContribuicao = provisao.DataUltimaAtualizacaoContribuicao;
            IndiceId = provisao.IndiceId.GetValueOrDefault();
            ValorSobrevivencia = provisao.ValorSobrevivencia.GetValueOrDefault();
            NaturezaRenda = provisao.NaturezaRenda;
            TabuaId = provisao.TabuaId;
            CodigoPortabilidadeInterna = provisao.CodigoPortabilidadeInterna;

            if (provisao.ProvisaoId == (int)TipoProvisaoEnum.PMBAC)
            {
                var pmbac = JsonConvert.DeserializeObject<ProvisaoMatematicaBeneficioAConceder>(JsonConvert.SerializeObject(provisao));

                Fator = pmbac.Fator;
                ValorDesvio = pmbac.Desvio;
                ValorBeneficioCorrigido = pmbac.ValorBeneficioCorrigido;
            }

            return this;
        }

        public MovimentoProvisao ComQuantidadeContribuicoes(int contribuicoes)
        {
            QuantidadeContribuicoes = contribuicoes;
            return this;
        }
            

        public void AdicionarProvisaoCobertura(ProvisaoCobertura provisaoCobertura)
        {
            _provisaoCobertura = provisaoCobertura;
            
        }

        public void AdicionarPremio(Premio premio)
        {
            Premio = premio;
        }
    }
}
