using System;
using System.Linq;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Testes.Infra.Builders;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio
{
    public class ParcelaDesapropriadaBuilder: MockBuilder<IParcelaDesapropriada>
    {
        public static ParcelaDesapropriadaBuilder UmBuilder()
        {
            return new ParcelaDesapropriadaBuilder();
        }
        public ParcelaDesapropriadaBuilder ComIdentificador(Guid identificador)
        {
            Mock.SetupGet(x => x.Identificador).Returns(identificador);
            return this;
        }

        public ParcelaDesapropriadaBuilder ComIdentificadorCorrelacaoExterna(string identificador)
        {
            Mock.SetupGet(x => x.IdentificadorCorrelacao).Returns(identificador);
            return this;
        }

        public ParcelaDesapropriadaBuilder ComIdentificadorNegocio(string identificador)
        {
            Mock.SetupGet(x => x.IdentificadorNegocio).Returns(identificador);
            return this;
        }

        public ParcelaDesapropriadaBuilder ComDataExecucaoEvento(DateTime data)
        {
            Mock.SetupGet(x => x.DataExecucaoEvento).Returns(data);
            return this;
        } 
        public ParcelaDesapropriadaBuilder Com(params ApropriacaoBuilder[] parcela)
        {            
            Mock.SetupGet(x => x.Parcelas).Returns(parcela.Select(p => p.Build()).ToList());
            return this;
        }

        public ParcelaDesapropriadaBuilder Padrao()
        {
            ComIdentificador(IdentificadoresPadrao.Identificador);
            ComIdentificadorCorrelacaoExterna(IdentificadoresPadrao.IdentificadorCorrelacao);
            ComIdentificadorNegocio(IdentificadoresPadrao.IdentificadorNegocio);
            ComDataExecucaoEvento(IdentificadoresPadrao.DataExecucaoEvento);
            Com(ApropriacaoBuilder.UmBuilder().Padrao().Com(PagamentoBuilder.UmBuilder().Padrao()));
            return this;
        }
    }      
}
