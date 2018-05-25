using Mongeral.Provisao.V2.Application.Interfaces;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.Provisao.V2.Domain.Factories;
using System;

namespace Mongeral.Provisao.V2.Application.Provisao
{
    public class CalculoProvisaoPremioNaoGanho: IEventoProvisao
    {
        public EventoEmissaoPremio CriarProvisao(EventoEmissaoPremio evento, CoberturaContratada cobertura)
        {
            if (cobertura.RegimeFinanceiroId.Equals(TipoRegimeFinanceiroEnum.ReparticaoSimples))
                return evento;

            //Processo para gerar pmbac
            return evento;
        }        
    }
}
