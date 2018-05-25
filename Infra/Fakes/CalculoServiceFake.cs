using Mongeral.Calculo.Contratos.Assinatura;
using System;

namespace Mongeral.Provisao.V2.Testes.Infra.Fake
{
    public class CalculoServiceFake : ICalculoService
    {
        public CalculoPMBACResultado CalcularPmbac(ParametrosCalculoPmbac pmbac)
        {
            var resultado = new CalculoPMBACResultado()
            {
                //AberturaValorAtualizacaoMonetaria = 0,
                //AberturaValorJuros = 0,
                //AberturaValorSobrevivencia = 0,
                Fator = (decimal)0.0000000000000000000000000047,
                Identificador = 0,
                PercentualJuros = (decimal)0.035000
            };

            return resultado;
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
