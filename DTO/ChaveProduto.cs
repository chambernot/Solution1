using System;

namespace Mongeral.Provisao.V2.DTO
{
    public class ChaveProduto : IEquatable<ChaveProduto>
    {
        public int ItemProdutoId { get; }
        public int TipoFormaContratacaoId { get; }
        public int? TipoRendaId { get; }
        public DateTime DataVigencia { get; }

        private DateTime _inicioVigencia;
        private DateTime _fimVigencia;

        public ChaveProduto(int itemProdutoId, int tipoFormaContratacaoId, int? tipoRendaId, DateTime data)
        {
            ItemProdutoId = itemProdutoId;
            TipoFormaContratacaoId = tipoFormaContratacaoId;
            DataVigencia = data;
            TipoRendaId = tipoRendaId;
        }

        public bool Equals(ChaveProduto other)
        {
            var result = this.ItemProdutoId == other.ItemProdutoId 
                && TipoFormaContratacaoId == other.TipoFormaContratacaoId 
                && TipoRendaId == other.TipoRendaId
                && other.EhVigente(this.DataVigencia);

            return result;
        }

        private bool EhVigente(DateTime dataVigencia)
        {
            return dataVigencia >= _inicioVigencia && dataVigencia <= _fimVigencia;
        }

        public void SetVigencia(DateTime dataInicioVigencia, DateTime dataFimVigencia)
        {
            _inicioVigencia = dataInicioVigencia;
            _fimVigencia = dataFimVigencia;
        }
    }
}
