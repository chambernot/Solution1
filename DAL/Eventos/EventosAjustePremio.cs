using Mongeral.Provisao.V2.DAL.Persistir;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.Provisao.V2.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.DAL
{
    public class EventosAjustePremio: IEventosBase<EventoAjustePremio>
    {
        private readonly Eventos<EventoAjustePremio> _eventos;
        private readonly Premios _premioDao;
        private readonly MovimentosProvisao _movimentoProvisaoDao;
        private readonly ProvisoesCobertura _provisaoCoberturaDao;

        public EventosAjustePremio(
            Eventos<EventoAjustePremio> eventos,
            Premios premioDao,
            MovimentosProvisao movimentoProvisaoDao,
            ProvisoesCobertura provisaoCoberturaDao)
        {
            _eventos = eventos;
            _premioDao = premioDao;
            _movimentoProvisaoDao = movimentoProvisaoDao;
            _provisaoCoberturaDao = provisaoCoberturaDao;
        }

        public async Task Adicionar(EventoAjustePremio evento)
        {
            foreach (var premio in evento.Premios)
            {
                if (evento.Id.Equals(default(Guid)))
                    await _eventos.Adicionar(evento);

                premio.TipoMovimentoId = (short)TipoMovimentoEnum.Ajuste;

                await _premioDao.Adicionar(premio);

                foreach (var provisoes in premio.MovimentosProvisao)
                {
                    await AdicionarMovimento(provisoes, premio);
                }
            }
        }

        public async Task AdicionarMovimento(MovimentoProvisao movimentoProvisao, Premio premio)
        {
            var provisaoCobertura = new ProvisaoCobertura(premio.Cobertura, movimentoProvisao.ProvisaoId);
            await _provisaoCoberturaDao.Adicionar(provisaoCobertura);

            movimentoProvisao.AdicionarProvisaoCobertura(provisaoCobertura);
            movimentoProvisao.AdicionarPremio(premio);

            await _movimentoProvisaoDao.Adicionar(movimentoProvisao);
        }

        public async Task<bool> Contem(Guid identificador)
        {
            return await _eventos.Contem(identificador);
        }
        
        public async Task Remover(Guid identificador)
        {
            await _movimentoProvisaoDao.Remover(identificador);

            await _premioDao.Remover(identificador);

            await _eventos.Remover(identificador);
        }
    }
}
