using Mongeral.Provisao.V2.DAL.Persistir;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Dominios;
using Mongeral.Provisao.V2.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.DAL
{
    public class EventosAlteracaoParametros: IEventosBase<EventoAlteracao>
    {
        private readonly Eventos<EventoAlteracao> _eventos;        
        private readonly HistoricosCobertura _historico;        

        public EventosAlteracaoParametros(Eventos<EventoAlteracao> eventos, HistoricosCobertura historico)
        {
            _eventos = eventos;
            _historico = historico;            
        }
        
        public async Task Salvar(EventoAlteracao evento)
        {
            await _eventos.Salvar(evento);

            foreach (var historico in evento.Historicos)
            {
                historico.InformaEvento(evento);
                historico.InformaStatus(StatusCobertura.StatusCoberturaEnum.Activa, DateTime.Now);
                await _historico.Salvar(historico);
            }
        }

        public async Task Apagar(Guid eventoId)
        {
            await _historico.Remover(eventoId);            

            await _eventos.Remover(eventoId);
        }


        public async Task<bool> ExisteEvento(Guid identificador)
        {
            return await _eventos.Contem(identificador);
        }

        public async Task Compensate(Guid identificador)
        {
            await _historico.Remover(identificador);
            
            await _eventos.Remover(identificador);
        }
    }
}
