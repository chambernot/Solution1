using System;
using System.Collections.Generic;
using Mongeral.Provisao.V2.Domain.Enum;

namespace Mongeral.Provisao.V2.Domain
{
    public class EventoInclusaoVg: EventoImplantacao
    {
        public EventoInclusaoVg(Guid identificador, string idCorrelacao, string idNegocio, DateTime dataExecucao) 
            : base(identificador, idCorrelacao, idNegocio, dataExecucao)
        {
        }
                
        public override short TipoEventoId => (short) TipoEventoEnum.InclusaoVg;

        public EventoInclusaoVg ComCoberturasVg(IEnumerable<CoberturaContratada> coberturas)
        {
            foreach (var cobertura in coberturas)
            {
                _listaCoberturas.Add(cobertura);
                cobertura.InformaEvento(this);
            }
            return this;
        }
    }
}
