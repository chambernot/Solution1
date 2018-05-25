using System;
using System.Collections.Generic;
using System.Linq;
using Mongeral.Provisao.V2.Domain.Enum;

namespace Mongeral.Provisao.V2.Domain
{
    public class EventoImplantacao : EventoOperacional
    {
        public EventoImplantacao(Guid identificador, string idCorrelacao, string idNegocio, DateTime dataExecucao)
            : base (identificador, idCorrelacao, idNegocio, dataExecucao)
        {
            _listaCoberturas = new List<CoberturaContratada>();
        }

        protected readonly List<CoberturaContratada> _listaCoberturas;
        public IEnumerable<CoberturaContratada> Coberturas => _listaCoberturas.AsEnumerable();
        public override short TipoEventoId => (short) TipoEventoEnum.Implantacao;

        public EventoImplantacao ComCoberturas(IEnumerable<CoberturaContratada> coberturas)
        {
            foreach (var cobertura in coberturas)
            {
                _listaCoberturas.Add(cobertura);
                cobertura.InformaEvento(this);
            }
            return this;
        }
        

        public IEnumerable<short> MovimentosPermitidos { get; }        
    }
   
}
