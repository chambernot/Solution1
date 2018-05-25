using System;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Testes.Infra.Builders;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio
{
    public class ProvisaoBuilder: MockBuilder<IProvisao>
    {
        public static ProvisaoBuilder UmBuilder()
        {
            return new ProvisaoBuilder();
        }

        public ProvisaoBuilder ComProvisaoId(short provisaoId)
        {
            Mock.SetupGet(x => x.ProvisaoId).Returns(provisaoId);
            return this;
        }

        public ProvisaoBuilder ComDataMovimento(DateTime dataMovimento)
        {
            Mock.SetupGet(x => x.DataMovimentacao).Returns(dataMovimento);
            return this;
        }

        public ProvisaoBuilder ComValor(decimal valor)
        {
            Mock.SetupGet(x => x.Valor).Returns(valor);
            return this;
        }

        public ProvisaoBuilder ComAtualizacaoMonetaria(decimal atualizacaoMonetaria)
        {
            Mock.SetupGet(x => x.AtualizacaoMonetaria).Returns(atualizacaoMonetaria);
            return this;
        }

        public ProvisaoBuilder ComJuros(decimal juros)
        {
            Mock.SetupGet(x => x.Juros).Returns(juros);
            return this;
        }

        public ProvisaoBuilder ComCodigoPortabilidadeInterna(string codigoPortInterna)
        {
            Mock.SetupGet(x => x.CodigoPortabilidadeInterna).Returns(codigoPortInterna);
            return this;
        }

        public ProvisaoBuilder ComValorUltimaContribuicao(decimal valorUltimaContribuicao)
        {
            Mock.SetupGet(x => x.ValorUltimaContribuicao).Returns(valorUltimaContribuicao);
            return this;
        }

        public ProvisaoBuilder ComNaturezaRenda(short naturezarenda)
        {
            Mock.SetupGet(x => x.NaturezaRenda).Returns(naturezarenda);
            return this;
        }

        public ProvisaoBuilder ComTabuaId(short tabuaId)
        {
            Mock.SetupGet(x => x.TabuaId).Returns(tabuaId);
            return this;
        }

        public ProvisaoBuilder ComPercentualTaxaJuros(decimal percentualTaxaJuros)
        {
            Mock.SetupGet(x => x.PercentualTaxaJuros).Returns(percentualTaxaJuros);
            return this;
        }

        public ProvisaoBuilder ComDataUltimaAtualizacaoContribuicao(DateTime dataUltimaAtualizacaoContribuicao)
        {
            Mock.SetupGet(x => x.DataUltimaAtualizacaoContribuicao).Returns(dataUltimaAtualizacaoContribuicao);
            return this;
        }

        public ProvisaoBuilder ComIndiceId(short indiceId)
        {
            Mock.SetupGet(x => x.IndiceId).Returns(indiceId);
            return this;
        }

        public ProvisaoBuilder ComValorSobrevivencia(decimal valorSobrevivencia)
        {
            Mock.SetupGet(x => x.ValorSobrevivencia).Returns(valorSobrevivencia);
            return this;
        }

        public ProvisaoBuilder Padrao()
        {
            ComProvisaoId(IdentificadoresPadrao.ProvisaoId);
            ComDataMovimento(IdentificadoresPadrao.DataMovimentacao);
            ComValor(IdentificadoresPadrao.ValorProvisao);
            ComAtualizacaoMonetaria(IdentificadoresPadrao.ValorAtualizacao);
            ComJuros(IdentificadoresPadrao.ValorJuros);
            ComCodigoPortabilidadeInterna(IdentificadoresPadrao.CodigoPortabilidadeInterna);
            ComValorUltimaContribuicao(IdentificadoresPadrao.ValorAtualizadoUltimaContribuicao);
            ComNaturezaRenda(IdentificadoresPadrao.NaturezaRenda);
            ComTabuaId(IdentificadoresPadrao.TabuaId);
            ComPercentualTaxaJuros(IdentificadoresPadrao.PercentualTaxaJuros);
            ComDataUltimaAtualizacaoContribuicao(IdentificadoresPadrao.DataUltimaAtualizacaoContribuicao);
            ComIndiceId(IdentificadoresPadrao.IndiceId);
            ComValorSobrevivencia(IdentificadoresPadrao.ValorSobrevivencia);
            return this;
        }

    }
}
