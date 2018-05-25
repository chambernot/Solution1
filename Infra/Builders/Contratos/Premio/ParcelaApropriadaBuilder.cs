using System;
using System.Linq;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Testes.Infra.Builders;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio
{
    public class ParcelaApropriadaBuilder: MockBuilder<IParcelaApropriada>
    {
        public static ParcelaApropriadaBuilder UmBuilder()
        {
            return new ParcelaApropriadaBuilder();
        }

        public ParcelaApropriadaBuilder ComIdentificador(Guid identificador)
        {
            Mock.SetupGet(x => x.Identificador).Returns(identificador);
            return this;
        }

        public ParcelaApropriadaBuilder ComIdentificadorCorrelacao(string idCorrelacao)
        {
            Mock.SetupGet(x => x.IdentificadorCorrelacao).Returns(idCorrelacao);
            return this;
        }

        public ParcelaApropriadaBuilder ComIdentificadorNegocio(string idNegocio)
        {
            Mock.SetupGet(x => x.IdentificadorNegocio).Returns(idNegocio);
            return this;
        }

        public ParcelaApropriadaBuilder ComDataExecucaoEvento(DateTime dataExecucao)
        {
            Mock.SetupGet(x => x.DataExecucaoEvento).Returns(dataExecucao);
            return this;
        }

        public ParcelaApropriadaBuilder Com(params ApropriacaoBuilder[] parcela)
        {
            Mock.SetupGet(x => x.Parcelas).Returns(parcela.Select(p => p.Build()).ToList());
            return this;
        }

        public ParcelaApropriadaBuilder Padrao()
        {
            ComIdentificador(IdentificadoresPadrao.Identificador);
            ComIdentificadorNegocio(IdentificadoresPadrao.IdentificadorNegocio);
            ComIdentificadorCorrelacao(IdentificadoresPadrao.IdentificadorCorrelacao);
            ComDataExecucaoEvento(IdentificadoresPadrao.DataExecucaoEvento);
            Com(ApropriacaoBuilder.UmBuilder().Padrao()
                .Com(PagamentoBuilder.UmBuilder().Padrao()));
            return this;
        }
    }
}
