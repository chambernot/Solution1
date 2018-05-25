using System;

namespace Mongeral.Provisao.V2.DTO
{
    public class PagamentoPremio
    {
        public DateTime DataPagamento { get; set; }
        public DateTime DataApropriacao { get; set; }
        public decimal ValorPago { get; set; }
        public decimal Desconto { get; set; }
        public decimal Multa { get; set; }
        public decimal IOFRetido { get; set; }
        public decimal IOFARecolher { get; set; }
        public string IdentificadorCredito { get; set; }
    }
}
