using System;
using System.Threading.Tasks;
using Mongeral.Provisao.V2.DAL.Persistir;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Entidades.EventosOperacional;
using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.Provisao.V2.Domain.Interfaces;

namespace Mongeral.Provisao.V2.DAL
{
    public class EventosPortabilidadePremio: IEventosBase<EventoPortabilidadePremio>
    {
        private readonly Eventos<EventoPortabilidadePremio> _eventos;
        private readonly PremioPortabilidadeDao _portabilidadePremioDao;
        private readonly MovimentosProvisao _movimentoProvisaoDao;
        private readonly ProvisoesCobertura _provisaoCoberturaDao;

        public EventosPortabilidadePremio(
            Eventos<EventoPortabilidadePremio> eventos,
            PremioPortabilidadeDao premioPortabilidadeDao,
            MovimentosProvisao movimentoProvisaoDao,
            ProvisoesCobertura provisaoCoberturaDao)
        {
            _eventos = eventos;
            _portabilidadePremioDao = premioPortabilidadeDao;
            _movimentoProvisaoDao = movimentoProvisaoDao;
            _provisaoCoberturaDao = provisaoCoberturaDao;
        }
        
        public async Task Adicionar(EventoPortabilidadePremio evento)
        {
            foreach (var premio in evento.PremiosValidos)
            {
                if (evento.Id.Equals(default(Guid)))
                    await _eventos.Adicionar(evento);

                premio.InformaEvento(evento);
                premio.TipoMovimentoId = (short)TipoMovimentoEnum.Portabilidade;
                await _portabilidadePremioDao.AdicionarPortabilidade(premio);
            }
        }

        public async Task<bool> Contem(Guid identificador)
        {
            return await _eventos.Contem(identificador);
        }

        public async Task Remover(Guid identificador)
        {
            await _portabilidadePremioDao.Remover(identificador);

            await _eventos.Remover(identificador);
        }
    }
}
