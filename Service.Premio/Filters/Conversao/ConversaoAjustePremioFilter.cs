using GreenPipes;
using Mongeral.Provisao.V2.Application.Context;
using Mongeral.Provisao.V2.Service.Premio.Extensions;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Service.Premio.Filters.Conversao
{
    public class ConversaoAjustePremioFilter : IFilter<AjustePremioContext>
    {
        public async Task Send(AjustePremioContext context, IPipe<AjustePremioContext> next)
        {
            context.Premios = context.Request.ToEventoAjustePremio();

            await next.Send(context);
        }

        public void Probe(ProbeContext context)
        {
            context.CreateScope("ConversaoAjustePremio");
        }
    }
}
