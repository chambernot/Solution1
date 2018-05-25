using Mongeral.Provisao.V2.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mongeral.Provisao.V2.Domain
{
    public abstract class EventoPremio : EventoOperacional
    {
        public EventoPremio(Guid identificador, string idCorrelacao, string idNegocio, DateTime dataExecucao)
            : base(identificador, idCorrelacao, idNegocio, dataExecucao)
        {
            _premios = new List<Premio>();
        }

        public void AdicionarPremio(Premio premio)
        {
            if (premio != null)
                _premios.Add(premio);
        }
        
        protected List<Premio> _premios;
        public IEnumerable<Premio> Premios => _premios.AsEnumerable();
        public abstract TipoMovimentoEnum TipoMovimento { get; }
        public abstract TipoEventoEnum TipoEvento { get; }
        public abstract IList<short> RegimeFinanceiroPermitido { get; }        
        public abstract IList<TipoProvisaoEnum> ObterProvisaoPossiveisDoEvento(short regimeFinanceiro);          
    }
}
