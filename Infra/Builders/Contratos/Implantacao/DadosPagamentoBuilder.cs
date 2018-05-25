using Mongeral.Integrador.Contratos;
using Mongeral.Integrador.Contratos.Enum;
using Mongeral.Provisao.V2.Testes.Infra.Builders;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Implantacao
{
    public class DadosPagamentoBuilder: MockBuilder<IFormaPagamento>
    {
        public static DadosPagamentoBuilder UmPagamento() { return new DadosPagamentoBuilder(); }

        public DadosPagamentoBuilder ComPeriodicidade(Periodicidade periodicidade)
        {
            Mock.SetupGet(p => p.Periodicidade).Returns(periodicidade);
            return this;
        }
    }    
}
