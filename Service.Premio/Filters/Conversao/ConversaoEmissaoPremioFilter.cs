using GreenPipes;
using Mongeral.Provisao.V2.Application.Context;
using Mongeral.Provisao.V2.Service.Premio.Extensions;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Service.Premio.Filters.Conversao
{
    public class ConversaoEmissaoPremioFilter : IFilter<EmissaoPremioContext>
    {
        public async Task Send(EmissaoPremioContext context, IPipe<EmissaoPremioContext> next)
        {
            context.Premios = context.Request.ToEventoPremio();

            await next.Send(context);
        }
        public void Probe(ProbeContext context)
        {
            context.CreateScope("ConversaoEmissaoPremio");
        }
    }
}
