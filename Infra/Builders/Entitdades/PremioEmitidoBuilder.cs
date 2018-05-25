using Mongeral.Provisao.V2.Domain;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Entitdades
{
    public class PremioEmitidoBuilder : PremioBuilder<Premio>
    {
        public PremioEmitidoBuilder(): base() { Instance = new Premio(); }

        public static PremioEmitidoBuilder Um()
        {
            return new PremioEmitidoBuilder();
        }

        public PremioEmitidoBuilder Com(params MovimentoProvisaoBuilder[] movimento)
        {
            ComMovimentoBuilder(movimento);
            return this;
        }

        public PremioEmitidoBuilder Com(CoberturaContratadaBuilder cobertura)
        {
            ComCoberturaBuilder(cobertura);
            return this;
        }

        public PremioEmitidoBuilder Padrao()
        {
            ComPremioPadrao();
            return this;
        }
    }
}