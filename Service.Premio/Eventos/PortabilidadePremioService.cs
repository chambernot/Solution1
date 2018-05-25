using Mongeral.Infrastructure.ServiceBus.Subscribe;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Interfaces;
using System.Threading.Tasks;
using Mongeral.Provisao.V2.Service.Premio.Validacao;
using Mongeral.Provisao.V2.Service.Premio.Factory;
using Mongeral.Provisao.V2.Service.Premio.Mapear;

namespace Mongeral.Provisao.V2.Service.Premio.Eventos
{
    public class PortabilidadePremioService : OrchestrationSubscribeService<IPortabilidadeApropriada>
    {
        private readonly IEventosBase<EventoPremio> _eventos;
        private readonly EventoPremioFactory _factory;

        public PortabilidadePremioService(IEventosBase<EventoPremio> eventos, EventoPremioFactory factory)
        {
            _eventos = eventos;
            _factory = factory;
        }

        public async Task<IPortabilidadeApropriada> Execute(IPortabilidadeApropriada message)
        {
            message.Validar();

            var evento = await _factory.Fabricar(message);
            await _eventos.Salvar(evento);

            message.Portabilidades.ForEach(p => p.ToProvisao(evento));

            return message;
        }

        public async Task Compensate(IPortabilidadeApropriada message)
        {
            await _eventos.Compensate(message.Identificador);
        }
    }
}
