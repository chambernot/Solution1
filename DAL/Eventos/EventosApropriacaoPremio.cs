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
    public class EventosApropriacaoPremio : IEventosBase<EventoApropriacaoPremio>
    {
        private readonly Eventos<EventoApropriacaoPremio> _eventos;
        private readonly Premios _premios;        
        private readonly MovimentosProvisao _movimentoProvisao;
        private readonly ProvisoesCobertura _provisaoCobertura;

        public EventosApropriacaoPremio(
            Eventos<EventoApropriacaoPremio> eventos,
            Premios premios,
            MovimentosProvisao movimentoProvisao,
            ProvisoesCobertura provisaoCobertura)
        {
            _eventos = eventos;
            _premios = premios;
            _movimentoProvisao = movimentoProvisao;
            _provisaoCobertura = provisaoCobertura;
        }

        public async Task Adicionar(EventoApropriacaoPremio evento)
        {            
            foreach (var premio in evento.Premios)
            {
                if (evento.Id.Equals(default(Guid)))
                    await _eventos.Adicionar(evento);
                                
                premio.TipoMovimentoId = (short)TipoMovimentoEnum.Apropriacao;                
                await _premios.Adicionar(premio);

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
            await _provisaoCobertura.Adicionar(provisaoCobertura);           
            
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

            await _apropriacaoPremioDao.Remover(identificador);

            await _eventos.Remover(identificador);
        }
    }
}
