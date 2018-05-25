using Mongeral.Infrastructure.Cache;
using Mongeral.Provisao.V2.Application.Interfaces;
using Mongeral.Provisao.V2.Application.Validador;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.DTO;
using System;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Application
{
    public class ExecutarEventoInclusaoVg: IExecucaoEvento
    {
        private readonly ValidadorCobertura _validador;
        private readonly IEventosBase<EventoInclusaoVg> _eventos;
        private readonly IndexedCachedContainer<ChaveProduto, DadosProduto> _container;

        public ExecutarEventoInclusaoVg(IndexedCachedContainer<ChaveProduto, DadosProduto> container,
                IEventosBase<EventoInclusaoVg> eventos, ValidadorCobertura validador)
        {
            _eventos = eventos;
            _container = container;
            _validador = validador;
        }

        public async Task Executar(EventoOperacional eventoOperacional)
        {
            var evento = (EventoInclusaoVg)eventoOperacional;

            foreach (var cobertura in evento.Coberturas)
            {
                var produto = _container.GetValue(new ChaveProduto(cobertura.ItemProdutoId, cobertura.TipoFormaContratacaoId, cobertura.TipoRendaId, cobertura.DataAssinatura));
                cobertura.ComDadosProduto(produto);

                await _validador.Validar(cobertura);
            }
            await _eventos.Salvar(evento);
        }

        public async Task Compensate(Guid identificador)
        {
            await _eventos.Compensate(identificador);
        }

        public async Task<bool> ExisteEvento(Guid identificador)
        {
            return await _eventos.ExisteEvento(identificador);
        }
    }
}
