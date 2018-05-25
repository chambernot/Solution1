using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.DTO;
using System;

namespace Mongeral.Provisao.V2.Service.Premio.Mapear
{
    public class ParcelaToPremio
    {
        public Domain.Premio ToPremio(IParcela parcela)
        {
            return new Domain.Premio()
            {
                ItemCertificadoApoliceId = long.Parse(parcela.ParcelaId.IdentificadorExternoCobertura),
                Numero = parcela.ParcelaId?.NumeroParcela?? 0,
                Competencia = parcela.Vigencia?.Competencia?? DateTime.MinValue,
                InicioVigencia = parcela.Vigencia?.Inicio?? DateTime.MinValue,
                FimVigencia = parcela.Vigencia?.Fim?? DateTime.MinValue,                
                ValorPremio = parcela.Valores?.Contribuicao?? 0.0M,
                ValorCarregamento = parcela.Valores?.Carregamento?? 0.0M,
                ValorBeneficio = parcela.Valores?.Beneficio?? 0.0M,
                ValorCapitalSegurado = parcela.Valores?.CapitalSegurado?? 0.0M
            };
        }

        public PagamentoPremio ToPagamentoPremio(IPagamento pagamento)
        {
            return new PagamentoPremio()
            {
                DataPagamento = pagamento.DataPagamento,
                DataApropriacao = pagamento.DataApropriacao,
                ValorPago = pagamento.ValorPago,
                Desconto = pagamento.Desconto,
                Multa = pagamento.Multa,
                IOFRetido = pagamento.IOFRetido,
                IOFARecolher = pagamento.IOFARecolher,
                IdentificadorCredito = pagamento.IdentificadorCredito
            };
        }
    }
}
