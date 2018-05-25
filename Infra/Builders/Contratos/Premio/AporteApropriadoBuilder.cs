using System;
using System.Linq;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Testes.Infra.Builders;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio
{
    public class AporteApropriadoBuilder: MockBuilder<IAporteApropriado>
    {
        public static AporteApropriadoBuilder UmBuilder()
        {
            return new AporteApropriadoBuilder();
        }

        public AporteApropriadoBuilder ComIdentificador(Guid identificador)
        {
            Mock.SetupGet(x => x.Identificador).Returns(identificador);
            return this;
        }

        public AporteApropriadoBuilder ComIdentificadorCorrelacao(string idCorrelacao)
        {
            Mock.SetupGet(x => x.IdentificadorCorrelacao).Returns(idCorrelacao);
            return this;
        }

        public AporteApropriadoBuilder ComIdentificadorNegocio(string idNegocio)
        {
            Mock.SetupGet(x => x.IdentificadorNegocio).Returns(idNegocio);
            return this;
        }

        public AporteApropriadoBuilder ComDataExecucaoEvento(DateTime dataExecucao)
        {
            Mock.SetupGet(x => x.DataExecucaoEvento).Returns(dataExecucao);
            return this;
        }

        public AporteApropriadoBuilder Com(params AporteBuilder[] aporte)
        {
            Mock.SetupGet(x => x.Aportes).Returns(aporte.Select(a => a.Build()).ToList());
            return this;
        }

        public AporteApropriadoBuilder Padrao()
        {
            ComIdentificador(IdentificadoresPadrao.Identificador);
            ComIdentificadorNegocio(IdentificadoresPadrao.IdentificadorNegocio);
            ComIdentificadorCorrelacao(IdentificadoresPadrao.IdentificadorCorrelacao);
            ComDataExecucaoEvento(IdentificadoresPadrao.DataExecucaoEvento);
            Com(AporteBuilder.UmBuilder().Padrao()
                .Com(PagamentoBuilder.UmBuilder().Padrao()));

            return this;
        }
    }
}
