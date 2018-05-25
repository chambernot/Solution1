using Mongeral.Integrador.Contratos;
using Mongeral.Integrador.Contratos.Enum;
using Mongeral.Provisao.V2.Testes.Infra.Builders;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Implantacao
{
    public class ContratacaoBuilder : MockBuilder<IContratacao>
    {
        public static ContratacaoBuilder UmaContratacao() { return new ContratacaoBuilder(); }

        public ContratacaoBuilder ComTipoRenda(TipoDeRendaEnum tipo)
        {
            Mock.SetupGet(x => x.TipoDeRenda).Returns(tipo);
            return this;
        }
        public ContratacaoBuilder ComTipoFormaContratacao(TipoFormaContratacaoEnum tipo)
        {
            Mock.SetupGet(x => x.TipoFormaContratacao).Returns(tipo);
            return this;
        }

        public ContratacaoBuilder Padrao()
        {
            ComTipoRenda(TipoDeRendaEnum.NaoSeAplica);
            ComTipoFormaContratacao(TipoFormaContratacaoEnum.RendaMensal);
            return this;
        }
    }
}