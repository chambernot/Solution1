using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Testes.Aceitacao.Util
{
    public class ImplantacaoParam
    {
        public Guid Identificador { get; set; }
        public long IdExterno { get; set; }
        public int ItemProdutoId { get; set; }
        public DateTime DataInicioVigencia { get; set; }
        public int ClasseRiscoId { get; set; }
        public int TipoFormaContratacaoId { get; set; }
        public int TipoRendaId { get; set; }
        public string IdentificadorNegocio { get; set; }
        public long InscricaoId { get; set; }
        public long Matricula { get; set; }
        public int Periodicidade { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
    }
}
