using System;
using System.Linq;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Testes.Infra.Builders;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio
{
    public class PortabilidadeApropriadaBuilder : MockBuilder<IPortabilidadeApropriada>
    {
        public static PortabilidadeApropriadaBuilder UmBuilder()
        {
            return new PortabilidadeApropriadaBuilder();
        }

        public PortabilidadeApropriadaBuilder ComIdentificador(Guid identificador)
        {
            Mock.SetupGet(x => x.Identificador).Returns(identificador);
            return this;
        }

        public PortabilidadeApropriadaBuilder ComIdentificadorCorrelacao(string idCorrelacao)
        {
            Mock.SetupGet(x => x.IdentificadorCorrelacao).Returns(idCorrelacao);
            return this;
        }

        public PortabilidadeApropriadaBuilder ComIdentificadorNegocio(string idNegocio)
        {
            Mock.SetupGet(x => x.IdentificadorNegocio).Returns(idNegocio);
            return this;
        }

        public PortabilidadeApropriadaBuilder ComDataExecucaoEvento(DateTime dataExecucao)
        {
            Mock.SetupGet(x => x.DataExecucaoEvento).Returns(dataExecucao);
            return this;
        }

        public PortabilidadeApropriadaBuilder Com(params PortabilidadeBuilder[] portabilidade)
        {
            Mock.SetupGet(x => x.Portabilidades).Returns(portabilidade.Select(p => p.Build()).ToList());
            return this;
        }

        public PortabilidadeApropriadaBuilder Padrao()
        {
            ComIdentificador(IdentificadoresPadrao.Identificador);
            ComIdentificadorNegocio(IdentificadoresPadrao.IdentificadorNegocio);
            ComIdentificadorCorrelacao(IdentificadoresPadrao.IdentificadorCorrelacao);
            ComDataExecucaoEvento(IdentificadoresPadrao.DataExecucaoEvento);
            Com(PortabilidadeBuilder.UmBuilder().Padrao()
                .Com(PagamentoBuilder.UmBuilder().Padrao()));
            return this;
        }
    }
}
