using Mongeral.Infrastructure.ServiceBus.Subscribe;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Application;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.Service.Premio.Factory;
using Mongeral.Provisao.V2.Service.Premio.Mapear;
using Mongeral.Provisao.V2.Service.Premio.Validacao;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Service.Premio.Eventos
{
    public class EmissaoPremioService : OrchestrationSubscribeService<IParcelaEmitida>
    {        
        private EventoPremioFactory _factory;
        private IEventosBase<EventoPremio> _eventos;
        
        public EmissaoPremioService(IEventosBase<EventoPremio> evento, EventoPremioFactory factory)
        {
            _eventos = evento;
            _factory = factory;
        }

        public async Task<IParcelaEmitida> Execute(IParcelaEmitida message)
        {
            message.Validar();

            var evento = await _factory.Fabricar(message);
            await _eventos.Salvar(evento);

            message.Parcelas.ForEach(p => p.ToProvisao(evento));

            return message;
        }
        
        public async Task Compensate(IParcelaEmitida message)
        {
            await _eventos.Compensate(message.Identificador);
        }
    }
}
