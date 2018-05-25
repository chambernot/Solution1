using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Domain.Interfaces
{
    public interface IPropostaEmissao
    {
        Guid Identificador { get; }
        long ItemCertificadoApoliceId { get; }
        DateTime Competencia { get; }
        DateTime DataInicioVigencia { get; }

        DateTime DataFimVigencia { get; }

        decimal ValorContribuicao { get; }
    }
}
