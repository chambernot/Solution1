using System.Collections.Generic;
using Mongeral.Integrador.Contratos;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Entidades;
using Mongeral.Provisao.V2.Domain.Entidades.EventosOperacional;

namespace Mongeral.Provisao.V2.Application.Context
{
    public class PortabilidadePremioContext: ProvisaoContext
    {
        public PortabilidadePremioContext(IPortabilidadeApropriada request) : base(request) { }

        protected override EventoOperacional CriarEvento(IEvento request)
        {
            return new EventoPortabilidadePremio(request.Identificador, request.IdentificadorCorrelacao, request.IdentificadorNegocio, request.DataExecucaoEvento);
        }

        public IEnumerable<PremioPortabilidade> Premios { get; set; }
        public new EventoPortabilidadePremio Evento => base.Evento as EventoPortabilidadePremio;
        public new IPortabilidadeApropriada Request => base.Request as IPortabilidadeApropriada;
    }
}
