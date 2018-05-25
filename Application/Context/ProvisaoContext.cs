using GreenPipes;
using Mongeral.Integrador.Contratos;
using Mongeral.Provisao.V2.Domain;

namespace Mongeral.Provisao.V2.Application.Context
{
    public abstract class ProvisaoContext : BasePipeContext, PipeContext
    {
        public ProvisaoContext(IEvento request)
        {
            Request = request;
            Evento = CriarEvento(request);
        }

        protected abstract EventoOperacional CriarEvento(IEvento request);
        public EventoOperacional Evento { get; }
        public IEvento Request { get; }
    }    
}
