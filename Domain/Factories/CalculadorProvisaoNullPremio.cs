using System;
using System.Collections.Generic;

namespace Mongeral.Provisao.V2.Domain.Factories
{
    public class CalculadorProvisaoEmissaoNull : ICalculadorProvisaoPremio
    {
        public IEnumerable<MovimentoProvisao> CriarProvisao(Premio premio)
        {
            return null;
        }
    }
}