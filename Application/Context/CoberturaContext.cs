using GreenPipes;
using Mongeral.Provisao.V2.Domain;

namespace Mongeral.Provisao.V2.Application.Context
{
    public class CoberturaContext : BasePipeContext, PipeContext
    {
        public CoberturaContext(CoberturaContratada cobertura)
        {
            Data = cobertura;
            Instance = null;
        }

        public CoberturaContratada Data { get; }
        public CoberturaContratada Instance { get; set; }
    }
}