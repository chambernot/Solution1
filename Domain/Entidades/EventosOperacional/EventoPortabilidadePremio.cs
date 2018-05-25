using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mongeral.Infrastructure.Assertions;
using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.Provisao.V2.Domain.Interfaces;

namespace Mongeral.Provisao.V2.Domain
{
    public class EventoPortabilidadePremio : EventoPremio
    {
        public EventoPortabilidadePremio(Guid identificador, string idCorrelacao, string idNegocio, DateTime dataExecucao)
            : base(identificador, idCorrelacao, idNegocio, dataExecucao) { }
     
        public override short TipoEventoId => (short)TipoEvento;
        public override TipoEventoEnum TipoEvento => TipoEventoEnum.PortabilidadePremio;

        public override TipoMovimentoEnum TipoMovimento => TipoMovimentoEnum.Portabilidade;

        public int ParcelaPortabilidade => 0;

        public override IList<TipoProvisaoEnum> ObterProvisaoPossiveisDoEvento(short regimeFinanceiro)
        {
            return new List<TipoProvisaoEnum>();
        }

        public override IList<short> RegimeFinanceiroPermitido => new List<short>()
        {
            (short)TipoRegimeFinanceiroEnum.FundoAcumulacao
        };
    }
}
