using Mongeral.Provisao.V2.DAL.Persistir;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Dominios;
using Mongeral.Provisao.V2.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.DAL
{
    public class EventosImplantacao : IEventosBase<EventoImplantacao>
    {
        private readonly Eventos<EventoImplantacao> _eventos;
        private readonly Coberturas _coberturaDao;
        private readonly HistoricosCobertura _historicoDao;

        public EventosImplantacao(Eventos<EventoImplantacao> evento,
            Coberturas coberturaDao,
            HistoricosCobertura historicoDao)
        {
            _eventos = evento;
            _coberturaDao = coberturaDao;
            _historicoDao = historicoDao;
        }
       
        public async Task<bool> ExisteEvento(Guid identificador)
        {
            return await _eventos.Contem(identificador);
        }

        public async Task Salvar(EventoImplantacao evento)
        {
            await _eventos.Salvar(evento);

            foreach (var cobertura in evento.Coberturas)
            {
                cobertura.AdicionarEventoId(evento.Id);
                await SalvarCobertura(cobertura);
            }
        }

        private async Task SalvarCobertura(CoberturaContratada cobertura)
        {            
            await _coberturaDao.Salvar(cobertura);

            if (cobertura.Historico != null)
            {
                cobertura.Historico.AdicionarCobertura(cobertura);
                cobertura.Historico.InformaStatus(StatusCobertura.StatusCoberturaEnum.Activa, DateTime.Now);
                await _historicoDao.Salvar(cobertura.Historico);
            }
        }       

        public async Task Compensate(Guid identificador)
        {
            await _historicoDao.Remover(identificador);

            await _coberturaDao.Remover(identificador);

            await _eventos.Remover(identificador);
        }        
    }
}
