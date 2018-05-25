using System;
using System.Linq;
using Mongeral.Integrador.Contratos.VG;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Implantacao;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.VG
{
    public class InclusaoVGBuilder: MockBuilder<IInclusaoCoberturaGrupal>
    {
        public static InclusaoVGBuilder UmaInclusao() { return new InclusaoVGBuilder(); }

        public InclusaoVGBuilder Com(params ProdutoBuilder[] produtos)
        {
            Mock.SetupGet(x => x.Produtos).Returns(produtos.Select(p => p.Build()).ToList());
            return this;
        }

        public InclusaoVGBuilder Com(ProponenteBuider proponente)
        {
            Mock.SetupGet(x => x.Proponente).Returns(proponente.Build());
            return this;
        }

        public InclusaoVGBuilder Com(DadosPagamentoBuilder pagamento)
        {
            Mock.SetupGet(x => x.DadosPagamento).Returns(pagamento.Build());
            return this;
        }

        public InclusaoVGBuilder ComIdentificador(Guid identificador)
        {
            Mock.SetupGet(x => x.Identificador).Returns(identificador);
            return this;
        }

        public InclusaoVGBuilder ComIdentificadorNegocio(string identificadorNegocio)
        {
            Mock.SetupGet(x => x.IdentificadorNegocio).Returns(identificadorNegocio);
            return this;
        }

        public InclusaoVGBuilder ComDataExecucao(DateTime dataExecucaoEvento)
        {
            Mock.SetupGet(x => x.DataExecucaoEvento).Returns(dataExecucaoEvento);
            return this;
        }

        public InclusaoVGBuilder ComDataAssinatura(DateTime dataAssinatura)
        {
            Mock.SetupGet(x => x.DataAssinatura).Returns(dataAssinatura);
            return this;
        }

        public InclusaoVGBuilder Padrao()
        {
            Mock.SetupGet(x => x.Identificador).Returns(IdentificadoresPadrao.Identificador);
            Mock.SetupGet(x => x.IdentificadorNegocio).Returns(IdentificadoresPadrao.IdentificadorNegocio);
            Mock.SetupGet(x => x.DataExecucaoEvento).Returns(IdentificadoresPadrao.DataExecucaoEvento);
            Mock.SetupGet(x => x.DataAssinatura).Returns(IdentificadoresPadrao.DataAssinatura);
            return this;
        }

        public InclusaoVGBuilder AdicionarMatricula(long matricula)
        {
            Mock.SetupGet(p => p.Proponente.Conjuge.Matricula).Returns(matricula);
            return this;
        }
    }
}
