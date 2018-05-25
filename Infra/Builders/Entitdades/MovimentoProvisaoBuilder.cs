using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Enum;
using System;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Entitdades
{
    public class MovimentoProvisaoBuilder : BaseBuilder<MovimentoProvisao>
    {
        private MovimentoProvisaoBuilder() { Instance = new MovimentoProvisao(); }

        public static MovimentoProvisaoBuilder UmBuilder()
        {
            return new MovimentoProvisaoBuilder();
        }

        public MovimentoProvisaoBuilder ComDataMovimentacao(DateTime dataMovimentacao)
        {
            Instance.DataMovimentacao = dataMovimentacao;
            return this;
        }

        public MovimentoProvisaoBuilder ComAtualizacaoMonetaria(decimal atualizacaoMonetaria)
        {
            Instance.ValorAtualizacao = atualizacaoMonetaria;
            return this;
        }

        public MovimentoProvisaoBuilder ComJuros(decimal juros)
        {
            Instance.ValorJuros = juros;
            return this;
        }

        public MovimentoProvisaoBuilder ComCodigoPortabilidadeInterna(string codigoPortabilidadeInterna)
        {
            Instance.CodigoPortabilidadeInterna = codigoPortabilidadeInterna;
            return this;
        }

        public MovimentoProvisaoBuilder ComNaturezaRenda(short naturezaRenda)
        {
            Instance.NaturezaRenda = naturezaRenda;
            return this;
        }

        public MovimentoProvisaoBuilder ComTabuaId(short tabuaId)
        {
            Instance.TabuaId = tabuaId;
            return this;
        }

        public MovimentoProvisaoBuilder ComPercentualTaxaJuros(decimal percentualTaxaJuros)
        {
            Instance.PercentualTaxaJuros = percentualTaxaJuros;
            return this;
        }

        public MovimentoProvisaoBuilder ComDataUltimaAtualizacaoContribuicao(DateTime dataUltimaAtualizacao)
        {
            Instance.DataUltimaAtualizacaoContribuicao = dataUltimaAtualizacao;
            return this;
        }

        public MovimentoProvisaoBuilder ComIndiceId(short indice)
        {
            Instance.IndiceId = indice;
            return this;
        }

        public MovimentoProvisaoBuilder ComValor(decimal valor)
        {
            Instance.ValorProvisao = valor;
            return this;
        }

        public MovimentoProvisaoBuilder ComValorSobrevivencia(decimal valorSobrevivencia)
        {
            Instance.ValorSobrevivencia = valorSobrevivencia;
            return this;
        }

        public MovimentoProvisaoBuilder ComValorUltimaContribuicao(decimal valorUltimaContribuicao)
        {
            Instance.ValorUltimaContribuicao = valorUltimaContribuicao;
            return this;
        }

        public MovimentoProvisaoBuilder ComTipoProvisao(TipoProvisaoEnum tipoProvisao)
        {
            Instance.ProvisaoId = (short)tipoProvisao;
            return this;
        }

        public MovimentoProvisaoBuilder Com(ProvisaoCoberturaBuilder provisaoCobertura)
        {
            Instance.AdicionarProvisaoCobertura(provisaoCobertura.Build());
            return this;
        }

        public MovimentoProvisaoBuilder Padrao()
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
