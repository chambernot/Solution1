using Mongeral.Provisao.V2.DAL.Persistir;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.DAL
{
    public class EventosPremio : IEventosBase<EventoPremio>
    {
        private readonly Eventos<EventoPremio> _eventos;
        private readonly MovimentosProvisao _movimentos;
        private readonly ProvisoesCobertura _provisoes;
        private readonly Premios _premios;

        public EventosPremio(Eventos<EventoPremio> eventos,
            MovimentosProvisao movimentos,
            ProvisoesCobertura provisoes,
            Premios premios)
        {
            _eventos = eventos;
            _movimentos = movimentos;
            _provisoes = provisoes;
            _premios = premios;
        }
            
        public async Task<bool> ExisteEvento(Guid identificador)
        {
            return await _eventos.Contem(identificador);
        }

        public async Task Salvar(EventoPremio evento)
        {
            foreach (var premio in evento.Premios)
            {
                if (evento.Id.Equals(default(Guid)))
                    await _eventos.Salvar(evento);

                await _premios.Salvar(premio);

                foreach (var movimento in premio.MovimentosProvisao)
                {
                    var provisaoCobertura = new ProvisaoCobertura(premio.Cobertura, movimento.ProvisaoId);
                    await _provisoes.Adicionar(provisaoCobertura);

                    movimento.AdicionarProvisaoCobertura(provisaoCobertura);

                    movimento.AdicionarPremio(premio);
                    await _movimentos.Salvar(movimento);
                }
            }
        }

        public async Task Compensate(Guid identificador)
        {
            var listaProvisaoCoberturaId = await _movimentos.ObterProvisaoCobertura(identificador);

            await _movimentos.Remover(identificador);
            await _provisoes.Remover(listaProvisaoCoberturaId);
            await _premios.Remover(identificador);
            await _eventos.Remover(identificador);
        }        
    }
}
