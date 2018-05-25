using Mongeral.Integrador.Contratos;
using Mongeral.Provisao.V2.Domain;

namespace Mongeral.Provisao.V2.Application.Context
{
    public class CompensacaoContext<TEventoOperacional> : ProvisaoContext
    {
        public CompensacaoContext(IEvento evento) : base(evento) { }

        protected override EventoOperacional CriarEvento(IEvento request)
        {
            return null;
        }
    }
}