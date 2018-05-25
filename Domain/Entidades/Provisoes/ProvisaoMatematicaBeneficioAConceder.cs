using Mongeral.Provisao.V2.DTO;

namespace Mongeral.Provisao.V2.Domain
{
    public class ProvisaoMatematicaBeneficioAConceder: ProvisaoDto
    {
        public decimal Fator { get; set; }
        public decimal Desvio { get; set; }
        public decimal ValorBeneficioCorrigido { get; set; }
    }
}
