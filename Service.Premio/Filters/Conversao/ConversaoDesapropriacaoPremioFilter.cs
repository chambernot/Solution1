using Mongeral.Provisao.V2.Application.Context;
using GreenPipes;
using System.Threading.Tasks;
using Mongeral.Provisao.V2.Service.Premio.Extensions;

namespace Mongeral.Provisao.V2.Service.Premio.Filters.Conversao
{
    public class ConversaoDesapropriacaoPremioFilter : IFilter<DesapropriacaoPremioContext>
    {
        public async Task Send(DesapropriacaoPremioContext context, IPipe<DesapropriacaoPremioContext> next)
        {
            context.Premios = context.Request.ToEventoDesapropriacao();

            await next.Send(context);
        }

        public void Probe(ProbeContext context)
        {
            context.CreateScope("ConversaoDesapropriacaoPremio");
        }
    }
}
