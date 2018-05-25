using System;
using System.Linq;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Testes.Infra.Builders;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio
{
    public class ParcelaAjustadaBuilder: MockBuilder<IParcelaAjustada>
    {
        public static ParcelaAjustadaBuilder UmBuilder()
        {
            return new ParcelaAjustadaBuilder();
        }

        public ParcelaAjustadaBuilder ComIdentificador(Guid identificador)
        {
            Mock.SetupGet(x => x.Identificador).Returns(identificador);
            return this;
        }

        public ParcelaAjustadaBuilder ComIdCorrelacao(string identificador)
        {
            Mock.SetupGet(x => x.IdentificadorCorrelacao).Returns(identificador);
            return this;
        }

        public ParcelaAjustadaBuilder ComIdNegocio(string identificador)
        {
            Mock.SetupGet(x => x.IdentificadorNegocio).Returns(identificador);
            return this;
        }

        public ParcelaAjustadaBuilder ComDataExecucao(DateTime data)
        {
            Mock.SetupGet(x => x.DataExecucaoEvento).Returns(data);
            return this;
        }

        public ParcelaAjustadaBuilder Com(params ParcelaBuilder[] parcela)
        {
            Mock.SetupGet(x => x.Parcelas).Returns(parcela.Select(p => p.Build()).ToList());
            return this;
        }

        public ParcelaAjustadaBuilder Padrao()
        {
            ComIdentificador(IdentificadoresPadrao.Identificador);
            ComIdCorrelacao(IdentificadoresPadrao.IdentificadorCorrelacao);
            ComIdNegocio(IdentificadoresPadrao.IdentificadorNegocio);
            ComDataExecucao(IdentificadoresPadrao.DataExecucaoEvento);
            Com(ParcelaBuilder.UmBuilder().Padrao());
            return this;
        }
    }
}
