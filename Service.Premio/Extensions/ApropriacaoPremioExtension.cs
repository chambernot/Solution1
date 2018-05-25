using Mongeral.Infrastructure.Assertions;
using Mongeral.Infrastructure.Extensions;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mongeral.Provisao.V2.Service.Premio.Extensions
{
    public static class ApropriacaoPremioExtension
    {
        public static void ValidarApropriacao(this IParcelaApropriada evento)
        {
            evento.Validar();
                        
            Assertion.NotNull(evento.Parcelas, "Nenhum prêmio para Apropriar.").Validate();            
            
            evento.Parcelas.Select(p => p.ValidarPremioApropriado()).Aggregate(Assertion.Neutral(), (x, y) => x.and(y)).Validate();
        }

        public static IAssertion ValidarPremioApropriado(this IApropriacao premio)
        {
            var premioValidator = premio.ValidarPremio();

            var itemCertificadoApolice = premio.ParcelaId.IdentificadorExternoCobertura;

            var dataPagamentoValidator = Assertion.GreaterThan(premio.Pagamento.DataPagamento, default(DateTime), $"A Data de Pagamento para o ItemCertificadoApolice: { itemCertificadoApolice }, não foi informada.");
            var dataApropriacaoValidator = Assertion.GreaterThan(premio.Pagamento.DataApropriacao, default(DateTime), $"A Data de Apropriação para o ItemCertificadoApolice: { itemCertificadoApolice },  não foi informada.");
            var valorPagoValidator = Assertion.GreaterThan(premio.Pagamento.ValorPago, default(decimal), $"O Valor Pago para o ItemCertificadoApolice: { itemCertificadoApolice }, não foi informado.");

            return premioValidator
            .and(dataPagamentoValidator)
            .and(dataApropriacaoValidator)
            .and(valorPagoValidator);
        }
        
        public static IEnumerable<PremioApropriado> ToEventoApropriacao(this IParcelaApropriada premioApropriado)
        {
            var evento = new EventoApropriacaoPremio(premioApropriado.Identificador, premioApropriado.IdentificadorCorrelacao, premioApropriado.IdentificadorNegocio, premioApropriado.DataExecucaoEvento);

            premioApropriado.Parcelas.SafeForEach(premio => evento.AdicionarPremiosApropriados(premio.ToPremioApropriado()));

            return evento.Premios;
        }

        public static PremioApropriado ToPremioApropriado(this IApropriacao premioApropriado)
        {
            var premio = premioApropriado.ToPremio<PremioApropriado>();

            premio.Pagamento = premioApropriado.Pagamento.ToPagamentoPremio();
            
            return premio;
        }
    }
}
