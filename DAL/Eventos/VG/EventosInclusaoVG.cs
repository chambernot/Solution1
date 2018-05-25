using System;
using System.Threading.Tasks;
using Mongeral.Provisao.V2.DAL.Persistir;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Interfaces;

namespace Mongeral.Provisao.V2.DAL
{
    public class EventosInclusaoVg: EventosImplantacao, IEventosBase<EventoInclusaoVg>
    {
        public EventosInclusaoVg(Eventos<EventoImplantacao> evento, Coberturas coberturaDao, 
            HistoricosCobertura historicoDao) : base(evento, coberturaDao, historicoDao)
        {
        }


        public async Task Salvar(EventoInclusaoVg evento)
        {
            await base.Salvar(evento);
        }

        public new async Task Compensate(Guid identificador)
        {
            await base.Compensate(identificador);
        }

        public new async Task<bool> ExisteEvento(Guid identificador)
        {
            return await base.ExisteEvento(identificador);
        }        
    }
}
