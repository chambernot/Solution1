using Mongeral.Integrador.Contratos.Premio;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio
{
    public class ApropriacaoBuilder : ParcelaBaseBuilder<IApropriacao>
    {
        public static ApropriacaoBuilder UmBuilder()
        {
            return new ApropriacaoBuilder();
        }

        public ApropriacaoBuilder Com(PagamentoBuilder pagamento)
        {
            Mock.SetupGet(x => x.Pagamento).Returns(pagamento.Build());
            return this;
        }

        public ApropriacaoBuilder Padrao()
        {
            ComPremioPadrao();
            return this;
        }
    }
}
