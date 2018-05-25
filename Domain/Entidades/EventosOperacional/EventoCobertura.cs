using Mongeral.Infrastructure.Assertions;
using Mongeral.Provisao.V2.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Mongeral.Provisao.V2.Domain
{
    public abstract class EventoCobertura : EventoOperacional
    {
        private HistoricoCoberturaContratada _historicoCoberturaContratada;
        protected List<MovimentoProvisao> _listaMovimentoProvisao;
        public List<MovimentoProvisao> MovimentosProvisao { get; set; }
        public HistoricoCoberturaContratada Historico => _historicoCoberturaContratada;
        public EventoCobertura(Guid identificador, string idCorrelacao, string idNegocio, DateTime dataExecucao) : base(identificador, idCorrelacao, idNegocio, dataExecucao)
        {
            _listaMovimentoProvisao = new List<MovimentoProvisao>();
        }
        public abstract TipoEventoEnum TipoEvento { get; }
        public EventoCobertura ComHistorico(HistoricoCoberturaContratada historico)
        {

            Assertion.NotNull(historico, "Historico cobertura não pode ser nulo").Validate();

            _historicoCoberturaContratada = historico;
            historico.InformaEvento(this);
            return this;
        }

        
    }
}
