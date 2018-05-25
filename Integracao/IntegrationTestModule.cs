using Mongeral.Calculo.Contratos.Assinatura;
using Mongeral.Provisao.V2.Adapters;
using Mongeral.Provisao.V2.NinjectConfig;
using Mongeral.Provisao.V2.Testes.Infra.Fake;
using Mongeral.Provisao.V2.Testes.Infra.Fakes;

namespace Mongeral.Provisao.V2.Testes.Integracao
{
    public class IntegrationTestModule : NinjectConfigurationModule
    {
        public override void Load()
        {
            base.Load();
            Rebind<ICalculoService>().To<CalculoServiceFake>();
            Rebind<IProdutoAdapter>().To<ProdutoAdapterFake>();
        }
    }
}
