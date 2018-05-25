using System;
using System.Collections.Generic;
using Mongeral.Provisao.V2.Domain.Enum;

namespace Mongeral.Provisao.V2.Domain
{
    public class EventoApropriacaoPremio : EventoPremio
    {
        public EventoApropriacaoPremio(Guid identificador, string idCorrelacao, string idNegocio, DateTime dataExecucao)
            : base(identificador, idCorrelacao, idNegocio, dataExecucao) { }
        
        public override short TipoEventoId => (short)TipoEvento;
        public override TipoEventoEnum TipoEvento => TipoEventoEnum.ApropriacaoPremio;
        public override TipoMovimentoEnum TipoMovimento => TipoMovimentoEnum.Apropriacao;

        public override IList<TipoProvisaoEnum> ObterProvisaoPossiveisDoEvento(short regimeFinanceiro)
        {
            return regimeFinanceiro == (short)TipoRegimeFinanceiroEnum.Capitalizacao ?
                new List<TipoProvisaoEnum> { TipoProvisaoEnum.PMBAC } :
                new List<TipoProvisaoEnum>();
        }

        public override IList<short> RegimeFinanceiroPermitido => new List<short>()
        {
            (short)TipoRegimeFinanceiroEnum.Capitalizacao,
            (short)TipoRegimeFinanceiroEnum.FundoAcumulacao
        };
    }
}
