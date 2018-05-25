using Mongeral.Provisao.V2.Service.Premio.Calculadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Service.Premio.Factory
{
    public class CalculadorProvisaoPovider
    {
        protected readonly List<CalculadorProvisaoMatematicaBeneficioAConceder> Calculadores = new List<CalculadorProvisaoMatematicaBeneficioAConceder>();

        public IEnumerable<CalculadorProvisaoMatematicaBeneficioAConceder> ObterCalculadoresProvisao()
        {
            return Calculadores;
        }
    }
}
