using Mongeral.Infrastructure.Assertions;
using Mongeral.Infrastructure.Extensions;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Entidades;
using System.Collections.Generic;
using System.Linq;

namespace Mongeral.Provisao.V2.Service.Premio.Extensions
{
    public static class DesapropriacaoPremioExtensions
    {
        public static void ValidarDesapropriacao(this IParcelaDesapropriada contrato)
        {
            contrato.Validar();

            Assertion.NotNull(contrato.Parcelas, "Nenhum prêmio para Desapropriar.").Validate();

            contrato.Parcelas.Select(p => p.ValidarPremioDesapropriado())
                .Aggregate(Assertion.Neutral(), (x, y) => x.and(y)).Validate();
        }

        public static IAssertion ValidarPremioDesapropriado(this IApropriacao premio)
        {
            return premio.ValidarPremioApropriado();
        }

        public static IEnumerable<PremioDesapropriado> ToEventoDesapropriacao(this IParcelaDesapropriada contrato)
        {
            var evento = new EventoDesapropriacaoPremio(contrato.Identificador, contrato.IdentificadorCorrelacao,
                contrato.IdentificadorNegocio, contrato.DataExecucaoEvento);

            contrato.Parcelas.SafeForEach(premio => evento.AdicionarPremio(premio.ToPremioDesapropriado()));

            return evento.Premios;
        }

        public static PremioDesapropriado ToPremioDesapropriado(this IApropriacao premioDesapropriado)
        {
            var premio = premioDesapropriado.ToPremio<PremioDesapropriado>();

            premio.Pagamento = premioDesapropriado.Pagamento.ToPagamentoPremio();

            return premio;
        }
    }
}

