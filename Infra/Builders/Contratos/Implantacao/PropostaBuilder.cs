using System;
using System.Linq;
using Mongeral.Integrador.Contratos;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Implantacao
{
    public class PropostaBuilder : MockBuilder<IProposta>
    {
        public static PropostaBuilder UmaProposta() { return new PropostaBuilder(); }

        public PropostaBuilder Com(params ProdutoBuilder[] produtos)
        {
            Mock.SetupGet(x => x.Produtos).Returns(produtos.Select(p => p.Build()).ToList());
            return this;
        }
        
        public PropostaBuilder Com(ProponenteBuider proponente)
        {
            Mock.SetupGet(x => x.Proponente).Returns(proponente.Build());
            return this;
        }

        public PropostaBuilder Com(DadosPagamentoBuilder pagamento)
        {
            Mock.SetupGet(x => x.DadosPagamento).Returns(pagamento.Build());
            return this;
        }

        public PropostaBuilder ComIdentificador(Guid identificador)
        {
            Mock.SetupGet(x => x.Identificador).Returns(identificador);
            return this;
        }

        public PropostaBuilder ComIdentificadorNegocio(string identificadorNegocio)
        {
            Mock.SetupGet(x => x.IdentificadorNegocio).Returns(identificadorNegocio);
            return this;
        }

        public PropostaBuilder ComDataExecucao(DateTime dataExecucaoEvento)
        {
            Mock.SetupGet(x => x.DataExecucaoEvento).Returns(dataExecucaoEvento);
            return this;
        }

        public PropostaBuilder ComDataAssinatura(DateTime dataAssinatura)
        {
            Mock.SetupGet(x => x.DataAssinatura).Returns(dataAssinatura);
            return this;
        }
        public PropostaBuilder ComDataImplantacao(DateTime data)
        {
            Mock.SetupGet(x => x.DataImplantacao).Returns(data);
            return this;
        }

        public PropostaBuilder ComNumeroProposta(long numero)
        {
            Mock.SetupGet(x => x.Numero).Returns(numero);
            return this;
        }

        public PropostaBuilder Padrao()
        {
            ComIdentificador(IdentificadoresPadrao.Identificador);
            ComIdentificadorNegocio(IdentificadoresPadrao.IdentificadorNegocio);
            ComDataExecucao(IdentificadoresPadrao.DataExecucaoEvento);
            ComDataAssinatura(IdentificadoresPadrao.DataAssinatura);
            ComDataImplantacao(IdentificadoresPadrao.DataImplantacao);
            ComNumeroProposta(IdentificadoresPadrao.NumeroProposta);
            return this;
        }

        public PropostaBuilder AdicionarMatricula(long matricula)
        {
            Mock.SetupGet(p => p.Proponente.Conjuge.Matricula).Returns(matricula);
            return this;
        }
    }
}
