using Mongeral.Provisao.V2.NinjectConfig;
using Mongeral.Provisao.V2.Testes.Infra.Fake;
using Mongeral.Calculo.Contratos.Assinatura;
using Mongeral.Provisao.V2.Adapters;
using Mongeral.Provisao.V2.Testes.Infra.Fakes;

namespace Mongeral.Provisao.V2.Testes.Aceitacao
{
    public class AcceptanceTestBaseModule : NinjectConfigurationModule
    {
        public override void Load()
        {
            base.Load();
            //Rebind<ITransactionalMethod>().To<TransactionalMethodFake>();
            //Rebind<IndexedCachedContainer<ChaveProduto, DadosProduto>>().To<DadosProdutoUtilFake>();
            Rebind<ICalculoService>().To<CalculoServiceFake>();
            Rebind<IProdutoAdapter>().To<ProdutoAdapterFake>();
        }
    }
}
