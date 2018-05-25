using System.Collections.Generic;
using Mongeral.Integrador.Contratos;
using Mongeral.Provisao.V2.Domain;

namespace Mongeral.Provisao.V2.Application.Context
{
    public class AlteracaoContext : ProvisaoContext
    {
        public AlteracaoContext(IProposta request) : base(request)
        {

        }

        protected override EventoOperacional CriarEvento(IEvento request)
        {
            return new EventoAlteracao(request.Identificador, request.IdentificadorCorrelacao, request.IdentificadorNegocio, request.DataExecucaoEvento);
        }

        public IEnumerable<HistoricoCoberturaContratada> Historicos { get; set; }
        public new EventoAlteracao Evento => base.Evento as EventoAlteracao;
        public new IProposta Request => base.Request as IProposta;
        
    }
}