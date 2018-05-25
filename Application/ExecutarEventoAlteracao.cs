using Mongeral.Provisao.V2.Application.Interfaces;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Application
{
    public class ExecutarEventoAlteracao : IExecucaoEvento
    {
        private readonly IEventosBase<EventoAlteracao> _eventos;
        private readonly ICoberturas _coberturas;

        public ExecutarEventoAlteracao(IEventosBase<EventoAlteracao> eventos, ICoberturas coberturas)
        {
            _eventos = eventos;
            _coberturas = coberturas;
        }

        public async Task Executar(EventoOperacional eventoOperacional)
        {
            var evento = (EventoAlteracao)eventoOperacional;
            
            await _eventos.Salvar(evento);
        }

        public async Task Compensate(Guid identificador)
        {
            await _eventos.Compensate(identificador);
        }

        public async Task<bool> ExisteEvento(Guid identificador)
        {
            return await _eventos.ExisteEvento(identificador);
        }
    }
}
