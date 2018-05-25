using Mongeral.Infrastructure.ServiceBus.Subscribe;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.Service.Premio.Factory;
using Mongeral.Provisao.V2.Service.Premio.Mapear;
using Mongeral.Provisao.V2.Service.Premio.Validacao;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Service.Premio.Eventos
{
    public class ApropriacaoPremioService : OrchestrationSubscribeService<IParcelaApropriada>
    {
        private readonly IEventosBase<EventoPremio> _eventos;
        private readonly EventoPremioFactory _factory;

        public ApropriacaoPremioService(IEventosBase<EventoPremio> eventos, EventoPremioFactory factory)
        {
            _eventos = eventos;
            _factory = factory;
        }

        public async Task<IParcelaApropriada> Execute(IParcelaApropriada message)
        {
            message.Validar();

            var evento = await _factory.Fabricar(message);
            await _eventos.Salvar(evento);

            message.Parcelas.ForEach(p => p.ToProvisao(evento));

            return message;
        }

        public async Task Compensate(IParcelaApropriada message)
        {
            await _eventos.Compensate(message.Identificador);
        }
    }
}
