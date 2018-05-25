using System;
using System.Collections.Generic;
using System.Linq;
using Mongeral.Infrastructure.Assertions;
using Mongeral.Infrastructure.Extensions;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Domain.Entidades;
using Mongeral.Provisao.V2.Domain.Entidades.EventosOperacional;

namespace Mongeral.Provisao.V2.Service.Premio.Extensions
{
    public static class PortabilidadePremioExtension
    {
        public static void ValidarPortabilidade(this IPortabilidadeApropriada evento)
        {
            evento.Validar();

            Assertion.NotNull(evento.Portabilidades, "Nenhum prêmio informado.").Validate();

            evento.Portabilidades.Select(p => p.ValidarPremioPortabilidade()).Aggregate(Assertion.Neutral(), (x, y) => x.and(y)).Validate();
        }

        public static IAssertion ValidarPremioPortabilidade(this IPortabilidade premio)
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

        public static IEnumerable<PremioPortabilidade> ToEventoPortabilidade(this IPortabilidadeApropriada contrato)
        {
            var evento = new EventoPortabilidadePremio(contrato.Identificador, contrato.IdentificadorCorrelacao, contrato.IdentificadorNegocio, contrato.DataExecucaoEvento);

            contrato.Portabilidades.SafeForEach(premio => evento.AdicionarPremio(premio.ToPremioPortabilidade()));

            return evento.Premios;
        }

        private static PremioPortabilidade ToPremioPortabilidade(this IPortabilidade premioApropriado)
        {
            var premio = premioApropriado.ToPremio<PremioPortabilidade>();

            premio.Pagamento = premioApropriado.Pagamento.ToPagamentoPremio();
            premio.CodigoSusep = premioApropriado.CodigoSusep;
            premio.Numero = 0;

            return premio;
        }
    }
}
