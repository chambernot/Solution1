using Mongeral.Provisao.V2.Domain.Enum;

namespace Mongeral.Provisao.V2.Domain
{
    public class TabuaBiometrica
    {        
        public short Id { get; set; }
        public TipoTabuaBiometricaEnum Tipo { get; set; }        
        public decimal? FatorAgravamento { get; set; }        
        public decimal? Complemento { get; set; }        
        public string IdSexo { get; set; }
    }
}
