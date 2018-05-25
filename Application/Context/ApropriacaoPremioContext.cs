using System.Collections.Generic;
using Mongeral.Integrador.Contratos;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Domain.Entidades;

namespace Mongeral.Provisao.V2.Application.Context
{
    public class ApropriacaoPremioContext : ProvisaoContext
    {
        public ApropriacaoPremioContext(IParcelaApropriada request) : base(request) { }

        protected override EventoOperacional CriarEvento(IEvento request)
        {
            return new EventoApropriacaoPremio(request.Identificador, request.IdentificadorCorrelacao, request.IdentificadorNegocio, request.DataExecucaoEvento);
        }

        public IEnumerable<PremioApropriado> Premios { get; set; }
        public new EventoApropriacaoPremio Evento => base.Evento as EventoApropriacaoPremio;
        public new IParcelaApropriada Request => base.Request as IParcelaApropriada;
    }
}