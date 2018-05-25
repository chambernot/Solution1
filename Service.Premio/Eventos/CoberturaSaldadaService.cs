using System;
using Mongeral.Provisao.V2.Service.Premio.Validacao;
using System.Threading.Tasks;
using Mongeral.Infrastructure.ServiceBus.Subscribe;
using Mongeral.Integrador.Contratos;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.Service.Premio.Factory;
using Mongeral.Provisao.V2.Domain;

namespace Mongeral.Provisao.V2.Service.Premio.Eventos
{
    public class CoberturaSaldadaService : OrchestrationSubscribeService<ICoberturaSaldada>
    {
        private EventoCoberturaFactory _factory;
        private IEventosBase<EventoCobertura> _eventos;
        private IProvisoes _provisoes;
        public CoberturaSaldadaService(IEventosBase<EventoCobertura> evento, EventoCoberturaFactory factory, IProvisoes provisoes)
        {
            _eventos = evento;
            _factory = factory;
            _provisoes = provisoes;
        }
        public Task Compensate(ICoberturaSaldada message)
        {
            throw new NotImplementedException();
        }

        public async Task<ICoberturaSaldada> Execute(ICoberturaSaldada message)
        {
            
            message.ValidarEvento();
            var evento = await _factory.Fabricar(message);
            await _eventos.Salvar(evento);

            return message;
        }
    }
}
