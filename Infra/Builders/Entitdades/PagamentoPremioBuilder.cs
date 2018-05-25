using System;
using Mongeral.Provisao.V2.DTO;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Entitdades
{
    public class PagamentoPremioBuilder: BaseBuilder<PagamentoPremio>
    {
        private PagamentoPremioBuilder()
        {
            Instance = new PagamentoPremio();
        }

        public static PagamentoPremioBuilder Um()
        {
            return new PagamentoPremioBuilder();
        }

        public PagamentoPremioBuilder ComDataPagamento(DateTime dataPagamento)
        {
            Instance.DataPagamento = dataPagamento;
            return this;
        }

        private PagamentoPremioBuilder ComDataApropriacao(DateTime dataApropriacao)
        {
            Instance.DataApropriacao = dataApropriacao;
            return this;
        }

        public PagamentoPremioBuilder ComValorPago(decimal valor)
        {
            Instance.ValorPago = valor;
            return this;
        }

        public PagamentoPremioBuilder ComDesconto(decimal desconto)
        {
            Instance.Desconto = desconto;
            return this;
        }

        public PagamentoPremioBuilder ComMulta(decimal multa)
        {
            Instance.Multa = multa;
            return this;
        }

        public PagamentoPremioBuilder ComIOFRetido(decimal iof)
        {
            Instance.IOFRetido = iof;
            return this;
        }

        public PagamentoPremioBuilder ComIOFARecolher(decimal iof)
        {
            Instance.IOFARecolher = iof;
            return this;
        }

        public PagamentoPremioBuilder ComIdentificadorCredito(string identificador)
        {
            Instance.IdentificadorCredito = identificador;
            return this;
        }

        public PagamentoPremioBuilder Padrao()
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
