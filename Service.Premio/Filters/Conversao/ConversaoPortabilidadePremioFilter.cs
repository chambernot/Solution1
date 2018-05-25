using System.Threading.Tasks;
using GreenPipes;
using Mongeral.Provisao.V2.Application.Context;
using Mongeral.Provisao.V2.Service.Premio.Extensions;

namespace Mongeral.Provisao.V2.Service.Premio.Filters.Conversao
{
    public class ConversaoPortabilidadePremioFilter: IFilter<PortabilidadePremioContext>
    {
        public async Task Send(PortabilidadePremioContext context, IPipe<PortabilidadePremioContext> next)
        {
            context.Premios = context.Request.ToEventoPortabilidade();
            await next.Send(context);
        }

        public void Probe(ProbeContext context)
        {
            context.CreateScope("ConversaoPortabilidadePremio");
        }
    }
}
