using System;
using System.Threading.Tasks;
using Mongeral.Infrastructure.Cache;
using Mongeral.Provisao.V2.Application.Interfaces;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.DTO;

namespace Mongeral.Provisao.V2.Application
{
    public class ProcessaEventoImplantacao : IProcessaEvento<EventoImplantacao>
    {
        private readonly IValidaEventoImplantacao _validador;
        private readonly IEventos<EventoImplantacao> _eventos;
        private readonly IndexedCachedContainer<ChaveProduto, DadosProduto> _produtoContainer;

        public ProcessaEventoImplantacao(
            IEventos<EventoImplantacao> eventos, 
            IValidaEventoImplantacao validador, 
            IndexedCachedContainer<ChaveProduto, DadosProduto> produtoContainer)
        {
            _eventos = eventos;
            _validador = validador;
            _produtoContainer = produtoContainer;
        }

        public async Task<EventoImplantacao> Processar(EventoImplantacao evento)
        {
            if (await _eventos.Contem(evento.Identificador))
                return evento;

            foreach (var cob in evento.Coberturas)
            {
                var dadosProduto = _produtoContainer.GetValue(new ChaveProduto(cob.ItemProdutoId, cob.TipoFormaContratacaoId, cob.TipoRendaId, cob.DataAssinatura));
                cob.ComDadosProduto(dadosProduto);

                await _validador.Validar(cob);
            }

            await _eventos.Adicionar(evento);

            return evento;
        }

        public async Task Compensar(Guid identificador)
        {
            await _eventos.Remover(identificador);
        }
    }
}
