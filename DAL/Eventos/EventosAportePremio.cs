using System;
using System.Threading.Tasks;
using Mongeral.Provisao.V2.DAL.Persistir;
using Mongeral.Provisao.V2.Domain.Entidades.EventosOperacional;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Enum;

namespace Mongeral.Provisao.V2.DAL
{
    public class EventosAportePremio: IEventosBase<EventoAportePremio>
    {
        private readonly Eventos<EventoAportePremio> _eventos;
        private readonly PremioAporteDao _aportePremioDao;
        private readonly MovimentosProvisao _movimentoProvisaoDao;
        private readonly ProvisoesCobertura _provisaoCoberturaDao;

        public EventosAportePremio(
            Eventos<EventoAportePremio> eventos,
            PremioAporteDao aportePremioDao,
            MovimentosProvisao movimentoProvisaoDao,
            ProvisoesCobertura provisaoCoberturaDao)
        {
            _eventos = eventos;
            _aportePremioDao = aportePremioDao;
            _movimentoProvisaoDao = movimentoProvisaoDao;
            _provisaoCoberturaDao = provisaoCoberturaDao;
        }

        public async Task Adicionar(EventoAportePremio evento)
        {
            foreach (var premio in evento.Premios)
            {
                if (evento.Id.Equals(default(Guid)))
                    await _eventos.Adicionar(evento);

                premio.TipoMovimentoId = (short)TipoMovimentoEnum.Aporte;
                await _aportePremioDao.AdicionaAporte(premio);
            }
        }

        public async Task Remover(Guid identificador)
        {
            await _movimentoProvisaoDao.Remover(identificador);

            await _aportePremioDao.Remover(identificador);

            await _eventos.Remover(identificador);
        }

        public async Task<bool> Contem(Guid identificador)
        {
            return await _eventos.Contem(identificador);
        }
    }
}
