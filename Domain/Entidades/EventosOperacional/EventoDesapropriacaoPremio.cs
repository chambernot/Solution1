using System;
using System.Collections.Generic;
using Mongeral.Provisao.V2.Domain.Enum;

namespace Mongeral.Provisao.V2.Domain
{
    public class EventoDesapropriacaoPremio: EventoPremio
    {
        public EventoDesapropriacaoPremio(Guid identificador, string idCorrelacao, string idNegocio, DateTime dataExecucao)
            : base(identificador, idCorrelacao, idNegocio, dataExecucao) { }
        
        public override short TipoEventoId => (short) TipoEvento;
        public override TipoEventoEnum TipoEvento => TipoEventoEnum.DesapropriacaoPremio;

        public override TipoMovimentoEnum TipoMovimento => TipoMovimentoEnum.Desapropriacao;

        public override IList<TipoProvisaoEnum> ObterProvisaoPossiveisDoEvento(short regimeFinanceiro)
        {
            return new List<TipoProvisaoEnum> { TipoProvisaoEnum.PMBAC };
        }

        public override IList<short> RegimeFinanceiroPermitido => new List<short>()
        {
            (short)TipoRegimeFinanceiroEnum.Capitalizacao,
            (short)TipoRegimeFinanceiroEnum.FundoAcumulacao
        };
    }
}
