using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.Provisao.V2.DTO;
using System;
namespace Mongeral.Provisao.V2.Testes.Infra.Builders
{
    public class ProvisaoDtoBuilder : BaseBuilder<ProvisaoDto>
    {
        private ProvisaoDtoBuilder() { Instance = new ProvisaoDto(); }
        public static ProvisaoDtoBuilder UmBuilder()
        {
            return new ProvisaoDtoBuilder();
        }
        public ProvisaoDtoBuilder ComDataMovimentacao(DateTime dataMovimentacao)
        {
            Instance.DataMovimentacao = dataMovimentacao;
            return this;
        }
        public ProvisaoDtoBuilder ComAtualizacaoMonetaria(decimal atualizacaoMonitaria)
        {
            Instance.AtualizacaoMonetaria = atualizacaoMonitaria;
            return this;
        }
        public ProvisaoDtoBuilder ComJuros(decimal juros)
        {
            Instance.Juros = juros;
            return this;
        }
        public ProvisaoDtoBuilder ComCodigoPortabilidadeInterna(string codigoPortabilidadeInterna)
        {
            Instance.CodigoPortabilidadeInterna = codigoPortabilidadeInterna;
            return this;
        }
        public ProvisaoDtoBuilder ComNaturezaRenda(short naturezaRenda)
        {
            Instance.NaturezaRenda = naturezaRenda;
            return this;
        }
        public ProvisaoDtoBuilder ComTabuaId(short tabuaId)
        {
            Instance.TabuaId = tabuaId;
            return this;
        }
        public ProvisaoDtoBuilder ComPercentualTaxaJuros(decimal percentualTaxaJuros)
        {
            Instance.PercentualTaxaJuros = percentualTaxaJuros;
            return this;
        }
        public ProvisaoDtoBuilder ComDataUltimaAtualizacaoContribuicao(DateTime dataUltimaAtualizacao)
        {
            Instance.DataUltimaAtualizacaoContribuicao = dataUltimaAtualizacao;
            return this;
        }
        public ProvisaoDtoBuilder ComIndiceId(short indice)
        {
            Instance.IndiceId = indice;
            return this;
        }
        public ProvisaoDtoBuilder ComValor(decimal valor)
        {
            Instance.Valor = valor;
            return this;
        }
        public ProvisaoDtoBuilder ComValorSobrevivencia(decimal valorSobrevivencia)
        {
            Instance.ValorSobrevivencia = valorSobrevivencia;
            return this;
        }
        public ProvisaoDtoBuilder ComValorUltimaContribuicao(decimal valorUltimaContribuicao)
        {
            Instance.ValorUltimaContribuicao = valorUltimaContribuicao;
            return this;
        }
        public ProvisaoDtoBuilder ComTipoProvisao(TipoProvisaoEnum tipoProvisao)
        {
            Instance.ProvisaoId = (short)tipoProvisao;
            return this;
        }
        public ProvisaoDtoBuilder Padrao()
        {
            ComTipoProvisao(TipoProvisaoEnum.PMBAC);
            ComDataMovimentacao(IdentificadoresPadrao.DataMovimentacao);
            ComAtualizacaoMonetaria(IdentificadoresPadrao.ValorAtualizacao);
            ComJuros(IdentificadoresPadrao.ValorJuros);
            ComCodigoPortabilidadeInterna(IdentificadoresPadrao.CodigoPortabilidadeInterna);
            ComNaturezaRenda(IdentificadoresPadrao.NaturezaRenda);
            ComTabuaId(IdentificadoresPadrao.TabuaId);
            ComPercentualTaxaJuros(IdentificadoresPadrao.PercentualTaxaJuros);
            ComDataUltimaAtualizacaoContribuicao(IdentificadoresPadrao.DataUltimaAtualizacaoContribuicao);
            ComIndiceId(IdentificadoresPadrao.IndiceId);
            ComValor(IdentificadoresPadrao.ValorProvisao);
            ComValorSobrevivencia(IdentificadoresPadrao.ValorSobrevivencia);
            ComValorUltimaContribuicao(IdentificadoresPadrao.ValorContribuicao);
            return this;
        }
    }
}
