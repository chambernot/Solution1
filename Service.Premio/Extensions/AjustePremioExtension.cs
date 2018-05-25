using Mongeral.Infrastructure.Assertions;
using Mongeral.Infrastructure.Extensions;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Service.Premio.Extensions
{
    public static class AjustePremioExtension
    {
        public static void ValidarAjustePremio(this IParcelaAjustada contrato)
        {
            contrato.Validar();

            Assertion.NotNull(contrato.Parcelas, "Nenhum prêmio para ajustar.").Validate();

            contrato.Parcelas.Select(p => p.ValidarPremio()).Aggregate(Assertion.Neutral(), (x, y) => x.and(y)).Validate();
        }

        public static IEnumerable<Domain.Premio> ToEventoAjustePremio(this IParcelaAjustada premioAjustado)
        {
            var evento = new EventoAjustePremio(premioAjustado);

            premioAjustado.Parcelas.SafeForEach(premio => evento.AdicionarPremio(premio.ToPremio<Domain.Premio>()));

            return evento.Premios;
        }
    }
}
