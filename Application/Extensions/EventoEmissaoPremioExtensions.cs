using Mongeral.Infrastructure.Assertions;
using Mongeral.Provisao.V2.Domain;

namespace Mongeral.Provisao.V2.Application.Extensions
{
    public static class EventoEmissaoPremioExtensions
    {
        public static void Validar(this EventoEmissaoPremio evento, CoberturaContratada cobertura)
        {            
            var coberturaValidate = Assertion.NotNull(cobertura, "O Evento não possui cobertura cadastrada.");

            coberturaValidate.Validate();
        }
    }
}
