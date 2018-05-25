using System;
using System.Collections.Generic;
using System.Linq;
using Mongeral.Infrastructure.Assertions;
using Mongeral.Provisao.V2.Domain.Enum;

namespace Mongeral.Provisao.V2.Domain
{
    public class EventoAlteracao : EventoOperacional
    {
        private readonly List<HistoricoCoberturaContratada> _historicoCoberturaContratadas;
        public IEnumerable<HistoricoCoberturaContratada> Historicos => _historicoCoberturaContratadas.AsEnumerable();
        

        public EventoAlteracao(Guid identificador, string idCorrelacao, string idNegocio, DateTime dataExecucao)
            : base(identificador, idCorrelacao, idNegocio, dataExecucao)
        {
            _historicoCoberturaContratadas = new List<HistoricoCoberturaContratada>();
        }
      
        public override short TipoEventoId => (short)TipoEventoEnum.AlteracaoParametros;

        public EventoAlteracao ComHistorico(IEnumerable<HistoricoCoberturaContratada> historicos)
        {
            foreach (var historico in historicos)
            {
                Assertion.NotNull(historico, "Historico cobertura não pode ser nulo").Validate();
                                
                _historicoCoberturaContratadas.Add(historico);
                historico.InformaEvento(this);
            }
            return this;
        }

        public IEnumerable<short> MovimentosPermitidos { get; }        
    }
}
