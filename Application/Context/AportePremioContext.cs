using System.Collections.Generic;
using Mongeral.Integrador.Contratos;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Entidades.EventosOperacional;
using Mongeral.Provisao.V2.Domain.Entidades.Premios;

namespace Mongeral.Provisao.V2.Application.Context
{
    public class AportePremioContext: ProvisaoContext
    {
        public AportePremioContext(IAporteApropriado request) : base(request) { }

        protected override EventoOperacional CriarEvento(IEvento request)
        {
            return new EventoAportePremio(request.Identificador, request.IdentificadorCorrelacao, request.IdentificadorNegocio, request.DataExecucaoEvento);
        }

        public IEnumerable<PremioAporte> Premios { get; set; }
        public new EventoAportePremio Evento => base.Evento as EventoAportePremio;
        public new IAporteApropriado Request => base.Request as IAporteApropriado;
    }
}
