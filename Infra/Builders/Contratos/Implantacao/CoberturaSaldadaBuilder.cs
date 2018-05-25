using Mongeral.Integrador.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Implantacao
{
    public class CoberturaSaldadaBuilder : MockBuilder<ICoberturaSaldada>
    {
        public static CoberturaSaldadaBuilder UmaCobertura()
        {
            return new CoberturaSaldadaBuilder();
        }

        public CoberturaSaldadaBuilder ComIdentificador(Guid identificador)
        {
            Mock.SetupGet(x => x.Identificador).Returns(identificador);
            return this;
        }

        public CoberturaSaldadaBuilder ComIdentificadorCorrelacaoExterna(string identificador)
        {
            Mock.SetupGet(x => x.IdentificadorCorrelacao).Returns(identificador);
            return this;
        }

        public CoberturaSaldadaBuilder ComIdentificadorNegocio(string identificador)
        {
            Mock.SetupGet(x => x.IdentificadorNegocio).Returns(identificador);
            return this;
        }

        public CoberturaSaldadaBuilder ComDataExecucaoEvento(DateTime data)
        {
            Mock.SetupGet(x => x.DataExecucaoEvento).Returns(data);
            return this;
        }
        public CoberturaSaldadaBuilder ComIdentificadorExternoCobertura(string IdentificadorExternoCobertura)
        {
            Mock.SetupGet(x => x.IdentificadorExternoCobertura).Returns(IdentificadorExternoCobertura);
            return this;
        }
        public CoberturaSaldadaBuilder ComValorBeneficio(decimal valorBeneficio)
        {
            Mock.SetupGet(x => x.ValorBeneficio).Returns(valorBeneficio);
            return this;
        }

        public CoberturaSaldadaBuilder ComValorCapitalSegurado(decimal valorCapitalSegurado)
        {
            Mock.SetupGet(x => x.ValorCapitalSegurado).Returns(valorCapitalSegurado);
            return this;
        }

        public CoberturaSaldadaBuilder Padrao()
        {
            ComIdentificadorExternoCobertura(IdentificadoresPadrao.ItemCertificadoApoliceId.ToString());
            ComValorBeneficio(IdentificadoresPadrao.ValorBeneficio);
            ComValorCapitalSegurado(IdentificadoresPadrao.ValorCapitalSegurado);
            return this;
        }
    }
}
