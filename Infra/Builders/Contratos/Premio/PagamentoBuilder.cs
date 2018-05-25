using System;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Testes.Infra.Builders;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio
{
    public class PagamentoBuilder: MockBuilder<IPagamento>
    {
        public static PagamentoBuilder UmBuilder()
        {
            return new PagamentoBuilder();
        }

        public PagamentoBuilder ComDataPagamento(DateTime data)
        {
            Mock.SetupGet(x => x.DataPagamento).Returns(data);
            return this;
        }

        public PagamentoBuilder ComDataApropriacao(DateTime data)
        {
            Mock.SetupGet(x => x.DataApropriacao).Returns(data);
            return this;
        }

        public PagamentoBuilder ComValorPago(decimal valor)
        {
            Mock.SetupGet(x => x.ValorPago).Returns(valor);
            return this;
        }

        public PagamentoBuilder ComDesconto(decimal desconto)
        {
            Mock.SetupGet(x => x.Desconto).Returns(desconto);
            return this;
        }

        public PagamentoBuilder ComMulta(decimal multa)
        {
            Mock.SetupGet(x => x.Multa).Returns(multa);
            return this;
        }

        public PagamentoBuilder ComIOFRetido(decimal iof)
        {
            Mock.SetupGet(x => x.IOFRetido).Returns(iof);
            return this;
        }

        public PagamentoBuilder ComIOFARecolher(decimal iof)
        {
            Mock.SetupGet(x => x.IOFARecolher).Returns(iof);
            return this;
        }

        public PagamentoBuilder ComIdentificadorCredito(string identificador)
        {
            Mock.SetupGet(x => x.IdentificadorCredito).Returns(identificador);
            return this;
        }

        public PagamentoBuilder Padrao()
        {
            ComDataPagamento(IdentificadoresPadrao.DataPagamento);
            ComDataApropriacao(IdentificadoresPadrao.DataApropriacao);
            ComValorPago(IdentificadoresPadrao.ValorPago);
            ComDesconto(IdentificadoresPadrao.Desconto);
            ComMulta(IdentificadoresPadrao.Multa);
            ComIOFRetido(IdentificadoresPadrao.IOFRetido);
            ComIOFARecolher(IdentificadoresPadrao.IOFARecolher);
            ComIdentificadorCredito(IdentificadoresPadrao.IdentificadorCredito);

            return this;
        }
    }
}
