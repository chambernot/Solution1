using Mongeral.Infrastructure.Assertions;
using Mongeral.Infrastructure.ServiceBus.Subscribe;
using Mongeral.Integrador.Contratos;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.Service.Premio.Mapear;
using Mongeral.Provisao.V2.Service.Premio.Validacao;
using System;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Service.Premio.Eventos.VG
{
    public class AlteracaoPropostaVgService : OrchestrationSubscribeService<IProposta>
    {
        private readonly IEventosBase<EventoAlteracao> _eventos;
        private readonly PropostaToEventoAlteracao _mapear;

        public AlteracaoPropostaVgService(IEventosBase<EventoAlteracao> eventos, PropostaToEventoAlteracao mapear)
        {
            _eventos = eventos;
            _mapear = mapear;
        }

        public async Task<IProposta> Execute(IProposta message)
        {
            message.Validar();

            var evento = await _mapear.ToEventoAlteracao(message);

            if (!await _eventos.ExisteEvento(message.Identificador))
                await _eventos.Salvar(evento);

            return message;
        }

        public async Task Compensate(IProposta message)
        {
            var identificador = Assertion.IsFalse(message.Identificador.Equals(Guid.Empty), "O identificador da proposta não foi informado.");

            await _eventos.Compensate(message.Identificador);
        }

    }
}
