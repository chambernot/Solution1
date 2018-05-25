using Mongeral.Integrador.Contratos.VG.Eventos;
using System;
using System.Linq;
namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio
{
    public class ParcelaCanceladaBuilder : MockBuilder<IParcelaFaturaCancelada>
    {
        public static ParcelaCanceladaBuilder UmBuilder()
        {
            return new ParcelaCanceladaBuilder();
        }
        public ParcelaCanceladaBuilder ComIdentificador(Guid identificador)
        {
            Mock.SetupGet(x => x.Identificador).Returns(identificador);
            return this;
        }
        public ParcelaCanceladaBuilder ComIdentificadorCorrelacaoExterna(string identificador)
        {
            Mock.SetupGet(x => x.IdentificadorCorrelacao).Returns(identificador);
            return this;
        }
        public ParcelaCanceladaBuilder ComIdentificadorNegocio(string identificador)
        {
            Mock.SetupGet(x => x.IdentificadorNegocio).Returns(identificador);
            return this;
        }
        public ParcelaCanceladaBuilder ComDataExecucaoEvento(DateTime data)
        {
            Mock.SetupGet(x => x.DataExecucaoEvento).Returns(data);
            return this;
        }
        public ParcelaCanceladaBuilder Com(params ParcelaBuilder[] parcela)
        {
            Mock.SetupGet(x => x.Parcelas).Returns(parcela.Select(p => p.Build()).ToList());
            return this;
        }
        public ParcelaCanceladaBuilder Padrao()
        {
            ComIdentificador(IdentificadoresPadrao.Identificador);
            ComIdentificadorCorrelacaoExterna(IdentificadoresPadrao.IdentificadorCorrelacao);
            ComIdentificadorNegocio(IdentificadoresPadrao.IdentificadorNegocio);
            ComDataExecucaoEvento(IdentificadoresPadrao.DataExecucaoEvento);
            Com(ParcelaBuilder.UmBuilder().Padrao().Com(ParcelaIdBuilder.UmBuilder().Padrao()));
            return this;
        }
    }
}
