using Mongeral.Integrador.Contratos.Premio;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio
{
    public class PortabilidadeBuilder: ParcelaBaseBuilder<IPortabilidade>
    {
        private PortabilidadeBuilder() { }

        public static PortabilidadeBuilder UmBuilder()
        {
            return new PortabilidadeBuilder();
        }

        public PortabilidadeBuilder ComCodigoSusep(string codigo)
        {
            Mock.SetupGet(x => x.CodigoSusep).Returns(codigo);
            return this;
        }

        public PortabilidadeBuilder Com(PagamentoBuilder pagamento)
        {
            Mock.SetupGet(x => x.Pagamento).Returns(pagamento.Build());
            return this;
        }

        public PortabilidadeBuilder Padrao()
        {
            ComPremioPadrao();
            return this;
        }
    }
}
