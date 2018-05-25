using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Testes.Infra.Builders;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio
{
    public class ValorBuilder : MockBuilder<IValores>
    {
        public static ValorBuilder UmBuilder()
        {
            return new ValorBuilder();
        }

        public ValorBuilder ComValorContribuicao(decimal valorContribuicao)
        {
            Mock.SetupGet(x => x.Contribuicao).Returns(valorContribuicao);
            return this;
        }

        public ValorBuilder ComValorIOF(decimal valorIOF)
        {
            Mock.SetupGet(x => x.IOF).Returns(valorIOF);
            return this;
        }
        public ValorBuilder ComValorCarregamento(decimal valorCarregamento)
        {
            Mock.SetupGet(x => x.Carregamento).Returns(valorCarregamento);
            return this;
        }

        public ValorBuilder ComValorProLaboreDescontado(decimal valorProLaboreDescontado)
        {
            Mock.SetupGet(x => x.ProLaboreDescontado).Returns(valorProLaboreDescontado);
            return this;
        }

        public ValorBuilder ComValorBeneficio(decimal valorBeneficio)
        {
            Mock.SetupGet(x => x.Beneficio).Returns(valorBeneficio);
            return this;
        }

        public ValorBuilder ComValorCapitalSegurado(decimal capitalSegurado)
        {
            Mock.SetupGet(x => x.CapitalSegurado).Returns(capitalSegurado);
            return this;
        }

        public ValorBuilder Padrao()
        {
            ComValorContribuicao(IdentificadoresPadrao.ValorContribuicao);
            ComValorIOF(IdentificadoresPadrao.ValorIOF);
            ComValorCarregamento(IdentificadoresPadrao.ValorCarregamento);
            ComValorProLaboreDescontado(IdentificadoresPadrao.ValorProlaboreDescontado);
            ComValorBeneficio(IdentificadoresPadrao.ValorBeneficio);            
            ComValorCapitalSegurado(IdentificadoresPadrao.ValorCapitalSegurado);
            return this;
        }
    }
}
