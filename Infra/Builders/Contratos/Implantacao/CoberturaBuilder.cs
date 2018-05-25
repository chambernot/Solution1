using System;
using Mongeral.Integrador.Contratos;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Implantacao
{
    public class CoberturaBuilder : MockBuilder<ICobertura>
    {
        public static CoberturaBuilder UmaCobertura()
        {
            return new CoberturaBuilder();
        }
        public CoberturaBuilder Com(ContratacaoBuilder contratacao)
        {
            Mock.SetupGet(x => x.Contratacao).Returns(contratacao.Build());
            return this;
        }

        public CoberturaBuilder Com(PrazosBuilder prazo)
        {
            Mock.SetupGet(x => x.Prazos).Returns(prazo.Build());
            return this;
        }

        public CoberturaBuilder ComItemCertificadoApolice(long itemCertificadoApolice)
        {
            Mock.SetupGet(x => x.IdentificadorExterno).Returns(itemCertificadoApolice.ToString());
            return this;
        }

        public CoberturaBuilder ComItemProdutoId(int itemProdutoId)
        {
            Mock.SetupGet(x => x.CodigoItemProduto).Returns(itemProdutoId);
            return this;
        }

        public CoberturaBuilder ComInicioVigencia(DateTime data)
        {
            Mock.SetupGet(x => x.InicioVigencia).Returns(data);
            return this;
        }

        public CoberturaBuilder ComFimVigencia(DateTime data)
        {
            Mock.SetupGet(x => x.FimVigencia).Returns(data);
            return this;
        }

        public CoberturaBuilder ComValorBeneficio(decimal valor)
        {
            Mock.SetupGet(x => x.ValorBeneficio).Returns(valor);
            return this;
        }

        public CoberturaBuilder ComValorCapital(decimal valor)
        {
            Mock.SetupGet(x => x.ValorCapital).Returns(valor);
            return this;
        }

        public CoberturaBuilder ComValorContribuicao(decimal valor)
        {
            Mock.SetupGet(x => x.ValorContribuicao).Returns(valor);
            return this;
        }

        public CoberturaBuilder ComClasseRisco(int classeRiscoId)
        {
            Mock.SetupGet(x => x.ClasseRisco).Returns(classeRiscoId);
            return this;
        }

        public CoberturaBuilder Padrao()
        {
            ComItemCertificadoApolice(IdentificadoresPadrao.ItemCertificadoApoliceId);
            ComClasseRisco(IdentificadoresPadrao.ClasseRiscoId);
            ComItemProdutoId(IdentificadoresPadrao.ItemProdutoId);
            ComInicioVigencia(IdentificadoresPadrao.DataInicioVigencia);
            ComFimVigencia(IdentificadoresPadrao.DataFimVigencia);
            ComValorCapital(IdentificadoresPadrao.ValorCapital);
            ComValorContribuicao(IdentificadoresPadrao.ValorContribuicao);
            ComValorBeneficio(IdentificadoresPadrao.ValorBeneficio);
            return this;
        }
    }
}
