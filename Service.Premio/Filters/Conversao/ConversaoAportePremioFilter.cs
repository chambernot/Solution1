using System;
using System.Threading.Tasks;
using GreenPipes;
using Mongeral.Provisao.V2.Application.Context;
using Mongeral.Provisao.V2.Service.Premio.Extensions;

namespace Mongeral.Provisao.V2.Service.Premio.Filters.Conversao
{
    public class ConversaoAportePremioFilter: IFilter<AportePremioContext>
    {
        public async Task Send(AportePremioContext context, IPipe<AportePremioContext> next)
        {
            context.Premios = context.Request.ToEventoAporte();

            await next.Send(context);
        }

        public void Probe(ProbeContext context)
        {
            context.CreateScope("ConversaoAportePremio");
        }
    }
}
