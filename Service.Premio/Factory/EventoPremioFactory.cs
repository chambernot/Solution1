using Mongeral.Infrastructure.Assertions;
using Mongeral.Integrador.Contratos;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Domain;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Service.Premio.Factory
{
    public class EventoPremioFactory
    {
        private FabricarEventoPremio _fabricarEventoPremio;

        public EventoPremioFactory(FabricarEventoPremio fabricarEventoPremio)
        {
            _fabricarEventoPremio = fabricarEventoPremio;
        }

        public async Task<EventoPremio> Fabricar(IEvento evento)
        {
            EventoPremio eventoPremio;

            switch (evento)
            {
                case IParcelaEmitida emissao:
                    eventoPremio = await _fabricarEventoPremio.Com(emissao);
                    break;
                case IParcelaApropriada apropriacao:
                    eventoPremio = await _fabricarEventoPremio.Com(apropriacao);
                    break;
                case IParcelaDesapropriada desapropriacao:
                    eventoPremio = await _fabricarEventoPremio.Com(desapropriacao);
                    break;
                case IParcelaAjustada ajuste:
                    eventoPremio = await _fabricarEventoPremio.Com(ajuste);
                    break;
                case IAporteApropriado aporte:
                    eventoPremio = await _fabricarEventoPremio.Com(aporte);
                    break;
                case IPortabilidadeApropriada portabilidade:
                    eventoPremio = await _fabricarEventoPremio.Com(portabilidade);
                    break;
                default:
                    eventoPremio = null;
                    break;
            }

            Assertion.NotNull(eventoPremio, $"Impossivel criar um evento para {evento.GetType()}");

            return eventoPremio;
        }
    }
}
