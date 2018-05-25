using Mongeral.Infrastructure.Assertions;
using Mongeral.Integrador.Contratos.Premio;
using System;

namespace Mongeral.Provisao.V2.Service.Premio.Validacao
{
    public static class PagamentoExtension
    {
        public static void Validar(this IPagamento pagamento, string itemCertificadoApolice)
        {
            var dataPagamento = Assertion.GreaterThan(pagamento.DataPagamento, default(DateTime), $"A Data de Pagamento para o ItemCertificadoApolice: { itemCertificadoApolice }, não foi informada.");
            var dataApropriacao = Assertion.GreaterThan(pagamento.DataApropriacao, default(DateTime), $"A Data de Apropriação para o ItemCertificadoApolice: { itemCertificadoApolice },  não foi informada.");
            var valorPago = Assertion.GreaterThan(pagamento.ValorPago, default(decimal), $"O Valor Pago para o ItemCertificadoApolice: { itemCertificadoApolice }, não foi informado.");

            dataPagamento.and(dataApropriacao).and(valorPago).Validate();
        }
    }
}
