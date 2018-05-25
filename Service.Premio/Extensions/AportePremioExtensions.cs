using System;
using System.Collections.Generic;
using System.Linq;
using Mongeral.Infrastructure.Assertions;
using Mongeral.Infrastructure.Extensions;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Domain.Entidades.EventosOperacional;
using Mongeral.Provisao.V2.Domain.Entidades.Premios;

namespace Mongeral.Provisao.V2.Service.Premio.Extensions
{
    public static class AportePremioExtensions
    {
        public static void ValidarAporte(this IAporteApropriado evento)
        {
            evento.Validar();

            Assertion.NotNull(evento.Aportes, "Nenhum aporte informado.").Validate();

            evento.Aportes.Select(a => a.ValidarPremioAporte()).Aggregate(Assertion.Neutral(), (x, y) => x.and(y)).Validate();
        }

        public static IAssertion ValidarPremioAporte(this IAporte premio)
        {
            var premioValidator = premio.ValidarPremio();

            var itemCertificadoApolice = premio.ParcelaId.IdentificadorExternoCobertura;

            var dataPagamentoValidator = Assertion.GreaterThan(premio.Pagamento.DataPagamento, default(DateTime), $"A Data de Pagamento para o ItemCertificadoApolice: { itemCertificadoApolice }, não foi informada.");
            var dataApropriacaoValidator = Assertion.GreaterThan(premio.Pagamento.DataApropriacao, default(DateTime), $"A Data de Portabilidade para o ItemCertificadoApolice: { itemCertificadoApolice },  não foi informada.");
            var valorPagoValidator = Assertion.GreaterThan(premio.Pagamento.ValorPago, default(decimal), $"O Valor Pago para o ItemCertificadoApolice: { itemCertificadoApolice }, não foi informado.");

            return premioValidator
                .and(dataPagamentoValidator)
                .and(dataApropriacaoValidator)
                .and(valorPagoValidator);
        }

        public static IEnumerable<PremioAporte> ToEventoAporte(this IAporteApropriado contrato)
        {
            var evento = new EventoAportePremio(contrato.Identificador, contrato.IdentificadorCorrelacao, contrato.IdentificadorNegocio, contrato.DataExecucaoEvento);

            contrato.Aportes.SafeForEach(premio => evento.AdicionarPremio(premio.ToPremioAporte()));

            return evento.Premios;
        }

        private static PremioAporte ToPremioAporte(this IAporte premioApropriado)
        {
            var premio = premioApropriado.ToPremio<PremioAporte>();

            premio.Pagamento = premioApropriado.Pagamento.ToPagamentoPremio();
            premio.Numero = 0;

            return premio;
        }
    }
}
