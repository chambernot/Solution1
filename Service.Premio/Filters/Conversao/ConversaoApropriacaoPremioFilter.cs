using System.Threading.Tasks;
using GreenPipes;
using Mongeral.Provisao.V2.Application.Context;
using Mongeral.Provisao.V2.Service.Premio.Extensions;

namespace Mongeral.Provisao.V2.Service.Premio.Filters.Conversao
{
    public class ConversaoApropriacaoPremioFilter : IFilter<ApropriacaoPremioContext>
    {
        public async Task Send(ApropriacaoPremioContext context, IPipe<ApropriacaoPremioContext> next)
        {
            context.Premios = context.Request.ToEventoApropriacao();

            await next.Send(context);
        }

        public void Probe(ProbeContext context)
        {
            context.CreateScope("ConversaoApropriacaoPremio");
        }
    }
}
