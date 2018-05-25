using System;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Testes.Infra.Builders;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio
{
    public class VigenciaBuilder : MockBuilder<IVigencia>
    {
        public static VigenciaBuilder UmBuilder()
        {
            return new VigenciaBuilder();
        }

        public VigenciaBuilder ComCompetencia(DateTime competencia)
        {
            Mock.SetupGet(x => x.Competencia).Returns(competencia);
            return this;
        }

        public VigenciaBuilder ComDataInicio(DateTime inicio)
        {
            Mock.SetupGet(x => x.Inicio).Returns(inicio);
            return this;
        }

        public VigenciaBuilder ComDataFim(DateTime fim)
        {
            Mock.SetupGet(x => x.Fim).Returns(fim);
            return this;
        }

        public VigenciaBuilder Padrao()
        {
            ComCompetencia(IdentificadoresPadrao.Competencia);
            ComDataInicio(IdentificadoresPadrao.DataInicioVigencia);
            ComDataFim(IdentificadoresPadrao.DataFimVigencia);
            return this;
        }
    }
}
