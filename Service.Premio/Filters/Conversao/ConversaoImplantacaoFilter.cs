using System.Threading.Tasks;
using GreenPipes;
using Mongeral.Provisao.V2.Application.Context;
using Mongeral.Provisao.V2.Service.Premio.Extensions;

namespace Mongeral.Provisao.V2.Service.Premio.Filters.Conversao
{
    public class ConversaoImplantacaoFilter : IFilter<ImplantacaoContext>
    {
        public async Task Send(ImplantacaoContext context, IPipe<ImplantacaoContext> next)
        {
            context.Coberturas = context.Request.ObterCoberturasDaProposta();
            await next.Send(context);
        }

        public void Probe(ProbeContext context)
        {
            context.CreateScope("IPropostaToImplantacaoFilter");
        }
    }

}