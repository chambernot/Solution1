using System.Collections.Generic;
using Mongeral.Integrador.Contratos;
using Mongeral.Integrador.Contratos.VG;
using Mongeral.Provisao.V2.Domain;

namespace Mongeral.Provisao.V2.Application.Context
{
    public class InclusaoVgContext: ProvisaoContext
    {
        public InclusaoVgContext(IEvento request) : base(request)
        {
        }

        protected override EventoOperacional CriarEvento(IEvento request)
        {
            return new EventoInclusaoVg(request.Identificador, request.IdentificadorCorrelacao, request.IdentificadorNegocio, request.DataExecucaoEvento);
        }

        public IEnumerable<CoberturaContratada> Coberturas { get; set; }
        public new EventoInclusaoVg Evento => base.Evento as EventoInclusaoVg;
        public new IInclusaoCoberturaGrupal Request => base.Request as IInclusaoCoberturaGrupal;
    }
}
