using System;
using Mongeral.Calculo.Contratos.Assinatura;

namespace Mongeral.Provisao.V2.Adapters.SvcImplementation
{
    public class CalculoService : ICalculoService
    {
        public CalculoPMBACResultado CalcularPmbac(ParametrosCalculoPmbac pmbac)
        {
            
            return WcfClient<ICalculoService>.CallUsing(x => x.CalcularPmbac(pmbac));
        }

        public CalculoPMBCResultado CalcularPmbc(ParametrosCalculoPmbc pmbc)
        {
            throw new NotImplementedException();
        }

        public CalculoTaxaBeneficioResultado CalcularTaxaBeneficio(ParametrosCalculoTaxaBeneficio taxaBeneficio)
        {
            throw new NotImplementedException();
        }

        public CalculoTaxaPremioResultado CalcularTaxaPremio(ParametrosCalculoTaxaPremio taxaPremio)
        {
            throw new NotImplementedException();
        }
    }
}
