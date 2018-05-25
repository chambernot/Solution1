using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mongeral.Provisao.V2.Domain.Enum;

namespace Mongeral.Provisao.V2.Domain
{
    public class EventoAportePremio: EventoPremio
    {
        public EventoAportePremio(Guid identificador, string idCorrelacao, string idNegocio, DateTime dataExecucao)
            : base(identificador, idCorrelacao, idNegocio, dataExecucao) { }

        public override short TipoEventoId => (short)TipoEvento;
        public override TipoEventoEnum TipoEvento => TipoEventoEnum.AportePremio;
        public override TipoMovimentoEnum TipoMovimento => TipoMovimentoEnum.Aporte;
        public int ParcelaAporte => 0;

        public override IList<short> RegimeFinanceiroPermitido => new List<short>()
        {
            (short)TipoRegimeFinanceiroEnum.FundoAcumulacao
        };

        public override IList<TipoProvisaoEnum> ObterProvisaoPossiveisDoEvento(short regimeFinanceiro)
        {
            return new List<TipoProvisaoEnum>();
        }
    }
}
