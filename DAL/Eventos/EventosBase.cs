using System;
using System.Threading.Tasks;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Interfaces;

namespace Mongeral.Provisao.V2.DAL
{
    public class EventosBase<TEvento> : IEventosBase<TEvento> where TEvento : EventoOperacional
    {
        private readonly IEventosDaoFactory<TEvento> _factory;
        public EventosBase(IEventosDaoFactory<TEvento> factory)
        {
            _factory = factory;
        }
        public async Task<bool> ExisteEvento(Guid identificador)
        {
            return await _factory.Criar().ExisteEvento(identificador);
        }

        public async Task Compensate(Guid identificador)
        {
            await _factory.Criar().Compensate(identificador);
        }

        public async Task Salvar(TEvento evento)
        {
            await _factory.Criar().Salvar(evento);
        }

        public Task<TEvento> ObterPorIdTransacao(Guid id)
        {
            throw new NotImplementedException();
        }        
    }
}