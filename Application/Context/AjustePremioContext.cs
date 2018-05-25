using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Integrador.Contratos;
using System.Collections.Generic;

namespace Mongeral.Provisao.V2.Application.Context
{
    public class AjustePremioContext: ProvisaoContext
    {
        public AjustePremioContext(IParcelaAjustada request) : base(request) { }

        protected override EventoOperacional CriarEvento(IEvento request)
        {
            return new EventoAjustePremio(request.Identificador, request.IdentificadorCorrelacao, request.IdentificadorNegocio, request.DataExecucaoEvento);
        }

        public IEnumerable<Premio> Premios { get; set; }
        public new EventoAjustePremio Evento => base.Evento as EventoAjustePremio;
        public new IParcelaAjustada Request => base.Request as IParcelaAjustada;
    }
}
