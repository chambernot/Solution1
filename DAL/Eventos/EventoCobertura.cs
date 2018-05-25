using Mongeral.Provisao.V2.DAL.Persistir;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.DAL
{
    public class EventoCobertura<TEventoCobertura>: IEventosBase<TEventoCobertura> where TEventoCobertura : Mongeral.Provisao.V2.Domain.EventoCobertura
    {
        private readonly Eventos<TEventoCobertura> _eventos;
        private readonly HistoricosCobertura _historico;
        private readonly MovimentosProvisao _movimentos;
        private readonly ProvisoesCobertura _provisoes;

        public EventoCobertura(Eventos<TEventoCobertura> eventos, HistoricosCobertura historico, MovimentosProvisao movimentos, ProvisoesCobertura provisoes)
        {
            _eventos = eventos;
            _historico = historico;
            _movimentos = movimentos;
            _provisoes = provisoes;
        }

        public async Task Compensate(Guid identificador)
        {
            await _historico.Remover(identificador);

            await _eventos.Remover(identificador);
        }

        public async Task<bool> ExisteEvento(Guid identificador)
        {
            return await _eventos.Contem(identificador);
        }

        public async Task Salvar(TEventoCobertura evento)
        {
            await _eventos.Salvar(evento);
            
            await _historico.Salvar(evento.Historico);

            foreach (var provisao in evento.MovimentosProvisao)
            {
                var provisaoCobertura = new ProvisaoCobertura(evento.Historico.Cobertura, provisao.ProvisaoId);
                await _provisoes.Adicionar(provisaoCobertura);
                provisao.EventoIdCobertura = evento.Id;
                provisao.AdicionarProvisaoCobertura(provisaoCobertura);
                await _movimentos.Salvar(provisao);
            }
        }
    }
}
