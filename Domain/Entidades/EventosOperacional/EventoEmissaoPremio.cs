using Mongeral.Provisao.V2.Domain.Enum;
using System;
using System.Collections.Generic;

namespace Mongeral.Provisao.V2.Domain
{
    public class EventoEmissaoPremio : EventoPremio
    {
        public EventoEmissaoPremio(Guid identificador, string idCorrelacao, string idNegocio, DateTime dataExecucao)
            : base(identificador, idCorrelacao, idNegocio, dataExecucao) { }        

        public override short TipoEventoId => (short)TipoEvento;
        public override TipoEventoEnum TipoEvento => TipoEventoEnum.EmissaoPremio;
        public override TipoMovimentoEnum TipoMovimento => TipoMovimentoEnum.Emissao;

        public override IList<TipoProvisaoEnum> ObterProvisaoPossiveisDoEvento(short regimeFinanceiro)
        {
            return regimeFinanceiro == (short)TipoRegimeFinanceiroEnum.Capitalizacao ?
                    new List<TipoProvisaoEnum> { TipoProvisaoEnum.PMBAC } :
                    new List<TipoProvisaoEnum> { TipoProvisaoEnum.PRNE, TipoProvisaoEnum.PPNG };
        }

        public override IList<short> RegimeFinanceiroPermitido => new List<short>()
        {
            (short) TipoRegimeFinanceiroEnum.Capitalizacao,
        };       
    }
}
