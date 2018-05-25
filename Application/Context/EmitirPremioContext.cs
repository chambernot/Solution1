using System.Collections.Generic;
using Mongeral.Integrador.Contratos;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Domain;

namespace Mongeral.Provisao.V2.Application.Context
{
    public class EmissaoPremioContext : ProvisaoContext
    {
        public EmissaoPremioContext(IParcelaEmitida request) : base(request) { }        

        protected override EventoOperacional CriarEvento(IEvento request)
        {
            return new EventoEmissaoPremio(request.Identificador, request.IdentificadorCorrelacao, request.IdentificadorNegocio, request.DataExecucaoEvento);
        }
        public IEnumerable<Premio> Premios { get; set; }
        public new EventoEmissaoPremio Evento => base.Evento as EventoEmissaoPremio;
        public new IParcelaEmitida Request => base.Request as IParcelaEmitida;
    }
}
