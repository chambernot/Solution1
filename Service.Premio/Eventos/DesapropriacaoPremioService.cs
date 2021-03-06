﻿using Mongeral.Infrastructure.ServiceBus.Subscribe;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.Service.Premio.Factory;
using Mongeral.Provisao.V2.Service.Premio.Mapear;
using Mongeral.Provisao.V2.Service.Premio.Validacao;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Service.Premio.Eventos
{
    public class DesapropriacaoPremioService : OrchestrationSubscribeService<IParcelaDesapropriada>
    {
        private readonly IEventosBase<EventoPremio> _eventos;
        private readonly EventoPremioFactory _factory;

        public DesapropriacaoPremioService(IEventosBase<EventoPremio> eventos, EventoPremioFactory factory)
        {
            _eventos = eventos;
            _factory = factory;
        }

        public async Task<IParcelaDesapropriada> Execute(IParcelaDesapropriada message)
        {
            message.Validar();

            var evento = await _factory.Fabricar(message);
            await _eventos.Salvar(evento);

            message.Parcelas.ForEach(p => p.ToProvisao(evento));

            return message;
        }

        public async Task Compensate(IParcelaDesapropriada message)
        {
            await _eventos.Compensate(message.Identificador);
        }
    }
}
