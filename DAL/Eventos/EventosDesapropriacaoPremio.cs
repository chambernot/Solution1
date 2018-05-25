using Mongeral.Provisao.V2.DAL.Persistir;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.Provisao.V2.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.DAL
{
    public class EventosDesapropriacaoPremio : IEventosBase<EventoDesapropriacaoPremio>
    {
        private readonly Eventos<EventoDesapropriacaoPremio> _eventos;
        private readonly PremioDesapropriadoDao _desapropriacaoPremioDao;
        private readonly MovimentosProvisao _movimentoProvisaoDao;
        private readonly ProvisoesCobertura _provisaoCoberturaDao;

        public EventosDesapropriacaoPremio(
            Eventos<EventoDesapropriacaoPremio> eventos,
            PremioDesapropriadoDao desapropriacaoPremioDao,
            MovimentosProvisao movimentoProvisaoDao,
            ProvisoesCobertura provisaoCoberturaDao)
        {
            _eventos = eventos;
            _desapropriacaoPremioDao = desapropriacaoPremioDao;
            _movimentoProvisaoDao = movimentoProvisaoDao;
            _provisaoCoberturaDao = provisaoCoberturaDao;
        }

        public async Task Adicionar(EventoDesapropriacaoPremio evento)
        {
            foreach (var premio in evento.PremiosValidos)
            {
                if (evento.Id.Equals(default(Guid)))
                    await _eventos.Adicionar(evento);

                premio.InformaEvento(evento);
                premio.TipoMovimentoId = (short)TipoMovimentoEnum.Desapropriacao;
                await _desapropriacaoPremioDao.AdicionarDesapropriacao(premio);

                if (premio.Cobertura.RegimeFinanceiroId == (short)TipoRegimeFinanceiroEnum.Capitalizacao)
                {
                    foreach (var provisoes in premio.MovimentosProvisao)
                    {
                        await AdicionarMovimento(provisoes, premio);
                    }
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

            await _desapropriacaoPremioDao.Remover(identificador);

            await _eventos.Remover(identificador);
        }
    }
}
