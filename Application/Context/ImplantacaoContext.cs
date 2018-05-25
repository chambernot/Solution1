using System.Collections.Generic;
using Mongeral.Integrador.Contratos;
using Mongeral.Provisao.V2.Domain;

namespace Mongeral.Provisao.V2.Application.Context
{
    public class ImplantacaoContext : ProvisaoContext
    {
        public ImplantacaoContext(IProposta request) : base(request)
        {
        }

        public IEnumerable<CoberturaContratada> Coberturas { get; set; }
        protected override EventoOperacional CriarEvento(IEvento request)
        {
            return new EventoImplantacao(request.Identificador, request.IdentificadorCorrelacao, request.IdentificadorNegocio, request.DataExecucaoEvento);
        }

        public new EventoImplantacao Evento => base.Evento as EventoImplantacao;
        public new IProposta Request => base.Request as IProposta;
    }
}
