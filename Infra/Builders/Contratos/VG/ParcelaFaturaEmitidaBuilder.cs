using Mongeral.Integrador.Contratos.VG.Eventos;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio;
using System;
using System.Linq;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.VG
{
    public class ParcelaFaturaEmitidaBuilder : MockBuilder<IParcelaFaturaEmitida>
    {
        public static ParcelaFaturaEmitidaBuilder UmBuilder()
        {
            return new ParcelaFaturaEmitidaBuilder();
        }

        public ParcelaFaturaEmitidaBuilder ComIdentificador(Guid identificador)
        {
            Mock.SetupGet(x => x.Identificador).Returns(identificador);
            return this;
        }

        public ParcelaFaturaEmitidaBuilder ComIdentificadorCorrelacaoExterna(string identificador)
        {
            Mock.SetupGet(x => x.IdentificadorCorrelacao).Returns(identificador);
            return this;
        }

        public ParcelaFaturaEmitidaBuilder ComIdentificadorNegocio(string identificador)
        {
            Mock.SetupGet(x => x.IdentificadorNegocio).Returns(identificador);
            return this;
        }

        public ParcelaFaturaEmitidaBuilder ComDataExecucaoEvento(DateTime data)
        {
            Mock.SetupGet(x => x.DataExecucaoEvento).Returns(data);
            return this;
        }

        public ParcelaFaturaEmitidaBuilder Com(params ParcelaBuilder[] parcela)
        {
            Mock.SetupGet(x => x.Parcelas).Returns(parcela.Select(p => p.Build()).ToList());
            return this;
        }

        public ParcelaFaturaEmitidaBuilder Padrao()
        {
            ComIdentificador(IdentificadoresPadrao.Identificador);
            ComIdentificadorNegocio(IdentificadoresPadrao.IdentificadorNegocio);
            ComDataExecucaoEvento(IdentificadoresPadrao.DataExecucaoEvento);
            Com(ParcelaBuilder.UmBuilder().Padrao());
            return this;
        }
    }
}
