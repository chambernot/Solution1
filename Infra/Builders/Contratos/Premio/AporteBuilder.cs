using Mongeral.Integrador.Contratos.Premio;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio
{
    public class AporteBuilder: ParcelaBaseBuilder<IAporte>
    {
        private AporteBuilder(): base() { }

        public static AporteBuilder UmBuilder()
        {
            return new AporteBuilder();
        }

        public AporteBuilder Com(PagamentoBuilder pagamento)
        {
            Mock.SetupGet(x => x.Pagamento).Returns(pagamento.Build());
            return this;
        }

        public AporteBuilder Padrao()
        {
            ComPremioPadrao();
            return this;
        }
    }
}
