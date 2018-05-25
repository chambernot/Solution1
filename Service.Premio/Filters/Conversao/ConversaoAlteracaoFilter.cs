using System.Threading.Tasks;
using GreenPipes;
using Mongeral.Provisao.V2.Application.Context;
using Mongeral.Provisao.V2.Service.Premio.Extensions;

namespace Mongeral.Provisao.V2.Service.Premio.Filters.Conversao
{
    public class ConversaoAlteracaoFilter : IFilter<AlteracaoContext>
    {
        public async Task Send(AlteracaoContext context, IPipe<AlteracaoContext> next)
        {
            context.Historicos = context.Request.ToHistoricoCoberturas();
            
            await next.Send(context);
        }

        public void Probe(ProbeContext context)
        {
            context.CreateScope("ConversaoAlteracaoParamentros");
        }
    }
}