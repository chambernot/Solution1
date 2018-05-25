using Mongeral.Integrador.Contratos;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Integrador.Contratos.Premio;
using System.Collections.Generic;
using Mongeral.Provisao.V2.Domain.Entidades;

namespace Mongeral.Provisao.V2.Application.Context
{
    public class DesapropriacaoPremioContext : ProvisaoContext
    {
        public DesapropriacaoPremioContext(IParcelaDesapropriada request) : base(request) { }
        
        protected override EventoOperacional CriarEvento(IEvento request)
        {
            return new EventoDesapropriacaoPremio(request.Identificador, request.IdentificadorCorrelacao, request.IdentificadorNegocio, request.DataExecucaoEvento);
        }

        public IEnumerable<PremioDesapropriado> Premios { get; set; }

        public new EventoDesapropriacaoPremio Evento => base.Evento as EventoDesapropriacaoPremio;

        public new IParcelaDesapropriada Request => base.Request as IParcelaDesapropriada;
    }
}
