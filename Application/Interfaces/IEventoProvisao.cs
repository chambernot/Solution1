using Mongeral.Provisao.V2.Domain;

namespace Mongeral.Provisao.V2.Application.Interfaces
{
    public interface IEventoProvisao
    {
        EventoEmissaoPremio CriarProvisao(EventoEmissaoPremio evento, CoberturaContratada dadosCobertura);
    }
}
