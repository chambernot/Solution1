using System.Threading.Tasks;
using GreenPipes;
using Mongeral.Provisao.V2.Application.Context;
using Mongeral.Provisao.V2.Service.Premio.Extensions;

namespace Mongeral.Provisao.V2.Service.Premio.Filters.Conversao
{
    public class ConversaoInclusaoVgFilter: IFilter<InclusaoVgContext>
    {
        public async Task Send(InclusaoVgContext context, IPipe<InclusaoVgContext> next)
        {
            context.Coberturas = context.Request.ObterCoberturasDaProposta();
            await next.Send(context);
        }

        public void Probe(ProbeContext context)
        {
            context.CreateScope("IInclusaoCoberturaGrupalToInclusaoVgFilter");
        }
    }
}
